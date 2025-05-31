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
            GenerateEnemy2_0(0);
            GenerateEnemy0_2(0);
            test = false;
        }
    }
}