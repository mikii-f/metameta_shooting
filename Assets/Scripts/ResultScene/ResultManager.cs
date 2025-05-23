using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//リザルトシーンにおける動作を管理する
public class ResultManager : MonoBehaviour
{
    [SerializeField] private Text rankingText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //「タイトルへ」をクリックしたときにタイトルシーンへ遷移
    public void Title()
    {
        SceneManager.LoadScene("TitleScene");
    }
}