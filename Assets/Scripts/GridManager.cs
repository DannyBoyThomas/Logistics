using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour
{
    
    public int size = 50;
    public  GameObject[,] grid;
    void Start()
    {
        grid = new GameObject[size, size];
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool addObject(GameObject g)
    {
        int x = Mathf.RoundToInt(g.transform.position.x);
        int z = Mathf.RoundToInt(g.transform.position.z);

        if (isSpaceForObject(g, x, z))
        {
            grid[x, z] = g;
            return true;
        }
            return false;
    }
    public bool isSpaceForObject(GameObject g, int x, int z)
    {
        if (g != null)
        {
            if (x < size && x >= 0 && z >= 0 && z < size)
            {
                return grid[x, z] == null;
            }
        }
        return true;
    }
    public Vector2 GetCoords(GameObject g)
    {
        int x = Mathf.RoundToInt(g.transform.position.x);
        int z = Mathf.RoundToInt(g.transform.position.z);
        return new Vector2(x, z);
    }
    public GameObject getObject(Vector2 pos)
    {
        return grid[(int)pos.x, (int)pos.y];
    }
}
