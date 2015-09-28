using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveLayout : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Save(string levelName)
    {
        int n = PlayerPrefs.GetInt("NumOfLevels");
        PlayerPrefs.SetString("Levels"+ n, levelName);
        int size = Instances.gridManager.size;
        PlayerPrefs.SetInt(levelName + "Size" , size);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject g = Instances.gridManager.getObject(new Vector2(i,j));
                if( g != null)
                {
                    PlayerPrefs.SetString("Object" + i + "x" + j, g.name);
                    int rot = Mathf.RoundToInt(g.transform.position.y);
                    PlayerPrefs.SetInt("ObjectDir" + i + "x" + j, rot);
                }
            }
        }
        PlayerPrefs.SetInt("NumOfLevels",PlayerPrefs.GetInt("NumOfLevels")+1);
    }
    public List<string> getSavedLevelNames()
    {
        List<string> levels = new List<string>();
        
        int maxLevels = PlayerPrefs.GetInt("NumOfLevels");

        for (int i = 0; i < maxLevels; i++)
        {
            levels.Add(PlayerPrefs.GetString("Levels" + i));
        }
       
        Debug.Log(levels);

        return levels;
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Load()
    {
        getSavedLevelNames();
    }
}
