using UnityEngine;
using UnityEngine.UI;

//レベル0の敵(雑魚) 行動パターン0
public class Enemy0_0 : Enemy
{
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 10;
        hp = 10;
        enemyLevel = EnemyLevel.Level0;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        interval = 3;
        intervalCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPause == false)
        {
            intervalCount += Time.deltaTime;
            Vector2 temp = parentRect.anchoredPosition + new Vector2(0, -100 * Time.deltaTime);
            parentRect.anchoredPosition = temp;
            if (intervalCount >= interval)
            {
                intervalCount = 0;
                GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0"), enemyParent);
                newBullet.GetComponent<RectTransform>().anchoredPosition = BasePos() + new Vector2(0, -50);
            }
        }
        if (BasePos().y < -600)
        {
            GameManager.disappearedEnemyCount++;
            Destroy(parentRect.gameObject);
        }
    }
}