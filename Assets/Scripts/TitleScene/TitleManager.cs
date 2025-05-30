using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//タイトルシーンにおける動作を管理
public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private RectTransform easyButtonRect;
    [SerializeField] private RectTransform normalButtonRect;
    [SerializeField] private RectTransform hardButtonRect;
    [SerializeField] private GameObject black;
    [SerializeField] private GameObject rankingPanel;
    [SerializeField] private Text ranking1;
    [SerializeField] private Text ranking2;
    [SerializeField] private Text ranking3;
    private AudioSource audioSource;
    [SerializeField] private AudioClip system1;
    [SerializeField] private AudioClip system2;
    [SerializeField] private AudioSource bgmSource;
    // Start is called before the first frame update
    void Start()
    {
        tutorialPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        rankingPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        List<List<int>> ranking = Manager.PastScores();
        for (int i=0; i<5; i++)
        {
            ranking1.text += ranking[0][i] + "\n";
            ranking2.text += ranking[1][i] + "\n";
            ranking3.text += ranking[2][i] + "\n";
        }
        StartCoroutine(Common.FadeIn(black));
    }


    public void GameStart()
    {
        levelSelectPanel.SetActive(true);
        audioSource.clip = system1;
        audioSource.Play();
    }

    //「遊び方」を押したら遊び方説明パネルを表示する
    public void Tutorial()
    {
        tutorialPanel.SetActive(true);
        audioSource.clip = system1;
        audioSource.Play();
    }
    public void Ranking()
    {
        rankingPanel.SetActive(true);
        audioSource.clip = system1;
        audioSource.Play();
    }
    //「戻る」またはパネルの外を押したら各種パネルを非表示にする
    public void Close()
    {
        tutorialPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        rankingPanel.SetActive(false);
        audioSource.clip = system2;
        audioSource.Play();
    }

    public void Easy()
    {
        Manager.level = 0;
        StartCoroutine(Common.Button(easyButtonRect));
        StartCoroutine(SceneChange("EasyScene"));
        audioSource.clip = system1;
        audioSource.Play();
    }
    public void Normal()
    {
        Manager.level = 1;
        StartCoroutine(Common.Button(normalButtonRect));
        StartCoroutine(SceneChange("NormalScene"));
        audioSource.clip = system1;
        audioSource.Play();
    }
    public void Hard()
    {
        Manager.level = 2;
        StartCoroutine(Common.Button(hardButtonRect));
        StartCoroutine(SceneChange("HardScene"));
        audioSource.clip = system1;
        audioSource.Play();
    }
    private IEnumerator SceneChange(string scene)
    {
        StartCoroutine(BGMFade());
        yield return StartCoroutine(Common.FadeOut(black));
        SceneManager.LoadScene(scene);
    }
    private IEnumerator BGMFade()
    {
        while (bgmSource.volume > 0)
        {
            bgmSource.volume -= Time.deltaTime;
            yield return null;
        }
    }
}