using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChangeText : MonoBehaviour
{
    private Text myText;
    // Start is called before the first frame update
    void Awake()
    {
        myText = GetComponent<Text>();
        StartCoroutine(Set());
    }
    private IEnumerator Set()
    {
        yield return null;
        if (myText.text != "")
        {
            if (myText.text[0] == '-')
            {
                myText.color = new(200f / 255, 0, 0, 1);
            }
            else
            {
                myText.text = "+" + myText.text;
                myText.color = new(0, 170f / 255, 1, 1);
            }
        }
        }

        // Update is called once per frame
        void Update()
    {
        Color c = myText.color;
        c.a -= Time.deltaTime;
        myText.color = c;
        if (c.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
