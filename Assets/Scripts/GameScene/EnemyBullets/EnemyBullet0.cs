using UnityEngine;

//敵の弾丸の動作を管理する
//直進タイプの弾丸
public class EnemyBullet0 : MonoBehaviour
{
    private RectTransform myRect;
    public int speed = 300;
    public float radian = Mathf.PI / 2;
    void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPause)
        {
            Move(radian);
        }
        if (myRect.anchoredPosition.y < -560)
        {
            Destroy(gameObject);
        }
    }
    private void Move(float rad)
    {
        myRect.anchoredPosition += new Vector2(Mathf.Cos(rad), -Mathf.Sin(rad)) * speed * Time.deltaTime;
    }
}