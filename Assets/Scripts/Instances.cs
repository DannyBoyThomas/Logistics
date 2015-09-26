using UnityEngine;
using System.Collections;

public class Instances : MonoBehaviour {

    public static GridManager gridManager;
    public static WorldPlacer worldPlacer;
	void Start () {
        gridManager = GameObject.FindObjectOfType<GridManager>();
        worldPlacer = GameObject.FindObjectOfType<WorldPlacer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
