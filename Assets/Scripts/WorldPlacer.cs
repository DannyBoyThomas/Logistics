using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WorldPlacer : MonoBehaviour
{

    public GameObject currentItem;
    public List<Vector2> selectedPlaces;
    Vector2 firstPos;
    GameObject selectorPrefab;

    GameObject highlightPrefab;
    GameObject errorPrefab;
    GameObject changePrefab;
    char dir = 'n';

    public bool escape = false;
    GameObject worldParent;

    void Start()
    {
        selectedPlaces = new List<Vector2>();
        selectorPrefab = (GameObject)Resources.Load("Prefabs/Selector");
        highlightPrefab = (GameObject)Resources.Load("Prefabs/Highlighter");
        errorPrefab = (GameObject)Resources.Load("Prefabs/Delete");
        changePrefab = (GameObject)Resources.Load("Prefabs/Overwrite");
        worldParent = GameObject.Find("World Objects");
    }

    // Update is called once per frame

    public LayerMask layer;
    int firstRot = 180;

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && getCurrentItem() != null)
        {
            // destroyCurrentItem();
           
            selectedPlaces.Clear();
            escape = true;

        }

        GameObject g = getCurrentItem();

        if (g == null)
        {
            GameObject.Find("Grid").GetComponent<Renderer>().material.color = Color.clear;
        }
        else
        {
            GameObject.Find("Grid").GetComponent<Renderer>().material.color = new Color(155 / 255f, 152 / 255f, 140 / 255f, 173 / 255f);
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200, layer))
        {
            if (hit.collider != null && hit.collider.name == "Plane")
            {
                int x = Mathf.RoundToInt(hit.point.x);
                int z = Mathf.RoundToInt(hit.point.z);
                removeSelectors();
                if (!Instances.itemMenu[0].HoverMenu() && !Instances.itemMenu[1].HoverMenu())
                {
                    GameObject p = (GameObject)Instantiate(highlightPrefab, new Vector3(x, 0.5f, z), Quaternion.identity);

                }
                if (g != null)
                {
                    g.transform.position = new Vector3(x, 0.5f, z);

                    Vector2 coords = new Vector2(x, z);


                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        //g.transform.Rotate(Vector3.up, 90);
                        firstRot += 90;

                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        //g.transform.Rotate(Vector3.up, -90);
                        firstRot -= 90;

                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        selectedPlaces.Clear();
                        firstPos = coords;
                        //firstRot = (int)getCurrentItem().transform.rotation.eulerAngles.y;

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






                        if (!escape)
                        {
                            draw();
                        }
                    }
                   

                }
            }
            if (g != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    getCurrentItem().transform.rotation = getDirection();//direction going
                }
                else
                {

                    getCurrentItem().transform.rotation = Quaternion.Euler(0, firstRot, 0);
                }
            }
            if (Input.GetMouseButtonUp(0) && g != null)
            {
                if (!escape)
                {
                    spawn();
                }
                firstRot = 180;
                getCurrentItem().transform.rotation = Quaternion.Euler(0, firstRot, 0);
                escape = false;


            }
        }
    }
    void draw()
    {
        removeSelectors();


        for (int i = 0; i < selectedPlaces.Count; i++)
        {
            Vector2 vec = selectedPlaces[i];
            GameObject g = getCurrentItem();
            int price = g.GetComponent<WorldObject>().Cost;
            bool gotFunds = availableFunds() >= price * (i + 1);

            bool useSelector = Instances.gridManager.inBounds(vec.x, vec.y) && isSpaceForObject(selectorPrefab, (int)vec.x, (int)vec.y) && gotFunds;
            GameObject prefab = useSelector ? selectorPrefab : errorPrefab;

            if (!useSelector) // try and swap
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    GameObject placed = Instances.gridManager.getObject(vec);
                    if (placed != null)
                    {
                        int placedPrice = placed.GetComponent<WorldObject>().Cost;
                        //Debug.Log("here");
                        prefab = changePrefab;
                    }
                }
            }
            GameObject p = (GameObject)Instantiate(prefab, new Vector3(vec.x, 0.5f, vec.y), Quaternion.identity);
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
        MoneyManager m = Instances.moneyManager;
        if (getCurrentItem() != null)
        {
            int price = getCurrentItem().GetComponent<WorldObject>().Cost;
            //bool rotate = !Input.GetKey(KeyCode.LeftShift);

            removeSelectors();
            if (Input.GetKey(KeyCode.LeftControl))
            {
                for (int i = 0; i < selectedPlaces.Count; i++) //remove swaps
                {
                    Vector2 vec = selectedPlaces[i];
                    GameObject g = Instances.gridManager.getObject(vec);
                    if (g != null)
                    {
                        int cost = g.GetComponent<WorldObject>().Cost;
                        m.AddFunds(cost);
                        Destroy(g);
                        Instances.gridManager.setObject(null, (int)vec.x, (int)vec.y);

                    }

                }
            }
            for (int i = 0; i < selectedPlaces.Count; i++)
            {
                Vector2 vec = selectedPlaces[i];
                if (Instances.gridManager.inBounds(vec.x, vec.y) && isSpaceForObject(getCurrentItem(), (int)vec.x, (int)vec.y))
                {

                    if (m.Money >= price)
                    {
                        GameObject h = (GameObject)Instantiate(currentItem, new Vector3(vec.x, 0.5f, vec.y), currentItem.transform.rotation);
                        h.transform.parent = worldParent.transform;
                        h.name = currentItem.name.Split('(')[0]; //remove clone name
                        h.GetComponent<WorldObject>().IsActive = true;

                        Instances.gridManager.addObject(h);
                        m.AddFunds(-price, h.transform.position);

                    }
                }
            }
        }
        selectedPlaces.Clear();
    }
    public void setCurrentItem(GameObject g)
    {
        currentItem = g;
        if (currentItem != null)
        {
            currentItem.transform.Rotate(0, 180, 0);
        }
    }
    public GameObject getCurrentItem()
    {
        return currentItem;
    }
    public bool isSpaceForObject(GameObject g, int x, int z)
    {
        return Instances.gridManager.isSpaceForObject(g, x, z);
    }
    Quaternion getDirection()
    {
        if (selectedPlaces.Count > 1)
        {
            if (dir == 'x') //x axis
            {
                int last = (int)(selectedPlaces[selectedPlaces.Count - 1]).x;
                int first = (int)firstPos.x;
                int dif = last - first;
                if (dif > 0) // + on X
                {
                    return Quaternion.Euler(0, 90, 0);
                }
                else
                {
                    return Quaternion.Euler(0, 270, 0);
                }
            }
            else if (dir == 'y') //y axis
            {
                int last = (int)(selectedPlaces[selectedPlaces.Count - 1]).y;
                int first = (int)firstPos.y;
                int dif = last - first;
                if (dif > 0) // + on X
                {
                    return Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    return Quaternion.Euler(0, 180, 0);
                }
            }
        }
        else
        {
            return currentItem.transform.rotation;
        }
        return Quaternion.identity;
    }
    public void destroyCurrentItem()
    {
        Destroy(currentItem);
        setCurrentItem(null);
    }
   
    public int availableFunds()
    {
        int money = Instances.moneyManager.Money;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            for (int i = 0; i < selectedPlaces.Count; i++)
            {
                Vector2 vec = selectedPlaces[i];
                GameObject current = Instances.gridManager.getObject(vec);
                if (current != null)
                {
                    int price = current.GetComponent<WorldObject>().Cost;
                    money += price;
                }
            }
        }


        return money;
    }
}
