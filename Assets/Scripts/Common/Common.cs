using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//よく使う関数を提供する
public class Common : MonoBehaviour
{
    //シーン遷移直後のフェードイン(最も手前にある黒いImageオブジェクトを引数に与える)
    public static IEnumerator FadeIn(GameObject black)
    {
        Image image = black.GetComponent<Image>();
        image.color = Color.black;
        while (image.color.a > 0)
        {
            Color temp = image.color;
            temp.a -= Time.deltaTime;
            image.color = temp;
            yield return null;
        }
        image.color = new(0, 0, 0, 0);
        black.SetActive(false);
    }
    //シーン遷移直前のフェードアウト(最も手前にある黒いImageオブジェクトを引数に与える)
    public static IEnumerator FadeOut(GameObject black)
    {
        Image image = black.GetComponent<Image>();
        black.SetActive(true);
        while (image.color.a < 1)
        {
            Color temp = image.color;
            temp.a += Time.deltaTime;
            image.color = temp;
            yield return null;
        }
        image.color = new(0, 0, 0, 1);
    }
    //ボタンを押したときのアニメーション
    public static IEnumerator Button(RectTransform buttonRect)
    {
        Vector2 temp = buttonRect.localScale;
        buttonRect.localScale = temp * 0.9f;
        yield return new WaitForSeconds(0.1f);
        buttonRect.localScale = temp;
    }
}