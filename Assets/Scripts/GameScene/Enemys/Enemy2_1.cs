using UnityEngine;
using UnityEngine.UI;

//レベル2の敵(ボス級) 行動パターン0
public class Enemy2_1 : Enemy
{
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        maxHP = 100;
        hp = 100;
        enemyLevel = EnemyLevel.Level2;
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