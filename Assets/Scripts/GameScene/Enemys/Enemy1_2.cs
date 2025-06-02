using System;
using UnityEngine;
using UnityEngine.UI;

//レベル1の敵(強め) 行動パターン2
public class Enemy1_2 : Enemy
{
    private float t = 0;
    private int right = 1;
    void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        myRect = GetComponent<RectTransform>();
        maxHP = 40;
        hp = 40;
        enemyLevel = EnemyLevel.Level1;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        interval = 2.5f;
        intervalCount = UnityEngine.Random.Range(0.5f, 2);
    }

    // Update is called once per frame
    void Update()
    {
        isBulletCollision = false;
        if (!GameManager.isPause)
        {
            t += Time.deltaTime;
            intervalCount += Time.deltaTime;
            //単振動
            myRect.anchoredPosition = new(0, 30 * Mathf.Sin(3 * Mathf.PI * t));
            //レーン切り替え
            if (parentRect.anchoredPosition.x > 500)
            {
                parentRect.anchoredPosition = new(499, parentRect.anchoredPosition.y - 100);
                right = -1;
            }
            else if (parentRect.anchoredPosition.x < -500)
            {
                parentRect.anchoredPosition = new(-499, parentRect.anchoredPosition.y - 100);
                right = 1;
            }
            //横移動
            else
            {
                parentRect.anchoredPosition += new Vector2(right * 250 * Time.deltaTime, 0);
            }
            //弾丸の発射
            if (intervalCount >= interval)
            {
                intervalCount = 0;
                if (-Manager.gameWidth < parentRect.anchoredPosition.x && parentRect.anchoredPosition.x < Manager.gameWidth)
                {
                    float rad0 = UnityEngine.Random.Range(-Mathf.PI / 2, Mathf.PI / 2);
                    Vector2 coreSpeed = new Vector2(0, -400);
                    for (int i = 0; i < 3; i++)
                    {
                        GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0_2"), enemyParent);
                        obj.GetComponent<EnemyBullet0_2>().initialization(coreSpeed, rad0 + Mathf.PI * 2 / 3 * i, 50, 1.2f, Mathf.PI * 2 / 3 * i);
                        obj.GetComponent<RectTransform>().anchoredPosition = parentRect.anchoredPosition;
                    }
                }
            }
            if (parentRect.anchoredPosition.y < -499)
            {
                GameManager.disappearedEnemyCount++;
                Destroy(parentRect.gameObject);
            }
        }
    }
}