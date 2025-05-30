using UnityEngine;
using UnityEngine.UI;

//レベル2の敵(ボス級) 行動パターン0
public class Enemy2_0 : Enemy
{
    private int duration = 40;
    private float durationCount = 0;
    private bool back = false;
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

    // Update is called once per frame
    void Update()
    {
        isBulletCollision = false;
        if (!GameManager.isPause)
        {
            durationCount += Time.deltaTime;
            intervalCount += Time.deltaTime;
            //Enemy1_0.csを参考に実装(ほぼコピペですが、実際の動作を見て数値を調整するなどしてみてください)

        }

        //上に退却していくタイプ
        if (parentRect.anchoredPosition.y > 650)
        {
            GameManager.disappearedEnemyCount++;
            Destroy(parentRect.gameObject);
        }
    }
}