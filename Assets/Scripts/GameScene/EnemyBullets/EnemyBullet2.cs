using UnityEngine;

//敵の弾丸の動作を管理する
//直進、大きく重いタイプの弾丸
public class EnemyBullet2 : MonoBehaviour
{
    private RectTransform myRect;
    private int speed = 200;
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPause)
        {
            myRect.anchoredPosition -= new Vector2(0, speed * Time.deltaTime);
        }
        if (myRect.anchoredPosition.y < -640)
        {
            Destroy(gameObject);
        }
    }
}