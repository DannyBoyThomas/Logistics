using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingText : MonoBehaviour {


    public Text text;

    public string Text;

    public Color color;
    public float fadeTime = 1.5f;

    public Vector3 worldPos;

    float lifeTime = 0;
    public void Update()
    {

        lifeTime += Time.deltaTime;

        transform.position = Camera.main.WorldToScreenPoint(worldPos);

        Vector3 pos = GetComponent<RectTransform>().anchoredPosition;

        pos.y += lifeTime * 15; // lifeTime;

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
