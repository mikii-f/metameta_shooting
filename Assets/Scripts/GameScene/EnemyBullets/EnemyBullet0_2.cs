using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet0_2 : MonoBehaviour
{
    //�����͂̍��W
    private Vector2 core;
    //�����͂̑��x
    private Vector2 coreSpeed;

    //�e�̏������x
    private Vector2 bulletSpeed;

    //�����͂̌W��
    private float k = 0.01f;

    private RectTransform myRect;
    //�����ʑ��ɂ�邸��
    private Vector2 pos;


    //�����͂����R�A���������ɔ��˂���邱�Ƃ�z��
    //rad�͒e�ۂ̏����̃R�A�̐i�s�����ɑ΂���p�x�Aamp�͐U���Aperiod�͎����Atheta0�͏����ʑ�
    public void initialization(Vector2 coreSpeed, float rad,float amp,float period,float theta0)
    {
        this.coreSpeed = coreSpeed;
        k = (Mathf.PI * 2 / period)* (Mathf.PI * 2 / period);
        float s= Mathf.Sqrt(k) * amp * Mathf.Cos(theta0);
        this.bulletSpeed = new Vector2(s * Mathf.Cos(rad), s*Mathf.Sin(rad))+coreSpeed;
        this.pos=new Vector2(amp * Mathf.Cos(theta0)*Mathf.Cos(rad), amp * Mathf.Sin(theta0)*Mathf.Sin(rad));
    }
    // Start is called before the first frame update
    void Start()
    {
        this.core=GetComponent<RectTransform>().anchoredPosition;
        myRect = GetComponent<RectTransform>();
        myRect.anchoredPosition += pos;
    }

    // Update is called once per frame
    void Update()
    {
        if(myRect.anchoredPosition.y < -560)
        {
            Destroy(gameObject);
        }
        if(!GameManager.isPause)
        {
            float t = Time.deltaTime;
            core+= coreSpeed * t;
            //�����͂̌v�Z
            Vector2 force = (core - myRect.anchoredPosition) * k;

            //�����͂𑬓x�ɉ�����
            bulletSpeed += force * t;
            //���x���ʒu�ɉ�����
            myRect.anchoredPosition += bulletSpeed * t;
        }
    }
}
