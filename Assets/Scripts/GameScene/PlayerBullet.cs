using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自機の弾丸の動作を管理する
public class PlayerBullet : MonoBehaviour
{
    private RectTransform myRect;   //自身の座標
    public int speed = 3000;

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}