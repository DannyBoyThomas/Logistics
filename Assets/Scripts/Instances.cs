using UnityEngine;
using System.Collections;

public class Instances : MonoBehaviour {

    public static GridManager gridManager;
    public static WorldPlacer worldPlacer;
    public static ItemMenu[] itemMenu;

    public static MoneyManager moneyManager;

	void Start () {
        gridManager = GameObject.FindObjectOfType<GridManager>();
        worldPlacer = GameObject.FindObjectOfType<WorldPlacer>();
        itemMenu = GameObject.FindObjectsOfType<ItemMenu>();
        moneyManager = GameObject.FindObjectOfType<MoneyManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
