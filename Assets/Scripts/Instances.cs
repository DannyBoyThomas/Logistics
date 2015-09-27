using UnityEngine;
using System.Collections;

public class Instances : MonoBehaviour {

    public static GridManager gridManager;
    public static WorldPlacer worldPlacer;
    public static ItemMenu itemMenu;
	void Start () {
        gridManager = GameObject.FindObjectOfType<GridManager>();
        worldPlacer = GameObject.FindObjectOfType<WorldPlacer>();
        itemMenu = GameObject.FindObjectOfType<ItemMenu>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
