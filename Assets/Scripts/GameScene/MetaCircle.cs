using System.Collections;
using UnityEngine;

//「メタ」を生成するときの演出用
public class MetaCircle : MonoBehaviour
{
    private RectTransform myRect;
    private Vector2 destination = new(0, 800);
    private float angle;
    private float speed;
    private bool start = false;

    void Awake()
    {
        myRect = GetComponent<RectTransform>();
        StartCoroutine(Set());
    }
    private IEnumerator Set()
    {
        yield return null;
        float xDelta = destination.x - myRect.anchoredPosition.x;
        float yDelta = destination.y - myRect.anchoredPosition.y;
        angle = Mathf.Atan2(yDelta, xDelta);
        speed = Mathf.Pow((Mathf.Pow(xDelta, 2) + Mathf.Pow(yDelta, 2)), 0.5f) * 3;
        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            myRect.anchoredPosition += new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed * Time.deltaTime;
            if (myRect.anchoredPosition.y > 600)
            {
                Destroy(gameObject);
            }
        }
    }
}