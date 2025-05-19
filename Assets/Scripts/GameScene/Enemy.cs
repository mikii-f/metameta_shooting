using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//敵の汎用的な動作を管理、個別の敵用クラスの親クラスとなる
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject images;
    protected RectTransform parentRect; //親の座標
    protected RectTransform myRect;     //親を基準とした、敵機自体の座標 (これら二つを分けることで、「下に移動しつつ円運動」とかを実現しやすくする)
    protected int hp;                   //体力
    protected EnemyLevel enemyLevel;    //自身のレベル
    protected Image[] myImages;
    protected RectTransform enemyParent;    //弾丸のInstantiate先
    
    private void OnTriggerEnter2D(Collider2D bullet)
    {
        //自機の弾丸に当たったときHPを減らすとともに弾丸をDestroy
        //一定割合HPが減ると色が変わっていく(赤→黄→紫)
        //HPが0になると撃破となり、GameManagerに通知、更に「メタ」をドロップしてからDestroy
    }

    //敵機を構成するImage要素全ての色を一気に変える
    private void ColorChange(Color color)
    {
        foreach (Image image in myImages)
        {
            image.color = color;
        }
    }
}