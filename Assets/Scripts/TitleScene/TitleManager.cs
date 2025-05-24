using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//タイトルシーンにおける動作を管理
public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject EasyPanel;
    [SerializeField] private GameObject NormalPanel;
    [SerializeField] private GameObject HardPanel;
    [SerializeField] private GameObject black;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Common.FadeIn(black));
    }


    public void GameStart()
    {
        EasyPanel.SetActive(true);
        NormalPanel.SetActive(true);
        HardPanel.SetActive(true);
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
        EasyPanel.SetActive(false);
        NormalPanel.SetActive(false);
        HardPanel.SetActive(false);
    }

    public void Easy()
    {
        SceneManager.LoadScene("EasyScene");
    }
    public void Normal()
    {
         SceneManager.LoadScene("NormalScene");
    }
    public void Hard()
    {
         SceneManager.LoadScene("HardScene");
    }
    private IEnumerator SceneChange(string scene)
    {
        yield return StartCoroutine(Common.FadeOut(black));
        SceneManager.LoadScene(scene);
    }
}