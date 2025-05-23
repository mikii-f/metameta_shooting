using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

//自機の弾丸の動作を管理する
public class PlayerBullet : MonoBehaviour
{
    private RectTransform myRect;   //自身の座標
    public const float nSpeed = 3000;
    public float time=0;
    private int ttemp;
    private static int temp = 0;

    PlayerBullet()
    {
        temp++;
        ttemp = temp;
    }

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPause)
        {
            move();
        }
    }
    private void move()
    {
        //moveNormal(Mathf.PI/2);
        accelation(Mathf.PI/2);

        if (myRect.anchoredPosition.y > 2000) Destroy(this);
    }
    private void moveNormal(float theta)
    {
        myRect.anchoredPosition += new Vector2(Mathf.Cos(theta)*nSpeed*Time.deltaTime, Mathf.Sin(theta)*nSpeed*Time.deltaTime);
    }
    private void accelation(float theta)
    {
        time += Time.deltaTime;
        float speed = 26666.67f * time * time - 11333.33f * time + 2000;
        if(ttemp==100)Debug.Log(speed);
        myRect.anchoredPosition += new Vector2(Mathf.Cos(theta) * speed * Time.deltaTime, Mathf.Sin(theta) * speed * Time.deltaTime);

    }
}