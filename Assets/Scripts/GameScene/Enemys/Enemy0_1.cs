using UnityEngine;
using UnityEngine.UI;

//レベル0の敵(雑魚) 行動パターン1
public class Enemy0_1 : Enemy
{
    private int width = 200;
    private int right = 1;
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 10;
        hp = 10;
        enemyLevel = EnemyLevel.Level0;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        interval = 5;
        intervalCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPause == false)
        {
            intervalCount += Time.deltaTime;
            //下方向への移動
            parentRect.anchoredPosition += new Vector2(0, -50 * Time.deltaTime);
            //横方向への往復移動
            Vector2 temp = myRect.anchoredPosition;
            temp.x += width * 2 * Time.deltaTime * right;
            if (temp.x >= width)
            {
                right = -1;
            }
            else if (temp.x <= -width)
            {
                right = 1;
            }
            myRect.anchoredPosition = temp;
            //3方向に撃つ
            if (intervalCount >= interval)
            {
                intervalCount = 0;
                GameObject newBullet1 = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0"), enemyParent);
                newBullet1.GetComponent<RectTransform>().anchoredPosition = BasePos() + new Vector2(0, -50);
                GameObject newBullet2 = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0"), enemyParent);
                newBullet2.GetComponent<RectTransform>().anchoredPosition = BasePos() + new Vector2(0, -50);
                newBullet2.GetComponent<EnemyBullet0>().radian = Mathf.PI / 3;
                GameObject newBullet3 = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0"), enemyParent);
                newBullet3.GetComponent<RectTransform>().anchoredPosition = BasePos() + new Vector2(0, -50);
                newBullet3.GetComponent<EnemyBullet0>().radian = Mathf.PI * 2 / 3;
            }
        }
        if (BasePos().y < -600)
        {
            GameManager.disappearedEnemyCount++;
            Destroy(parentRect.gameObject);
        }
    }
}