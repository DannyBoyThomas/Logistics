using UnityEngine;
using System.Collections;

public class ItemMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClick(GameObject g)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        GameObject h = (GameObject)Instantiate(g, pos, Quaternion.identity);
        Instances.worldPlacer.setCurrentItem(h);
     
       
      
    }


}
