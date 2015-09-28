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
    public void setObject(GameObject g, int x, int z)
    {
        grid[x,z] = g;
    }
    public bool addObject(GameObject g)
    {
        int x = Mathf.RoundToInt(g.transform.position.x);
        int z = Mathf.RoundToInt(g.transform.position.z);

        if ( inBounds(x,z) && isSpaceForObject(g, x, z))
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
        if(inBounds(pos.x,pos.y))
        {
          return grid[(int)pos.x, (int)pos.y];
        }
        return null;
    }
    public Vector2 GetCoordsFromVector(Vector3 vec)
    {
        return new Vector2(Mathf.RoundToInt(vec.x), Mathf.RoundToInt(vec.z));
    }
    public bool inBounds(float x, float z)
    {
        Vector2 coords = new Vector2(Mathf.RoundToInt(x), Mathf.RoundToInt(z));
        if (coords.x >= 0 && coords.x < size)
        {
            if (coords.y >= 0 && coords.y < size)
            {
                //Debug.Log(coords);
                return true;
            }
        }
        return false;
    }
    public void ClearGrid()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (grid[i, j] != null)
                {
                    Destroy(grid[i, j]);
                        
                }
                grid[i,j] = null;
            }
        }
        
    }
}
