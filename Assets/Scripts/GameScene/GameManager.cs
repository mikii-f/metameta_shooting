using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ゲームシーン全体をまとめ、管理するための親クラス
public abstract class GameManager : MonoBehaviour
{
    [SerializeField] private Text rankingText;
    [SerializeField] private Text guideText;
    [HideInInspector] public int score = 0;     //スコア
    [SerializeField] private Text scoreText;
    protected float duration = 0;               //経過時間
    [SerializeField] protected Text durationText;
    private int life = 3;                       //ライフ
    [SerializeField] private Text lifeText;
    private List<int> destroyedEnemy = new(){0, 0, 0};  //撃破した敵の数(強さ別)
    [HideInInspector] public int skillPoint = 0;        //SP
    [SerializeField] private Slider spSlider;
    public static bool isPause = true;                  //ポーズ中かどうか
    private bool isStart = false;
    [SerializeField] private GameObject black;
    [SerializeField] protected RectTransform enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Common.FadeIn(black));
        DisplayRanking();
    }

    //画面上に各難易度のランキングを表示
    private void DisplayRanking()
    {

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
        yield return new WaitForSeconds(1);
        guideText.text = "2";
        yield return new WaitForSeconds(1);
        guideText.text = "1";
        yield return new WaitForSeconds(1);
        guideText.text = "Go!!";
        yield return new WaitForSeconds(1);
        guideText.text = "";
        isPause = false;
    }
    //自機がダメージを受けた時のライフを減らす処理
    //ライフが0になったらGameOver()を呼び出す

    public void Damaged()
    {

    }
    public void DestroyEnemy(EnemyLevel enemyLevel)
    {

    }
    private void GameOver()
    {

    }
    private void GameClear()
    {

    }
    
}