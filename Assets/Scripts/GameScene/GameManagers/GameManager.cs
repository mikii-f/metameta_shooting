using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//ゲームシーン全体をまとめ、管理するための親クラス
public abstract class GameManager : MonoBehaviour
{
    private int gameLevel = 0;
    [SerializeField] private Text rankingText;
    private List<int> ranking;
    [SerializeField] private Text guideText;
    public GameObject finishButton;
    [SerializeField] private GameObject finishText;
    [SerializeField] private GameObject pauseButton;
    private int score = 0;     //スコア
    [SerializeField] private Text scoreText;
    [SerializeField] private RectTransform scoreParent;
    protected float duration = 0;               //経過時間
    [SerializeField] protected Text durationText;
    private int life = 10;                       //ライフ
    [SerializeField] private Text lifeText;
    private List<int> destroyedEnemy = new(){0, 0, 0};  //撃破した敵の数(強さ別)
    [SerializeField] protected Text l0EnemyText;
    [SerializeField] protected Text l1EnemyText;
    [SerializeField] protected Text l2EnemyText;
    [HideInInspector] public int skillPoint = 50;       //SP
    [SerializeField] private Slider spSlider;
    [SerializeField] private Image skill1Image;
    [SerializeField] private Image skill1Image2;
    [SerializeField] private Image skill2Image;
    [SerializeField] private Image skill2Image2;
    [SerializeField] private Image skill3Image;
    [SerializeField] private Image skill3Image2;
    [HideInInspector] public bool isSkill1 = false;
    private float skill1Count = 0;
    [SerializeField] private Text skill1CountText;
    [HideInInspector] public bool isSkill2 = false;
    private float skill2Count = 0;
    [SerializeField] private Text skill2CountText;
    public static bool isSkill3 = false;
    private float skill3Count = 0;
    [SerializeField] private Text skill3CountText;
    public static bool isPause = true;                  //ポーズ中かどうか
    [SerializeField] private GameObject pausePanel;
    private bool isStart = false;
    [SerializeField] private GameObject black;
    [SerializeField] protected RectTransform enemyParent;
    [SerializeField] private RectTransform metaParent;
    protected float interval = 1;
    protected float intervalCount = 0;
    protected List<int> appearedEnemy = new(){0, 0, 0}; //発生した敵の数(強さ別)
    public static int disappearedEnemyCount = 0;        //自然消滅も含めて、消えたエネミーの数の合計
    private readonly List<string> metas = new List<string>() {"メ夕", "メク", "〆タ", "〆夕", "〆ク", "Xタ", "X夕", "Xク"};
    private AudioSource audioSource;
    [SerializeField] private AudioClip system1;
    [SerializeField] private AudioClip system2;
    [SerializeField] private AudioClip countDown;
    [SerializeField] private AudioClip whistle;
    [SerializeField] private AudioClip skill;
    [SerializeField] private AudioClip cymbal;
    [SerializeField] private AudioClip destroyEnemy;
    [SerializeField] private AudioSource bgmSource;

