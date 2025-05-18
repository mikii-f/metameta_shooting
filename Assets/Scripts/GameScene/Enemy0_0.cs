using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//レベル0の敵(雑魚)
public class Enemy0_0 : Enemy
{
    void Awake()
    {
        parentRect = GetComponent<RectTransform>();
        myRect = images.GetComponent<RectTransform>();
        hp = 10;
        enemyLevel = EnemyLevel.Level0;
        myImages = images.GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0"), enemyParent);
        newBullet.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp = parentRect.anchoredPosition + new Vector2(0, -100 * Time.deltaTime);
        parentRect.anchoredPosition = temp;
    }
}