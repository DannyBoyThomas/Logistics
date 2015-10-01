using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour {

	Color baseCol = Color.black;
        Color textCol = Color.white;
	void Start () 
    {
        GetComponent<Image>().color = baseCol;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponentInChildren<Text>().color = textCol;
        }
	}
    float targetOpacity = 0.7f;
    float hiddenOpacity = 0.1f;
	// Update is called once per frame
    
	void Update () 
    {
        if (HoverMenu())
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) //hide selector
            {
                Instances.worldPlacer.destroyCurrentItem();
            }
            Color col = GetComponent<Image>().color;

            if (col.a < targetOpacity)
            {
                GetComponent<Image>().color += new Color(0, 0, 0, 0.1f);

                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<Image>())
                    {
                        Color c = transform.GetChild(i).GetComponent<Image>().color;
                        transform.GetChild(i).GetComponent<Image>().color = new Color(c.r, c.g, c.b, col.a * 2);
                    }
                    if (transform.GetChild(i).GetComponent<Text>())
                    {
                        Color t = transform.GetChild(i).GetComponentInChildren<Text>().color;
                        transform.GetChild(i).GetComponentInChildren<Text>().color = new Color(t.r, t.g, t.b, col.a * 2);
                    }
                }
            }

        }
        else
        {
            Color col = GetComponent<Image>().color;
            if (col.a > hiddenOpacity)
            {
                GetComponent<Image>().color -= new Color(0, 0, 0, 0.1f);
                 for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).GetComponent<Image>())
                    {
                        Color c = transform.GetChild(i).GetComponent<Image>().color;
                        transform.GetChild(i).GetComponent<Image>().color = new Color(c.r, c.g, c.b, col.a);
                    }
                    if (transform.GetChild(i).GetComponent<Text>())
                    {
                        Color t = transform.GetChild(i).GetComponentInChildren<Text>().color;
                        transform.GetChild(i).GetComponentInChildren<Text>().color = new Color(t.r, t.g, t.b, col.a);
                    }
                }
            }
        }
	}
    public bool HoverMenu()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 mousePosition = Input.mousePosition;
        Vector3[] worldCorners = new Vector3[4];
        rect.GetWorldCorners(worldCorners);

        if (mousePosition.x >= worldCorners[0].x && mousePosition.x < worldCorners[2].x
           && mousePosition.y >= worldCorners[0].y && mousePosition.y < worldCorners[2].y)
        {
            return true;
        }
        return false;
    }
    


}
