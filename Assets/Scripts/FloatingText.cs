using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingText : MonoBehaviour {


    public Text text;

    public string Text;

    public Color color;
    public float fadeTime = 1.5f;


    public void Update()
    {
        Vector2 pos = GetComponent<RectTransform>().anchoredPosition;
        pos.y += Time.deltaTime * 15f;
        GetComponent<RectTransform>().anchoredPosition = pos;


        
        text.text = Text;
        fadeTime -= Time.deltaTime;

        if(fadeTime < 0)
        {
            if (color.a > 0)
                color.a -= Time.deltaTime * 5f;
            else
                Destroy(this.gameObject);
        }

        text.color = color;
    }
}
