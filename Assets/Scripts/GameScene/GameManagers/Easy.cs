using System.Linq;

//GameManagerを継承し、難易度「やさしい」のゲームを管理する
public class Easy : GameManager
{
    protected override void Algorithm()
    {
        if (intervalCount >= interval)
        {
            if (appearedEnemy.Sum() == 8 || appearedEnemy.Sum() == 16)
            {
                GenerateEnemy1_0(UnityEngine.Random.Range(-Manager.gameWidth + 50, Manager.gameWidth - 50));
            }
            else if (appearedEnemy[0] < Manager.enemyComposition[0][0])
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
            interval = UnityEngine.Random.Range(4f, 5f);
        }
    }
}