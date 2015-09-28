using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class MenuButton : MonoBehaviour {

    public GameObject spawnObject;
	void Start () 
    {
        if (spawnObject != null)
        {
            Text t = GetComponentInChildren<Text>();
            t.text = spawnObject.name;

#if UNITY_EDITOR
            Texture2D image= AssetPreview.GetAssetPreview(spawnObject);
            
            if (image != null)
            {
                GetComponent<Image>().sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
            }
#endif
        }


	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
    public void OnClick()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject h = (GameObject)Instantiate(spawnObject, pos, Quaternion.identity);

        foreach(MenuCategory cat in Instances.itemMenu[0].GetComponentsInChildren<MenuCategory>())
        {
            cat.Open = false;
        }

        Instances.worldPlacer.setCurrentItem(h);



    }
}
