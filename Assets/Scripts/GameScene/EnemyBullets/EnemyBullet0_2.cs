using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵の弾丸の動作を管理する
//Enemy0_2専用
public class EnemyBullet0_2 : MonoBehaviour
{
    //復元力の座標
    private Vector2 core;
    //復元力の速度
    private Vector2 coreSpeed;

    //弾の初期速度
    private Vector2 bulletSpeed;

    //復元力の係数
    private float k = 0.01f;

    private RectTransform myRect;
    //初期位相によるずれ
    private Vector2 pos;


    //復元力を持つコアが下向きに発射されることを想定
    //radは弾丸の初期のコアの進行方向に対する角度、ampは振幅、periodは周期、theta0は初期位相
    public void initialization(Vector2 coreSpeed, float rad, float amp, float period, float theta0)
    {
        this.coreSpeed = coreSpeed;
        k = (Mathf.PI * 2 / period) * (Mathf.PI * 2 / period);
        float s = Mathf.Sqrt(k) * amp * Mathf.Cos(theta0);
        this.bulletSpeed = new Vector2(s * Mathf.Cos(rad), s * Mathf.Sin(rad)) + coreSpeed;
        this.pos = new Vector2(amp * Mathf.Cos(theta0) * Mathf.Cos(rad), amp * Mathf.Sin(theta0) * Mathf.Sin(rad));
    }
    // Start is called before the first frame update
    void Start()
    {
        this.core = GetComponent<RectTransform>().anchoredPosition;
        myRect = GetComponent<RectTransform>();
        myRect.anchoredPosition += pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (myRect.anchoredPosition.y < -560)
        {
            Destroy(gameObject);
        }
        if (!GameManager.isPause)
        {
            float t = Time.deltaTime;
            core += coreSpeed * t;
            //復元力の計算
            Vector2 force = (core - myRect.anchoredPosition) * k;

            //復元力を速度に加える
            bulletSpeed += force * t;
            //速度を位置に加える
            myRect.anchoredPosition += bulletSpeed * t;
        }
    }
}