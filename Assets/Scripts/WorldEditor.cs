using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldEditor : MonoBehaviour {

    List<Vector2> selectedPlaces;
    Vector2 firstPos;
    char dir = 'n';
    GameObject deletePrefab;
    bool cancelled = false;
	void Start () 
    {
        deletePrefab = (GameObject)Resources.Load("Prefabs/Delete");
	}
	
	// Update is called once per frame
	void Update () 
    {

        Vector2 coord = getPointerCoord();
        GameObject cI = Instances.worldPlacer.getCurrentItem();
        if (Input.GetKey(KeyCode.Escape))
        {
            cancelled = true;
        }
        if (coord.x >= 0 &&  cI== null)
        {
         
            if (Input.GetMouseButtonDown(1))
            {
                
                firstPos = coord;

                //}

            }

            else if (Input.GetMouseButton(1) && !cancelled)
            {
                

                selectedPlaces = new List<Vector2>();
                int difX = (int)(coord.x - firstPos.x);
                int difY = (int)(coord.y - firstPos.y);
                int valX = difX < 0 ? -1 : 1;
                int valY = difY < 0 ? -1 : 1;
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    for (int i = 0; i <= Mathf.Abs(difX); i++)
                    {
                        for (int j = 0; j < Mathf.Abs(difY); j++)
                        {
                            selectedPlaces.Add(new Vector2(firstPos.x + (valX * i), firstPos.y + (valY * j)));
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(difX) > Mathf.Abs(difY)) //addOn x Axis
                    {
                        dir = 'x';
                        for (int i = 0; i <= Mathf.Abs(difX); i++)
                        {
                            selectedPlaces.Add(new Vector2(firstPos.x + (valX * i), firstPos.y));
                        }
                    }
                    else // y axis
                    {
                        dir = 'y';
                        for (int i = 0; i <= Mathf.Abs(difY); i++)
                        {
                            selectedPlaces.Add(new Vector2(firstPos.x, firstPos.y + (valY * i)));
                        }
                    }
                }
                draw();
               
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (!cancelled)
                {
                    remove();
                }
                cancelled = false;

            }
        }
	}
    void draw()
    {
        removeSelectors();


        for (int i = 0; i < selectedPlaces.Count; i++)
        {
            Vector2 vec = selectedPlaces[i];
            Instantiate(deletePrefab, new Vector3(vec.x, 0.5f, vec.y), Quaternion.identity);
        }
    }
    void removeSelectors()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Selector"))
        {
            Destroy(g);
        }
    }
    void remove()
    {
        
        for (int i = 0; i < selectedPlaces.Count; i++)
        {
            Vector2 vec = selectedPlaces[i];
           
            GameObject g = Instances.gridManager.getObject(vec);
            if (g != null)
            {
                int price = g.GetComponent<WorldObject>().Cost;
                
                Destroy(g);
                Instances.gridManager.setObject(null, (int)vec.x, (int)vec.y);
                Instances.moneyManager.AddFunds(price, new Vector3(vec.x,0.5f,vec.y));
            }
        }
        selectedPlaces.Clear();
    }
    public LayerMask layer;
    Vector2 getPointerCoord()
    {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200, layer))
            {
                if (hit.collider != null && hit.collider.name == "Plane")
                {
                    int x = Mathf.RoundToInt(hit.point.x);
                    int z = Mathf.RoundToInt(hit.point.z);
                    return new Vector2(x, z);
                }
            }
            return new Vector2(-10, -10); ;
    }
}
