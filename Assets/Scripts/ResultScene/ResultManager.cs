using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//リザルトシーンにおける動作を管理する
public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text rankingText;
    [SerializeField] private RectTransform titleButtonRect;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text durationText;
    [SerializeField] private Text enemysText;
    [SerializeField] private GameObject black;
    
    // Start is called before the first frame update
    void Start()
    {
        black.SetActive(false);
        //スコア、生存時間、敵撃破数のテキストを更新

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