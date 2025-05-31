using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//レベル2の敵(ボス級) 行動パターン0
public class Enemy2_0 : Enemy
{
    private int duration = 40;
    private float durationCount = 0;
    private bool back = false;
    //private float firstX = 0;
    //private float firstY = 650;
    //private System.Random random = new System.Random();
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 100;
        hp = 100;
        enemyLevel = EnemyLevel.Level2;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        interval = 7;
        intervalCount = 3;
    }
    //void Start()
    //{
    //    firstY = parentRect.anchoredPosition.y;
    //    firstX = parentRect.anchoredPosition.x;
    //}

    // Update is called once per frame
    void Update()
    {
        isBulletCollision = false;
        if (!GameManager.isPause)
        {
            durationCount += Time.deltaTime;
            intervalCount += Time.deltaTime;
            //退却体勢に入る
            if (durationCount > duration)
            {
                back = true;
            }
            //上から降りてくる
            if (!back && parentRect.anchoredPosition.y > 400)
            {
                parentRect.anchoredPosition += new Vector2(0, -75) * Time.deltaTime;
            }
            //上に帰っていく
            else if (back)
            {
                parentRect.anchoredPosition += new Vector2(0, 75) * Time.deltaTime;
            }
            //弾丸の発射
            if (intervalCount >= interval)
            {
                intervalCount = 0;
                GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet2"), enemyParent);
                newBullet.GetComponent<RectTransform>().anchoredPosition = BasePos() + new Vector2(0, -100);
            }
            //振動(実装したい場合は、振動終了時にmyRect(ベース位置からのズレに当たる)を(0, 0)に戻すのが理想)
            //if (interval - 1.75f < intervalCount && intervalCount < interval)
            //{
            //    float t = intervalCount * 5f / 4.2f;
            //    int r = (int)(1.25 * t * t - 5 * t + 10);
            //    myRect.anchoredPosition += new Vector2(random.Next(r * 2) - r, random.Next(r * 2) - r);
            //}
        }

        //上に退却していくタイプ
        if (parentRect.anchoredPosition.y > 650)
        {
            GameManager.disappearedEnemyCount++;
            Destroy(parentRect.gameObject);
        }
    }
    //private float landing(float start, float goal, float time, float now)
    //{
    //    if (now < 0) return start;
    //    if (now > time) return goal;
    //    if (time == 0 || Mathf.Abs(goal - start) < 0.001f) return goal;
    //    float v = goal - start;

    //    float x = Mathf.Abs(v);

    //    if (x <= 1) return goal;

    //    float v0 = 2 * x / time;
    //    float nx = v0 * now - v0 * v0 / (4 * x) * now * now;

    //    return v * (nx / x) + start;

    //}
}