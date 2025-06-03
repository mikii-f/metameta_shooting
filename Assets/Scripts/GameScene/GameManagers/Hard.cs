using System.Collections;
using System.Linq;
using UnityEngine;

//GameManagerを継承し、難易度「難しい」のゲームを管理する
public class Hard : GameManager
{
    protected override void Algorithm()
    {
        if (intervalCount >= interval)
        {
            if (appearedEnemy.Sum() % 15 == 0)
            {
                GenerateEnemy1_1(0);
            }
            else if (appearedEnemy.Sum() == 12 || appearedEnemy.Sum() == 20 | appearedEnemy.Sum() == 46 || appearedEnemy.Sum() == 57)
            {
                GenerateEnemy1_0(-Manager.gameWidth + 50);
                GenerateEnemy1_0(Manager.gameWidth - 50);
            }
            else if (appearedEnemy.Sum() == 1 || appearedEnemy.Sum() == 25 || appearedEnemy.Sum() == 39 || appearedEnemy.Sum() == 63)
            {
                StartCoroutine(GenerateEnemys());
            }
            else if (appearedEnemy.Sum() == 6 || appearedEnemy.Sum() == 32 || appearedEnemy.Sum() == 67)
            {
                GenerateEnemy2_0(0);
            }
            else if (appearedEnemy[0] < Manager.enemyComposition[2][0])
            {
                if (UnityEngine.Random.Range(0, 10) < 7)
                {
                    GenerateEnemy0_0(UnityEngine.Random.Range(-Manager.gameWidth, Manager.gameWidth));
                }
                else
                {
                    GenerateEnemy0_1(UnityEngine.Random.Range(-Manager.gameWidth + 200, Manager.gameWidth - 200));
                }
            }
            intervalCount = 0;
            interval = UnityEngine.Random.Range(3f, 4f);
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