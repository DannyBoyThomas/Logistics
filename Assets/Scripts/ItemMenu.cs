using UnityEngine;
using System.Collections;

public class ItemMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
           if(HoverMenu())
           {
                Instances.worldPlacer.destroyCurrentItem();
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
