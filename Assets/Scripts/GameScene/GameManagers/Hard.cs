using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManagerを継承し、難易度「難しい」のゲームを管理する
public class Hard : GameManager
{
    private float time = 0; //経過時間
    private int cnt = 0;
    private bool test = true;   //テスト用
    protected override void Algorithm()
    {
        time += Time.deltaTime;

        if (test)
        {
            //0.1秒ごとに敵を生成
            if (1<=time&& time<1.5)
            {
                if (time>=1+cnt*0.1)
                {
                    GenerateEnemy0_2((int)(-200+(time*60-60)*10));
                    cnt++;
                }
            }
        }
    }
}
