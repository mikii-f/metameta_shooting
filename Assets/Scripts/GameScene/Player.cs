using System.Collections;
using System.Collections.Generic;
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
    private bool isSkill1 = false;
    private bool isSkill2 = false;
    private bool isSkill3 = false;
    [SerializeField] private float playerSpeed = 600;
    private float interval = 0.1f;      //弾丸発射の間隔
    private float intervalCount = 0;    //intervalの計測用
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
        if (!GameManager.isPause)
        {
            Move();
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
            angle = Mathf.Atan2(mousePosition.y, mousePosition.x);
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
            float newX = Mathf.Clamp(playerRect.anchoredPosition.x + playerSpeed * Mathf.Cos(angle) * Time.deltaTime, -320, 320);
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

    public void Skill1()
    {

    }
    public void Skill2()
    {

    }
    public void Skill3()
    {

    }
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        //衝突したのが敵または敵の弾丸であるならば、GameManagerにライフの減少を通知し、無敵時間に入る
        if (enemy.CompareTag("Enemy") || enemy.CompareTag("EnemyBullet"))
        {
            gameManager.Damaged();
            StartCoroutine(Damaged());
        }
        //弾丸の場合はその弾丸をDestroy
        if(enemy.CompareTag("EnemyBullet"))
        {
            Destroy(enemy.gameObject);
        }
    }
    //ダメージを受けた時の点滅及び無敵時間
    private IEnumerator Damaged()
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
        yield return new WaitForSeconds(0.25f);
        ColorChange(defaultColor);
        playerCollider.enabled = true;
    }
    private void ColorChange(Color color)
    {
        base1.color = color;
        base2.color = color;
    }
}