    // Start is called before the first frame update
    void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case ("EasyScene"):
                gameLevel = 0;
                break;
            case ("NormalScene"):
                gameLevel = 1;
                break;
            case ("HardScene"):
                gameLevel = 2;
                break;
            default:
                break;
        }
        isPause = true;
        isSkill3 = false;
        disappearedEnemyCount = 0;
        finishButton.SetActive(false);
        finishText.SetActive(false);
        pausePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        DisplayRanking();
        lifeText.text = "×" + life.ToString();
        skill1Image.color = new(0.5f, 100f/255, 0, 1); skill2Image.color = new(0.5f, 100f / 255, 0, 1); skill3Image.color = new(0.5f, 100f / 255, 0, 1);
        skill1Image2.color = Color.gray; skill2Image2.color = Color.gray; skill3Image2.color = Color.gray;
        L0EnemyText(0); L1EnemyText(0); L2EnemyText(0);
        spSlider.value = skillPoint;
        StartCoroutine(Common.FadeIn(black));
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
    }
    void Update()
    {
        if (!isPause)
        {
            //どの難易度でも共通の処理
            if (!finishButton.activeSelf)
            {
                intervalCount += Time.deltaTime;
                duration += Time.deltaTime;
                durationText.text = duration.ToString("F2") + "s";
                if (disappearedEnemyCount == Manager.enemyComposition[gameLevel].Sum())
                {
                    GameClear();
                }
            }
            if (Input.GetKeyDown(KeyCode.P) && pauseButton.activeSelf)
            {
                Pause();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Skill1();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Skill2();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Skill3();
            }
            if (isSkill1)
            {
                skill1Count -= Time.deltaTime;
                skill1CountText.text = skill1Count.ToString("F2");
                if (skill1Count <= 0)
                {
                    isSkill1 = false;
                    skill1CountText.text = "";
                    SPManage();
                }
            }
            if (isSkill2)
            {
                skill2Count -= Time.deltaTime;
                skill2CountText.text = skill2Count.ToString("F2");
                if (skill2Count <= 0)
                {
                    isSkill2 = false;
                    skill2CountText.text = "";
                    SPManage();
                }
            }
            if (isSkill3)
            {
                skill3Count -= Time.deltaTime;
                skill3CountText.text = skill3Count.ToString("F2");
                if (skill3Count <= 0)
                {
                    isSkill3 = false;
                    skill3CountText.text = "";
                    SPManage();
                }
            }
            //難易度ごとの敵発生アルゴリズム
            Algorithm();
        }
    }
    //画面上に各難易度のランキングを表示
    private void DisplayRanking()
    {
        ranking = Manager.PastScores()[Manager.level];
        rankingText.text = "";
        for (int i=0; i<5; i++)
        {
            rankingText.text += ranking[i].ToString() + "\n";
        }
    }
    //クリックしてゲーム開始
    public void GameStart()
    {
        if (!isStart)
        {
            isStart = true;
            StartCoroutine(Go());
        }
    }
    private IEnumerator Go()
    {
        guideText.text = "3";
        audioSource.clip = countDown;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        guideText.text = "2";
        audioSource.clip = countDown;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        guideText.text = "1";
        audioSource.clip = countDown;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        guideText.text = "Go!!";
        audioSource.clip = whistle;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        guideText.text = "";
        isPause = false;
        bgmSource.Play();
    }
    //自機がダメージを受けた時のライフを減らす処理
    //ライフが0になったらGameOver()を呼び出す

    public int Damaged()
    {
        if (!finishButton.activeSelf)
        {
            life--;
            lifeText.text = "×" + life.ToString();
            if (life == 0)
            {
                GameOver();
                return 1;
            }
            return 0;
        }
        return 0;
    }
    public void ScoreChange(int s)
    {
        score += s;
        scoreText.text = score.ToString();
        GameObject scoreChangeText = Instantiate(Resources.Load<GameObject>("Prefabs/ScoreChangeText"), scoreParent);
        scoreChangeText.GetComponent<RectTransform>().anchoredPosition = new(0, 70);
        scoreChangeText.GetComponent<Text>().text = s.ToString();
        if (score > ranking[0])
        {
            scoreText.color = Color.yellow;
        }
        else
        {
            scoreText.color = Color.white;
        }
    }
    public void Skill1()
    {
        if (!isPause && !isSkill1 && skillPoint >= 100)
        {
            StartCoroutine(Common.Button(skill1Image.rectTransform));
            isSkill1 = true;
            skill1Count = 15;
            skill1CountText.text = "10.00";
            skillPoint -= 100;
            skill1Image.color = new(0.5f, 100f / 255, 0, 1);
            skill1Image2.color = Color.gray;
            SPManage();
            audioSource.clip = skill;
            audioSource.Play();
        }
    }
    public void Skill2()
    {
        if (!isPause && !isSkill2 && skillPoint >= 100)
        {
            StartCoroutine(Common.Button(skill2Image.rectTransform));
            isSkill2 = true;
            skill2Count = 15;
            skill2CountText.text = "10.00";
            skillPoint -= 100;
            skill2Image.color = new(0.5f, 100f / 255, 0, 1);
            skill2Image2.color = Color.gray;
            SPManage();
            audioSource.clip = skill;
            audioSource.Play();
        }
    }
    public void Skill3()
    {
        if (!isPause && !isSkill3 && skillPoint >= 100)
        {
            StartCoroutine(Common.Button(skill3Image.rectTransform));
            isSkill3 = true;
            skill3Count = 15;
            skill3CountText.text = "10.00";
            skillPoint -= 100;
            skill3Image.color = new(0.5f, 100f / 255, 0, 1);
            skill3Image2.color = Color.gray;
            SPManage();
            audioSource.clip = skill;
            audioSource.Play();
        }

    }
    //SP管理
    public void SPManage()
    {
        spSlider.value = skillPoint;
        if (skillPoint >= 100)
        {
            if (!isSkill1)
            {
                skill1Image.color = new(1, 200f / 255, 0, 1);
                skill1Image2.color = Color.white;
            }
            if (!isSkill2)
            {
                skill2Image.color = new(1, 200f / 255, 0, 1);
                skill2Image2.color = Color.white;
            }
            if (!isSkill3)
            {
                skill3Image.color = new(1, 200f / 255, 0, 1);
                skill3Image2.color = Color.white;
            }
        }
        else
        {
            skill1Image.color = new(0.5f, 100f / 255, 0, 1); skill2Image.color = new(0.5f, 100f / 255, 0, 1); skill3Image.color = new(0.5f, 100f / 255, 0, 1);
            skill1Image2.color = Color.gray; skill2Image2.color = Color.gray; skill3Image2.color = Color.gray;
        }
    }

    //敵機が撃破されたことを受け取る
    public void DestroyEnemy(EnemyLevel enemyLevel)
    {
        disappearedEnemyCount++;
        if (disappearedEnemyCount != Manager.enemyComposition[gameLevel].Sum())
        {
            audioSource.clip = destroyEnemy;
            audioSource.Play();
        }
        switch (enemyLevel)
        {
            case EnemyLevel.Level0:
                destroyedEnemy[0]++;
                L0EnemyText(destroyedEnemy[0]);
                skillPoint = Mathf.Min(skillPoint + 10, 200);
                spSlider.value = skillPoint;
                SPManage();
                StartCoroutine(DropMeta0());
                break;
            case EnemyLevel.Level1:
                destroyedEnemy[1]++;
                L1EnemyText(destroyedEnemy[1]);
                skillPoint = Mathf.Min(skillPoint + 25, 200);
                spSlider.value = skillPoint;
                SPManage();
                StartCoroutine(DropMeta1());
                audioSource.clip = destroyEnemy;
                audioSource.Play();
                break;
            case EnemyLevel.Level2:
                destroyedEnemy[2]++;
                L2EnemyText(destroyedEnemy[2]);
                skillPoint = Mathf.Min(skillPoint + 50, 200);
                spSlider.value = skillPoint;
                SPManage();
                StartCoroutine(DropMeta2());
                audioSource.clip = destroyEnemy;
                audioSource.Play();
                break;
            default:
                break;
        }
    }
    //レベル0の敵を倒した時は正解と不正解が1個ずつ落ちてくる
    private IEnumerator DropMeta0()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i=0; i<2; i++)
        {
            GameObject meta = Instantiate(Resources.Load<GameObject>("Prefabs/Meta"), metaParent);
            meta.GetComponent<RectTransform>().anchoredPosition = new(UnityEngine.Random.Range(-250, 251), 600);
            if (i % 2 == 1)
            {
                //奇数番目は「メタ」以外の文字に書き換え
                meta.transform.GetChild(0).GetComponent<Text>().text = metas[UnityEngine.Random.Range(0, metas.Count)];
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    //レベル1の敵を倒した時は正解と不正解が2個ずつ落ちてくる それぞれ高得点な黄文字と小文字となっている
    private IEnumerator DropMeta1()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 4; i++)
        {
            GameObject meta = Instantiate(Resources.Load<GameObject>("Prefabs/Meta"), metaParent);
            meta.GetComponent<RectTransform>().anchoredPosition = new(UnityEngine.Random.Range(-250, 251), 600);
            Text t = meta.transform.GetChild(0).GetComponent<Text>();
            if (i % 2 == 1)
            {
                //奇数番目は「メタ」以外の文字に書き換え
                t.text = metas[UnityEngine.Random.Range(0, metas.Count)];
            }
            //黄(オレンジ)文字
            if (i < 2)
            {
                t.color = new(1, 1, 0, 1);
            }
            //小文字
            else
            {
                t.rectTransform.localScale = new(0.7f, 0.7f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    //レベル2の敵を倒した時は正解と不正解が4個ずつ落ちてくる 黄文字と小文字が2個ずつ含まれる(スキル3を使う前提の量)
    private IEnumerator DropMeta2()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 8; i++)
        {
            GameObject meta = Instantiate(Resources.Load<GameObject>("Prefabs/Meta"), metaParent);
            meta.GetComponent<RectTransform>().anchoredPosition = new(UnityEngine.Random.Range(-250, 251), 600);
            Text t = meta.transform.GetChild(0).GetComponent<Text>();
            if (i % 2 == 1)
            {
                //奇数番目は「メタ」以外の文字に書き換え
                t.text = metas[UnityEngine.Random.Range(0, metas.Count)];
            }
            //黄(オレンジ)文字
            if (i % 4 < 2)
            {
                t.color = new(1, 1, 0, 1);
            }
            //小文字
            else
            {
                t.rectTransform.localScale = new(0.7f, 0.7f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void GameOver()
    {
        guideText.text = "GameOver!!";
        guideText.color = Color.red;
        isPause = true;
        StartCoroutine(Result(3));
    }
    private void GameClear()
    {
        guideText.text = "No Enemies Remain";
        guideText.color = new(1, 1, 0, 0.5f);
        finishText.SetActive(true);
        finishButton.SetActive(true);
        pauseButton.SetActive(false);
        audioSource.clip = cymbal;
        audioSource.Play();
    }
    public void Finish()
    {
        StartCoroutine(Common.Button(finishButton.GetComponent<RectTransform>()));
        StartCoroutine(Result(0));
        audioSource.clip = system1;
        audioSource.Play();
    }
    private void SaveResult()
    {
        Manager.Result result = new();
        result.score = score;
        result.duration = duration;
        result.destroyedEnemy = destroyedEnemy;
        Manager.result = result;
    }
    private IEnumerator Result(int t)
    {
        SaveResult();
        yield return new WaitForSeconds(t);
        StartCoroutine(BGMFade());
        yield return StartCoroutine(Common.FadeOut(black));
        SceneManager.LoadScene("ResultScene");
    }
    //ポーズ
    public void Pause()
    {
        if (!isPause)
        {
            pausePanel.SetActive(true);
            isPause = true;
            audioSource.clip = system1;
            audioSource.Play();
        }
    }
    //ポーズからゲームに戻る
    public void Back()
    {
        pausePanel.SetActive(false);
        isPause = false;
        audioSource.clip = system2;
        audioSource.Play();
    }
    //最初からやり直す
    public void Restart()
    {
        audioSource.clip = system1;
        audioSource.Play();
        StartCoroutine(Res());
    }
    private IEnumerator Res()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //タイトルに戻る
    public void Title()
    {
        audioSource.clip = system1;
        audioSource.Play();
        StartCoroutine (Tit());
    }
    private IEnumerator Tit()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("TitleScene");
    }
    private void L0EnemyText(int x)
    {
        l0EnemyText.text = x.ToString() + "/" + Manager.enemyComposition[gameLevel][0].ToString();
    }
    private void L1EnemyText(int x)
    {
        l1EnemyText.text = x.ToString() + "/" + Manager.enemyComposition[gameLevel][1].ToString();
    }
    private void L2EnemyText(int x)
    {
        l2EnemyText.text = x.ToString() + "/" + Manager.enemyComposition[gameLevel][2].ToString();
    }
    protected abstract void Algorithm();
    protected void GenerateEnemy0_0(int x)
    {
        GameObject newEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy0_0"), enemyParent);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = new(x, 600);
        appearedEnemy[0]++;
    }
    protected void GenerateEnemy0_1(int x)
    {
        GameObject newEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy0_1"), enemyParent);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = new(x, 600);
        appearedEnemy[0]++;
    }
    protected void GenerateEnemy0_2(int x)
    {
        GameObject newEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy0_2"), enemyParent);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = new(x, 600);
        appearedEnemy[0]++;
    }
    protected void GenerateEnemy1_0(int x)
    {
        GameObject newEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy1_0"), enemyParent);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = new(x, 650);
        appearedEnemy[1]++;
    }
    protected void GenerateEnemy1_1(int x)
    {
        GameObject newEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy1_1"), enemyParent);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = new(x, 650);
        appearedEnemy[1]++;
    }
    protected void GenerateEnemy2_0(int x)
    {
        GameObject newEnemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemy2_0"), enemyParent);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = new(x, 650);
        appearedEnemy[2]++;
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