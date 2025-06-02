using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManagerを継承し、難易度「難しい」のゲームを管理する
public class Hard : GameManager
{
    private bool test = true;   //テスト用
    protected override void Algorithm()
    {
        if (test)
        {
            StartCoroutine(GenerateEnemys());
            test = false;
        }
    }
    private IEnumerator GenerateEnemys()
    {
        GenerateEnemy1_2();
        yield return StartCoroutine(Wait(0.25f));
        GenerateEnemy0_3();
        yield return StartCoroutine(Wait(0.25f));
        GenerateEnemy0_3();
        yield return StartCoroutine(Wait(0.25f));
        GenerateEnemy0_3();
    }
    private IEnumerator Wait(float t)
    {
        float temp = 0;
        while (true)
        {
            if (!GameManager.isPause)
            {
                yield return null;
                temp += Time.deltaTime;
                if (temp >= t)
                {
                    break;
                }
            }
            yield return null;
        }
    }
}