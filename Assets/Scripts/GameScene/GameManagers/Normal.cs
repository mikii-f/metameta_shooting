using System.Linq;

//GameManagerを継承し、難易度「ふつう」のゲームを管理する
public class Normal : GameManager
{
    protected override void Algorithm()
    {
        if (intervalCount >= interval)
        {
            if (appearedEnemy.Sum() == 3 || appearedEnemy.Sum() == 25)
            {
                GenerateEnemy1_0(-Manager.gameWidth + 50);
                GenerateEnemy1_0(Manager.gameWidth - 50);
            }
            else if (appearedEnemy.Sum() % 9 == 0 && appearedEnemy.Sum() != 0)
            {
                GenerateEnemy1_1(0);
            }
            else if (appearedEnemy.Sum() == 29)
            {
                GenerateEnemy2_0(0);
            }
            else if (appearedEnemy[0] < Manager.enemyComposition[1][0])
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
            interval = UnityEngine.Random.Range(3.5f, 4.5f);
        }
    }
}