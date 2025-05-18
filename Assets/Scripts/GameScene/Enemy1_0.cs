using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//レベル1の敵(強め)
public class Enemy1_0 : Enemy
{
    void Awake()
    {
        parentRect = GetComponent<RectTransform>();
        myRect = images.GetComponent<RectTransform>();
        hp = 25;
        enemyLevel = EnemyLevel.Level1;
        myImages = images.GetComponentsInChildren<Image>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}