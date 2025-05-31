using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//レベル0の敵(雑魚) 行動パターン2
public class Enemy0_2 : Enemy
{
    //二次関数の頂点に到達する時間
    private const float vertexTime = 0.8f;
    //二次関数の頂点のY座標
    private const float vertexY = 250f;
    //二次関数の幅(生成された後に右に動く)
    private const float width = 300f;
    //生成されてから経過した時間
    private float time = 0;
    private bool isShot = false;
    private Vector2 startV;

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 10;
        hp = 10;
        enemyLevel = EnemyLevel.Level0;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        startV = parentRect.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        isBulletCollision = false;
        if (GameManager.isPause == false)
        {
            time += Time.deltaTime;
            parentRect.anchoredPosition = new Vector2(
                width / (2 * vertexTime) * time + startV.x,
                landing(startV.y, vertexY, vertexTime, time)
            );
            if (!isShot && time > vertexTime)
            {
                isShot = true;

                float rad0 = Random.Range(-Mathf.PI / 2, Mathf.PI / 2);
                Vector2 coreSpeed = new Vector2(0, -400);
                for (int i = 0; i < 3; i++)
                {
                    GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0_2"), enemyParent);
                    obj.GetComponent<EnemyBullet0_2>().initialization(coreSpeed, rad0 + Mathf.PI * 2 / 3 * i, 50, 1.2f, Mathf.PI * 2 / 3 * i);
                    obj.GetComponent<RectTransform>().anchoredPosition = parentRect.anchoredPosition;
                }
            }
            if (parentRect.anchoredPosition.y > 600)
            {
                GameManager.disappearedEnemyCount++;
                Destroy(parentRect.gameObject);
            }
        }
    }
    private float landing(float start, float goal, float time, float now)
    {
        if (time == 0 || Mathf.Abs(goal - start) < 0.001f) return goal;
        float v = goal - start;

        float x = Mathf.Abs(v);

        float v0 = 2 * x / time;
        float nx = v0 * now - v0 * v0 / (4 * x) * now * now;

        return v * (nx / x) + start;
    }
}