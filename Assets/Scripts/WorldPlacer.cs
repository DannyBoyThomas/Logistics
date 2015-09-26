﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldPlacer : MonoBehaviour {

    public GameObject currentItem;
    List<Vector2> selectedPlaces;
    Vector2 firstPos;
    GameObject selectorPrefab;
<<<<<<< HEAD
    GameObject highlightPrefab;
    char dir = 'n';
=======
>>>>>>> origin/master
   public bool buttonClicked = false;
    bool prevClicked = false;
	void Start () {
        selectedPlaces = new List<Vector2>();
        selectorPrefab = (GameObject)Resources.Load("Prefabs/Selector");
        highlightPrefab = (GameObject)Resources.Load("Prefabs/Highlighter");
	}
	
	// Update is called once per frame
    
    public LayerMask layer;
	void Update ()
    {
        GameObject g = getCurrentItem();
<<<<<<< HEAD


      
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(currentItem);
                setCurrentItem(selectorPrefab);
            }
=======
       
        if (g != null )
        {
>>>>>>> origin/master
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50,layer))
            {
                if (hit.collider != null && hit.collider.name == "Plane")
                {
                    int x = Mathf.RoundToInt(hit.point.x);
                    int z = Mathf.RoundToInt(hit.point.z);
                    GameObject high = highlightPrefab.transform.FindChild("Parent").gameObject;
                    Graphics.DrawMeshNow(high.GetComponent<MeshFilter>().sharedMesh, new Vector3(x, 0.5f, z), Quaternion.identity, LayerMask.NameToLayer("Overlay"));
                    if (g != null)
                    {
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

                        selectedPlaces = new List<Vector2>();
                        int difX = (int)(coords.x - firstPos.x);
                        int difY = (int)(coords.y - firstPos.y);
                        int valX = difX < 0 ? -1 : 1;
                        int valY = difY < 0 ? -1 : 1;
                        if (Mathf.Abs(difX) > Mathf.Abs(difY)) //addOn x Axis
                        {
                            for (int i = 0; i <= Mathf.Abs(difX); i++)
                            {
                                selectedPlaces.Add(new Vector2(firstPos.x + (valX*i), firstPos.y));
                            }
                        }
                        else // y axis
                        {
                            for (int i = 0; i <= Mathf.Abs(difY); i++)
                            {
                                selectedPlaces.Add(new Vector2(firstPos.x, firstPos.y + (valY*i)));
                            }
                        }
                        draw();
                    }
                    
                 }
              }
            if (Input.GetMouseButtonUp(0) )
            {
<<<<<<< HEAD
               spawn();
=======
                if (!buttonClicked)
                {
                    spawn();
                }
                else
                {
                    buttonClicked = false;
                }
                
               
>>>>>>> origin/master
                
            }
          }  
	}
    void draw()
    {
        removeSelectors();
       
        for (int i = 0; i < selectedPlaces.Count; i++)
        {
            Vector2 vec = selectedPlaces[i];
            Instantiate(selectorPrefab, new Vector3(vec.x, 0.5f, vec.y), Quaternion.identity);
        }
    }
    void removeSelectors()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Selector"))
        {
            Destroy(g);
        }
    }
    void spawn()
    {
        removeSelectors();
        for (int i = 0; i < selectedPlaces.Count; i++)
        {
            Vector2 vec = selectedPlaces[i];
            if (isSpaceForObject(currentItem, (int)vec.x, (int)vec.y))
            {
<<<<<<< HEAD
               
                GameObject h = (GameObject)Instantiate(currentItem, new Vector3(vec.x, 0.5f, vec.y), getDirection());
=======
                Debug.Log("spawned");
                GameObject h = (GameObject)Instantiate(currentItem, new Vector3(vec.x, 0.5f, vec.y), currentItem.transform.rotation);
>>>>>>> origin/master
                Instances.gridManager.addObject(h);
            }
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
