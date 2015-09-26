using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour
{
    
    public int size = 20;
    public  GameObject[,] grid;
    void Start()
    {

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
        return grid[x,z] == null;
    }
    public Vector2 GetCoords(GameObject g)
    {
        int x = Mathf.RoundToInt(g.transform.position.x);
        int z = Mathf.RoundToInt(g.transform.position.z);
        return new Vector2(x, z);
    }
}
