using UnityEngine;

//敵の弾丸の動作を管理する
//ホーミングタイプの弾丸
public class EnemyBullet1 : MonoBehaviour
{
    private RectTransform myRect;
    private int speed = 200;
    private RectTransform playerRect;
    private float angle = -Mathf.PI / 2;
    private bool homing = true;
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        playerRect = GameObject.Find("Player").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPause)
        {
            if (homing)
            {
                Vector2 playerPos = playerRect.anchoredPosition;
                Vector2 myPos = myRect.anchoredPosition;
                if (playerPos.y + 50 > myPos.y)
                {
                    homing = false;
                }
                else
                {
                    angle = Mathf.Atan2((playerPos.y - myPos.y), (playerPos.x - myPos.x));
                }
            }
            myRect.anchoredPosition += new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed * Time.deltaTime;
        }
        if (myRect.anchoredPosition.y < -560 || myRect.anchoredPosition.x > 400 || myRect.anchoredPosition.x < -400)
        {
            Destroy(gameObject);
        }
    }
}