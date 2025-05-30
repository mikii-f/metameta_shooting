using UnityEngine;
using UnityEngine.UI;

//レベル2の敵(ボス級) 行動パターン0
public class Enemy2_0 : Enemy
{
    private float durationCount = 0;
    //個の敵が一時停止するY座標
    private const float stopY = 400;
    private float firstY = 0;
    private bool isShot = false;
    private System.Random random = new System.Random();
    private float firstX = 0;

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 100;
        hp = 100;
        enemyLevel = EnemyLevel.Level2;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        interval = 7;
        intervalCount = 3;

    }
    void Start()
    {
        //初期位置を記録
        firstY = parentRect.anchoredPosition.y;
        firstX = parentRect.anchoredPosition.x;
    }
    // Update is called once per frame
    void Update()
    {
        isBulletCollision = false;
        if (!GameManager.isPause)
        {
            durationCount += Time.deltaTime;
            intervalCount += Time.deltaTime;
            //Enemy1_0.csを参考に実装(ほぼコピペですが、実際の動作を見て数値を調整するなどしてみてください
            
            //生成されて2秒で定位置へ移動
            //その2秒後に弾を発射、さらに1秒停止
            //その後2秒かけて上へ戻る

            if (durationCount < 2.5)
            {
                parentRect.anchoredPosition = new Vector2(firstX, landing(firstY, stopY, 2f, durationCount));
            }
            else 
            {
                parentRect.anchoredPosition = new Vector2(firstX, landing(stopY, firstY, 2f , durationCount-5f));
            }

            //振動。弾を発射する時間に近づくにつれて大きくなる
            if(2.25 < durationCount && durationCount < 4)
            {
                float t = durationCount*5f/4.2f;
                int r = (int)(1.25*t*t-5*t+10);
                parentRect.anchoredPosition += new Vector2(random.Next(r*2)-r,random.Next(r*2)-r);
            }

            if (durationCount>4&&!isShot)
            {
                isShot = true;
                Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet2"), enemyParent).GetComponent<RectTransform>().anchoredPosition = parentRect.anchoredPosition;
            }
            //上に退却していくタイプ
            if (durationCount > 7.2f)
            {
                GameManager.disappearedEnemyCount++;
                Destroy(parentRect.gameObject);
            }
        }
    }
    private float landing(float start, float goal,float time,float now)
    {
        if (now < 0) return start;
        if (now > time) return goal;
        if (time == 0 || Mathf.Abs(goal-start) < 0.001f) return goal;
        float v = goal - start;

        float x = Mathf.Abs(v);

        if (x <= 1) return goal;

        float v0 = 2 * x / time;
        float nx = v0 * now - v0 * v0 / (4 * x) * now * now;

        return v * (nx / x) + start;

    }
}