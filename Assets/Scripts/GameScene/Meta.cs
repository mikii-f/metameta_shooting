using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//「メタ」またはそれに似た文字の動作を管理する
public class Meta : MonoBehaviour
{
    [SerializeField] private RectTransform parentRect;
    private RectTransform myRect;
    private RectTransform playerRect;
    private int width = 100;
    private int xSpeed = 100;
    private int ySpeed = 200;
    private int right = 1;
    private bool isMeta = false;
    void Awake()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        myRect = GetComponent<RectTransform>();
        playerRect = GameObject.Find("Player").GetComponent<RectTransform>();
        width = UnityEngine.Random.Range(50, 100);
        xSpeed = UnityEngine.Random.Range(50, 100);
        ySpeed = UnityEngine.Random.Range(150, 200);
        StartCoroutine(MetaCheck());
    }
    private IEnumerator MetaCheck()
    {
        yield return null;
        if (GetComponent<Text>().text == "メタ")
        {
            isMeta = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.isPause == false)
        {
            //スキル3発動中、自身が「メタ」であるときのみプレイヤーに向かって移動する
            if (GameManager.isSkill3 && isMeta)
            {
                Vector2 playerPos = playerRect.anchoredPosition;
                float deltaX = playerPos.x - BasePos().x;
                float deltaY = playerPos.y - BasePos().y;
                //既に近い場合は動かない
                if (Mathf.Pow((Mathf.Pow(deltaX, 2) + Mathf.Pow(deltaY, 2)), 0.5f) >= 30)
                {
                    float angle = Mathf.Atan2(deltaY, deltaX);
                    parentRect.anchoredPosition += new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ySpeed * 2 * Time.deltaTime;
                }
            }
            else
            {
                parentRect.anchoredPosition -= new Vector2(0, ySpeed * Time.deltaTime);
                Vector2 temp = myRect.anchoredPosition;
                temp.x += xSpeed * 2 * Time.deltaTime * right;
                if (temp.x >= width)
                {
                    right = -1;
                }
                else if (temp.x <= -width)
                {
                    right = 1;
                }
                myRect.anchoredPosition = temp;
            }
        }
        if (BasePos().y < -600)
        {
            Destroy(gameObject);
        }
    }

    private Vector2 BasePos()
    {
        return parentRect.anchoredPosition + myRect.anchoredPosition;
    }
    //Colliderを持っているのは子要素であるため、親要素ごと消えるようにする必要がある
    private void OnDestroy()
    {
        Destroy(parentRect.gameObject);
    }
}