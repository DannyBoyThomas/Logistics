using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    float targetOpacity = 0.5f;
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
                    Color c = transform.GetChild(i).GetComponent<Image>().color;
                    transform.GetChild(i).GetComponent<Image>().color = new Color(c.r, c.g, c.b, col.a * 2);
                    Color t = transform.GetChild(i).GetComponentInChildren<Text>().color;
                    transform.GetChild(i).GetComponentInChildren<Text>().color = new Color(t.r, t.g, t.b, col.a*2);
                   
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
                    Color c = transform.GetChild(i).GetComponent<Image>().color;
                    transform.GetChild(i).GetComponent<Image>().color = new Color(c.r, c.g, c.b, col.a );
                     Color t = transform.GetChild(i).GetComponentInChildren<Text>().color;
                     transform.GetChild(i).GetComponentInChildren<Text>().color = new Color(t.r, t.g, t.b, col.a);
                   
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
