using UnityEngine;

//自機の弾丸の動作を管理する
public class PlayerBullet : MonoBehaviour
{
    private RectTransform myRect;
    private int speed = 1000;
    public float radian = Mathf.PI / 2;
    private float time = 0;
    public bool accel = false;  //速度変化を採用するかしないか
    //private int ttemp;
    //private static int temp = 0;

    //PlayerBullet()
    //{
    //    temp++;
    //    ttemp = temp;
    //}

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPause)
        {
            if (!accel)
            {
                Normal(radian);
            }
            else
            {
                Acceleration(radian);
            }
        }
        if (myRect.anchoredPosition.y > 555)
        {
            Destroy(gameObject);
        }
    }

    private void Normal(float rad)
    {
        myRect.anchoredPosition += new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * speed * Time.deltaTime;
    }
    private void Acceleration(float rad)
    {
        time += Time.deltaTime;
        float newSpeed = 2000 * time * time - 1000 * time + 500;
        myRect.anchoredPosition += new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * newSpeed * Time.deltaTime;
    }

    //private void move()
    //{
    //    //moveNormal(Mathf.PI/2);
    //    accelation(Mathf.PI/2);

    //    if (myRect.anchoredPosition.y > 550) Destroy(gameObject);
    //}
    //private void moveNormal(float theta)
    //{
    //    myRect.anchoredPosition += new Vector2(Mathf.Cos(theta)*nSpeed*Time.deltaTime, Mathf.Sin(theta)*nSpeed*Time.deltaTime);
    //}
    //private void accelation(float theta)
    //{
    //    time += Time.deltaTime;
    //    float speed = 26666.67f * time * time - 11333.33f * time + 2000;
    //    if(ttemp==100)Debug.Log(speed);
    //    myRect.anchoredPosition += new Vector2(Mathf.Cos(theta) * speed * Time.deltaTime, Mathf.Sin(theta) * speed * Time.deltaTime);

    //}
}