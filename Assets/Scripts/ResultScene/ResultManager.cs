using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//リザルトシーンにおける動作を管理する
public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text rankingText;
    private List<int> ranking;
    [SerializeField] private RectTransform titleButtonRect;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text durationText;
    [SerializeField] private Text enemysText;
    [SerializeField] private GameObject black;
    [SerializeField] private Text rankinText;

    // Start is called before the first frame update
    void Start()
    {
        black.SetActive(false);
        //スコア、生存時間、敵撃破数のテキストを更新
        scoreText.text = Manager.result.score.ToString();
        durationText.text = Manager.result.duration.ToString("F2") + "s";
        string enemy_1 = Manager.result.destroyedEnemy[0].ToString();
        string enemy_2 = Manager.result.destroyedEnemy[1].ToString();
        string enemy_3 = Manager.result.destroyedEnemy[2].ToString();
        enemysText.text = "L0:" + enemy_1 + "/" + Manager.enemyComposition[Manager.level][0].ToString();
        enemysText.text += "  L1:" + enemy_2 + "/" + Manager.enemyComposition[Manager.level][1].ToString();
        enemysText.text += "  L2:" + enemy_3 + "/" + Manager.enemyComposition[Manager.level][2].ToString();
        UpdateRanking();
    }

    private void UpdateRanking()
    {
        ranking = Manager.PastScores()[Manager.level];
        if (ranking[4] < Manager.result.score)
        {
            if (Manager.result.score == Manager.BestScore())
            {
                rankinText.text = "ベストスコア!";
            }
            else if (ranking[0] < Manager.result.score)
            {
                rankinText.text = "ハイスコア更新!";
            }
            else
            {
                rankinText.text = "ランクイン!";
            }
            ranking.Add(Manager.result.score);
            ranking.Sort((a, b) => b - a);
            ranking.RemoveAt(5);
            rankingText.text = "";
            for (int i = 0; i < 5; i++)
            {
                rankingText.text += ranking[i] + "\n";
            }
            Manager.Save(ranking);
            scoreText.color = Color.yellow;
        }
        else
        {
            rankingText.text = "";
            for (int i = 0; i < 5; i++)
            {
                rankingText.text += ranking[i] + "\n";
            }
        }
    }

    //「タイトルへ」をクリックしたときにタイトルシーンへ遷移
    public void Title()
    {
        StartCoroutine(Common.Button(titleButtonRect));
        StartCoroutine(GoTitle());
    }
    private IEnumerator GoTitle()
    {
        yield return StartCoroutine(Common.FadeOut(black));
        SceneManager.LoadScene("TitleScene");
    }
}