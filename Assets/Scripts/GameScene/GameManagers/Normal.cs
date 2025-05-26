using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManagerを継承し、難易度「ふつう」のゲームを管理する
public class Normal : GameManager
{
    private bool test = true;   //テスト用
    protected override void Algorithm()
    {
        if (test)
        {
            GenerateEnemy1_1(0);
            test = false;
        }
    }
}