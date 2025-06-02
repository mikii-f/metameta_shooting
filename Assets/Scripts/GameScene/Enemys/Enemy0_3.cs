using System;
using UnityEngine;
using UnityEngine.UI;

//レベル0の敵(雑魚) 行動パターン3
public class Enemy0_3 : Enemy
{
    private float t = 0;
    private int right = 1;
    void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        myRect = GetComponent<RectTransform>();
        maxHP = 10;
        hp = 10;
        enemyLevel = EnemyLevel.Level0;
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
        if (GameManager.isPause == false)
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
                    GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0"), enemyParent);
                    newBullet.GetComponent<RectTransform>().anchoredPosition = BasePos() + new Vector2(0, -50);
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