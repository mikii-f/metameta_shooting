using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

//シーンを跨いでのデータ管理
public class Manager : MonoBehaviour
{
    public static int level; //選択した難易度
    public static List<List<int>> enemyComposition = new(){new(){20, 2, 0}, new(){30, 8, 1}, new(){50, 15, 3}}; //各難易度の敵構成(例)
    public static Result result; //その回のゲームのプレイ結果

    //ゲームの結果として使用する情報をまとめて保持する
    public class Result
    {
        public int score;       //スコア
        public int duration;    //生存時間
        public List<int> destroyedEnemy;    //撃破した敵の数(強さ別)
    }

    //過去のスコアの取得
    public static List<List<int>> PastScores()
    {
        //初回起動の場合は仮のデータを作成して保存
        if (PlayerPrefs.GetInt("dataExist") == 0)
        {
            PlayerPrefs.SetString("easyScore", JsonConvert.SerializeObject(new List<int>() { 0, 0, 0, 0, 0 }));
            PlayerPrefs.SetString("normalScore", JsonConvert.SerializeObject(new List<int>() { 0, 0, 0, 0, 0 }));
            PlayerPrefs.SetString("hardScore", JsonConvert.SerializeObject(new List<int>() { 0, 0, 0, 0, 0 }));
            PlayerPrefs.SetInt("dataExist", 1);
        }
        //難易度「やさしい」「ふつう」「難しい」それぞれのランキングを取得
        List<List<int>> pastScores = new();
        pastScores.Add(JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("easyScore")));
        pastScores.Add(JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("normalScore")));
        pastScores.Add(JsonConvert.DeserializeObject<List<int>>(PlayerPrefs.GetString("hardScore")));
        return pastScores;
    }
    //更新されたランキングを保存
    public static void Save(int level, List<int> scores)
    {
        switch (level)
        {
            case 0:
                PlayerPrefs.SetString("easyScore", JsonConvert.SerializeObject(scores));
                break;
            case 1:
                PlayerPrefs.SetString("normalScore", JsonConvert.SerializeObject(scores));
                break;
            case 2:
                PlayerPrefs.SetString("hardScore", JsonConvert.SerializeObject(scores));
                break;
            default:
                break;
        }
    }
}