using UnityEngine;
using UnityEngine.UI;

//レベル1の敵(強め) 行動パターン2
public class Enemy1_2 : Enemy
{
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 40;
        hp = 40;
        enemyLevel = EnemyLevel.Level1;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        isBulletCollision = false;
        if (!GameManager.isPause)
        {
        }
    }
}