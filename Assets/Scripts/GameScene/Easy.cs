using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManagerを継承し、難易度「やさしい」のゲームを管理する
public class Easy : GameManager
{
    private bool test = true;   //動作確認用 後で消します
    // Update is called once per frame
    void Update()
    {
        if (!isPause)
        {
            duration += Time.deltaTime;
            durationText.text = duration.ToString("F1") + "s";
            if (test)   //動作確認用 後で消します
            {
                test = false;
                GameObject newEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy0_0"), enemyParent);
                newEnemy.GetComponent<RectTransform>().anchoredPosition = new(0, 600);
            }
        }
    }
}