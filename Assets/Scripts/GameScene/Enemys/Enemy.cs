using UnityEngine;
using UnityEngine.UI;

//敵の汎用的な動作を管理、個別の敵用クラスの親クラスとなる
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected RectTransform parentRect; //親の座標
    protected RectTransform myRect;     //親を基準とした、敵機自体の座標 (これら二つを分けることで、「下に移動しつつ円運動」とかを実現しやすくする)
    protected int maxHP;                //最大体力
    protected int hp;                   //現在の体力
    protected EnemyLevel enemyLevel;    //自身のレベル
    protected Image[] myImages;
    protected RectTransform enemyParent;    //弾丸のInstantiate先
    protected float interval;
    protected float intervalCount = 0;
    protected bool isBulletCollision = false;
    protected AudioSource audioSource;
    
    private void OnTriggerEnter2D(Collider2D bullet)
    {
        //自機の弾丸に当たったときHPを減らすとともに弾丸をDestroy
        if (bullet.CompareTag("PlayerBullet") && !isBulletCollision)
        {
            isBulletCollision = true;
            hp--;
            Destroy(bullet.gameObject);
            audioSource.Play();
            //HPが0になると撃破となり、GameManagerに通知、更にSPおよび「メタ」獲得演出を出してからDestroy
            if (hp == 0)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().DestroyEnemy(enemyLevel);
                Transform decorationParent = GameObject.Find("DecorationParent").transform;
                GameObject spCircle = Instantiate(Resources.Load<GameObject>("Prefabs/SPCircle"), decorationParent);
                spCircle.GetComponent<RectTransform>().anchoredPosition = BasePos();
                GameObject metaCircle = Instantiate(Resources.Load<GameObject>("Prefabs/MetaCircle"), decorationParent);
                metaCircle.GetComponent<RectTransform>().anchoredPosition = BasePos();
                Destroy(parentRect.gameObject);
            }
            //一定割合HPが減ると色が変わっていく(赤→黄→紫)
            else if (hp < maxHP / 5)
            {
                ColorChange(new(120f / 255, 0, 120f / 255, 1));
            }
            else if (hp < maxHP / 2)
            {
                ColorChange(new(200f / 255, 200f / 255, 0, 1));
            }
        }
    }

    //敵機を構成するImage要素全ての色を一気に変える
    private void ColorChange(Color color)
    {
        foreach (Image image in myImages)
        {
            image.color = color;
        }
    }
    protected Vector2 BasePos()
    {
        return parentRect.anchoredPosition + myRect.anchoredPosition;
    }
}