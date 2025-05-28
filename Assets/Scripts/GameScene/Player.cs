using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//自機の動作を管理する
public class Player : MonoBehaviour
{
    private RectTransform playerRect;       //自身の座標
    [SerializeField] private Image base1;
    [SerializeField] private Image base2;
    private readonly Color defaultColor = new(0, 170f / 255, 1, 1);
    private Collider2D playerCollider;      //自身の当たり判定
    [SerializeField] private Image semiCircle;
    private RectTransform semiCircleRect;
    [SerializeField] private RectTransform bulletParent;    //弾丸の複製先(親オブジェクト)
    [SerializeField] private GameManager gameManager;   //GameManagrへのアクセス用
    [SerializeField] private float playerSpeed = 600;
    private float interval = 0.1f;      //弾丸発射の間隔
    private float intervalCount = 0;    //intervalの計測用
    private bool isEnemyCollision = false;
    private bool isWordCollision = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRect = GetComponent<RectTransform>();
        playerCollider = GetComponent<Collider2D>();
        semiCircleRect = semiCircle.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        isEnemyCollision = false;
        isWordCollision = false;
        intervalCount += Time.deltaTime;
        if (!GameManager.isPause)
        {
            Move();
            //スキル1発動中の場合、intervalの半分までカウントしたら発射


            //スキル1未発動の場合、intervalまでカウントしたら発射

            if (intervalCount >= interval)
            {
                intervalCount = 0;
                //ダメージを受けた直後、ゲーム終了後は撃たない
                if (playerCollider.enabled && !gameManager.finishButton.activeSelf)
                {
                    Bullet();
                }
            }
        }
    }
    private void Move()
    {
        float angle = -999;
        //左ドラッグ中はマウスの位置(スマホなら手の位置)に応じて自機が移動
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;    //マウスの位置(画面左下が(0,0))
                                                            //右下の操作マーク(画面中心を原点とする座標において、(x, y)=(660, -360))が操作の中心となるように補正
            mousePosition.x -= 1620;
            mousePosition.y -= 180;
            if (Mathf.Pow(Mathf.Pow(mousePosition.x, 2) + Mathf.Pow(mousePosition.y, 2), 0.5f) < 400)
            {
                angle = Mathf.Atan2(mousePosition.y, mousePosition.x);
            }
        }
        //矢印キーでの操作への対応
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            angle = Mathf.PI / 4;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            angle = 3 * Mathf.PI / 4;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            angle = -3 * Mathf.PI / 4;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            angle = -Mathf.PI / 4;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            angle = 0;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            angle = Mathf.PI / 2;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            angle = Mathf.PI;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            angle = -Mathf.PI / 2;
        }
        //プレイヤーの入力を受け付け、angleの値が変更されていたらその方向に移動
        if (angle >= -100)
        {
            float newX = Mathf.Clamp(playerRect.anchoredPosition.x + playerSpeed * Mathf.Cos(angle) * Time.deltaTime, -Manager.gameWidth, Manager.gameWidth);
            float newY = Mathf.Clamp(playerRect.anchoredPosition.y + playerSpeed * Mathf.Sin(angle) * Time.deltaTime, -520, 480);
            playerRect.anchoredPosition = new(newX, newY);
            semiCircle.color = new(1, 1, 1, 0.5f);
            semiCircleRect.localEulerAngles = new(0, 0, angle * Mathf.Rad2Deg - 90);
        }
        else
        {
            semiCircle.color = Color.clear;
        }
    }
    private void Bullet()
    {
        GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/PlayerBullet"), bulletParent);
        newBullet.GetComponent<RectTransform>().anchoredPosition = playerRect.anchoredPosition + new Vector2(0, 50);
        //スキル2発動中の場合、追加で角度π/3, 2π/3の方向にも弾丸を発射(Enemy0_1.csの50行目付近を参考)

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameManager.finishButton.activeSelf && !isEnemyCollision)
        {
            //衝突したのが敵または敵の弾丸であるならば、GameManagerにライフの減少を通知し、無敵時間に入る
            if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
            {
                isEnemyCollision = true;
                if (gameManager.Damaged() == 0)
                {
                    StartCoroutine(Damaged());
                }
                else
                {
                    StartCoroutine(Destroyed());
                }
            }
            //弾丸の場合はその弾丸をDestroy
            if (collision.CompareTag("EnemyBullet"))
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Word") && !isWordCollision)
        {
            isWordCollision = true;
            //色やScaleに応じたスコア増減処理
            Text t = collision.GetComponent<Text>();
            int cScore = 0;
            if (t.text == "メタ")
            { 
                //テキストがメタであった場合
                if (t.color == new Color(1, 1, 0, 1))
                {
                    //テキストのカラーがオレンジの場合+1000点
                    cScore = 1000;
                }
                else if (Mathf.Abs(t.rectTransform.localScale.x - 0.7f) < 0.1f)
                {
                    //テキストのスケールが小さい場合+1000点
                    cScore = 1000;
                }
                else
                {
                    //それ以外の場合+500点
                    cScore = 500;
                }
            }
            else
            {
                //テキストがメタでなかった場合
                //テキストが「メタ」以外であった場合-250点
                cScore = -250;
            }


            gameManager.ScoreChange(cScore);
            Destroy(collision.gameObject);
        }
    }
    //ダメージを受けた時の点滅及び無敵時間
    private IEnumerator Damaged()
    {
        playerCollider.enabled = false;
        ColorChange(Color.clear);
        yield return StartCoroutine(Wait(0.2f));
        ColorChange(defaultColor);
        yield return StartCoroutine(Wait(0.2f));
        ColorChange(Color.clear);
        yield return StartCoroutine(Wait(0.2f));
        ColorChange(defaultColor);
        yield return StartCoroutine(Wait(0.2f));
        ColorChange(Color.clear);
        yield return StartCoroutine(Wait(0.2f));
        ColorChange(defaultColor);
        playerCollider.enabled = true;
    }
    private IEnumerator Wait(float t)
    {
        float temp = 0;
        while (true)
        {
            if (!GameManager.isPause)
            {
                yield return null;
                temp += Time.deltaTime;
                if (temp >= t)
                {
                    break;
                }
            }
            yield return null;
        }
    }
    private IEnumerator Destroyed()
    {
        playerCollider.enabled = false;
        ColorChange(Color.clear);
        yield return new WaitForSeconds(0.25f);
        ColorChange(defaultColor);
        yield return new WaitForSeconds(0.25f);
        ColorChange(Color.clear);
        yield return new WaitForSeconds(0.25f);
        ColorChange(defaultColor);
        yield return new WaitForSeconds(0.25f);
        ColorChange(Color.clear);
    }
    private void ColorChange(Color color)
    {
        base1.color = color;
        base2.color = color;
    }
}