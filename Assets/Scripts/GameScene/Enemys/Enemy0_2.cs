using UnityEngine;
using UnityEngine.UI;

//レベル0の敵(雑魚) 行動パターン2
public class Enemy0_2 : Enemy
{
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 10;
        hp = 10;
        enemyLevel = EnemyLevel.Level0;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPause == false)
        {
        }
    }
}