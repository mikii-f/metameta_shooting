using UnityEngine;
using UnityEngine.UI;

//レベル1の敵(強め) 行動パターン1
public class Enemy1_1 : Enemy
{
    private RectTransform playerRect;
    private int duration = 20;
    private float durationCount = 0;
    private float shotCount = 0;
    private float shotIntervalCount = 0.1f;
    private int state = 0;
    private Vector2 destination;

    void Awake()
    {
        playerRect = GameObject.Find("Player").GetComponent<RectTransform>();
        myRect = GetComponent<RectTransform>();
        maxHP = 40;
        hp = 40;
        enemyLevel = EnemyLevel.Level1;
        myImages = GetComponentsInChildren<Image>(true);
        enemyParent = GameObject.Find("EnemyParent").GetComponent<RectTransform>();
        audioSource = GetComponent<AudioSource>();
        interval = 4;
        intervalCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        isBulletCollision = false;
        if (!GameManager.isPause)
        {
            durationCount += Time.deltaTime;
            switch (state)
            {
                //目的地の決定
                case 0:
                    destination = new Vector2(playerRect.anchoredPosition.x, 400);
                    state = 1;
                    break;
                //移動中
                case 1:
                    float xDelta = destination.x - parentRect.anchoredPosition.x;
                    float yDelta = destination.y - parentRect.anchoredPosition.y;
                    float angle = Mathf.Atan2(yDelta, xDelta);
                    parentRect.anchoredPosition += new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 600 * Time.deltaTime;
                    if (destination.x - 20 < parentRect.anchoredPosition.x && parentRect.anchoredPosition.x < destination.x + 20 && parentRect.anchoredPosition.y <= 400)
                    {
                        state = 2;
                    }
                    break;
                //連射
                case 2:
                    if (shotIntervalCount >= 0.1f)
                    {
                        GameObject newBullet = Instantiate(Resources.Load<GameObject>("Prefabs/EnemyBullet0"), enemyParent);
                        newBullet.GetComponent<RectTransform>().anchoredPosition = BasePos() + new Vector2(0, -100);
                        newBullet.GetComponent<EnemyBullet0>().speed = 900;
                        shotCount++;
                        if (shotCount == 5)
                        {
                            state = 3;
                            shotCount = 0;
                            break;
                        }
                        else
                        {
                            shotIntervalCount = 0;
                        }
                    }
                    else
                    {
                        shotIntervalCount += Time.deltaTime;
                        durationCount += Time.deltaTime;
                    }
                    break;
                //待機
                case 3:
                    intervalCount += Time.deltaTime;
                    if (intervalCount >= interval)
                    {
                        state = 0;
                        intervalCount = 0;
                    }
                    if (durationCount >= duration)
                    {
                        state = 4;
                    }
                    break;
                //下方向に去る
                case 4:
                    parentRect.anchoredPosition += new Vector2(0, -600 * Time.deltaTime);
                    break;
                default:
                    break;
            }
        }
        if (parentRect.anchoredPosition.y < -650)
        {
            GameManager.disappearedEnemyCount++;
            Destroy(parentRect.gameObject);
        }
    }
}