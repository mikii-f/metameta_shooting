using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//タイトルシーンにおける動作を管理
public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private RectTransform easyButtonRect;
    [SerializeField] private RectTransform normalButtonRect;
    [SerializeField] private RectTransform hardButtonRect;
    [SerializeField] private GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        tutorialPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        StartCoroutine(Common.FadeIn(black));
    }


    public void GameStart()
    {
        levelSelectPanel.SetActive(true);
    }

    //「遊び方」を押したら遊び方説明パネルを表示する
    public void Tutorial()
    {
        tutorialPanel.SetActive(true);
    }
    public void Ranking()
    {

    }
    //「戻る」またはパネルの外を押したら各種パネルを非表示にする
    public void Close()
    {
        tutorialPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
    }

    public void Easy()
    {
        Manager.level = 0;
        StartCoroutine(Common.Button(easyButtonRect));
        StartCoroutine(SceneChange("EasyScene"));
    }
    public void Normal()
    {
        Manager.level = 1;
        StartCoroutine(Common.Button(normalButtonRect));
        StartCoroutine(SceneChange("NormalScene"));
    }
    public void Hard()
    {
        Manager.level = 2;
        StartCoroutine(Common.Button(hardButtonRect));
        StartCoroutine(SceneChange("HardScene"));
    }
    private IEnumerator SceneChange(string scene)
    {
        yield return StartCoroutine(Common.FadeOut(black));
        SceneManager.LoadScene(scene);
    }
}