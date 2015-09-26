using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldPlacer : MonoBehaviour {

    public GameObject currentItem;
    List<Vector2> selectedPlaces;
    Vector2 firstPos;
    GameObject selectorPrefab;
    bool prevClicked = false;
	void Start () {
        selectedPlaces = new List<Vector2>();
        selectorPrefab = (GameObject)Resources.Load("Prefabs/Selector");
	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject g = getCurrentItem();
        selectedPlaces = new List<Vector2>();
        if (g != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 20))
            {
                if (hit.collider != null && hit.collider.name == "Plane")
                {
                    int x = Mathf.RoundToInt(hit.point.x);
                    int z = Mathf.RoundToInt(hit.point.z);

                    g.transform.position = new Vector3(x, 0.5f, z);
                    Vector2 coords = new Vector2(x,z);
            

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        g.transform.Rotate(Vector3.up, 90);
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        g.transform.Rotate(Vector3.up, -90);
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        firstPos = coords;

                    }
                    else if (Input.GetMouseButton(0))
                    {
                        int difX = (int)(coords.x - firstPos.x);
                        int difY = (int)(coords.y - firstPos.y);

                        if (difX > difY) //addOn x Axis
                        {
                            for (int i = 0; i < difX; i++)
                            {
                                selectedPlaces.Add(new Vector2(firstPos.x + i, firstPos.y));
                            }
                        }
                        else // y axis
                        {
                            for (int i = 0; i < difY; i++)
                            {
                                selectedPlaces.Add(new Vector2(firstPos.x, firstPos.y + i));
                            }
                        }
                        draw();
                    }
                    else if(Input.GetMouseButtonUp(0))
                    {
                        spawn();
                    }
                 }
              }
            }  
	}
    void draw()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Selector"))
        {
            Destroy(g);
        }
        for (int i = 0; i < selectedPlaces.Count; i++)
        {
            Vector2 vec = selectedPlaces[i];
            Instantiate(currentItem, new Vector3(vec.x, 0.5f, vec.y), Quaternion.identity);
        }
    }
    void spawn()
    {
        for (int i = 0; i < selectedPlaces.Count; i++)
        {
            Vector2 vec = selectedPlaces[i];
            Instantiate(currentItem, new Vector3(vec.x, 0.5f, vec.y), Quaternion.identity);
        }
    }
    public void setCurrentItem(GameObject g)
    {
        currentItem = g;
    }
    public GameObject getCurrentItem()
    {
        return currentItem;
    }
    public bool isSpaceForObject(GameObject g, int x, int z)
    {
        return Instances.gridManager.isSpaceForObject(g, x, z);
    }
}
