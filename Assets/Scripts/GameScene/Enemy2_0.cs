using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//レベル2の敵(ボス級？)
public class Enemy2_0 : Enemy
{
    void Awake()
    {
        parentRect = GetComponent<RectTransform>();
        myRect = images.GetComponent<RectTransform>();
        hp = 50;
        enemyLevel = EnemyLevel.Level2;
        myImages = images.GetComponentsInChildren<Image>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}