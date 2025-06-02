using UnityEngine;
using UnityEngine.UI;

//レベル1の敵(強め) 行動パターン0
public class Enemy1_0 : Enemy
{
    private int duration = 20;
    private float durationCount = 0;
    private bool back = false;
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 40;
        hp = 40;
        enemyLevel = EnemyLevel.Level1;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        interval = 5;
        intervalCount = 4;
    }

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
            if (!back && parentRect.anchoredPosition.y > 450)
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
                GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet1"), enemyParent);
                newBullet.GetComponent<RectTransform>().anchoredPosition = BasePos() + new Vector2(0, -100);
            }
            //上に退却していくタイプ
            if (parentRect.anchoredPosition.y > 650)
            {
                GameManager.disappearedEnemyCount++;
                Destroy(parentRect.gameObject);
            }
        }
    }
}