using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveLayout : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Save(string levelName)
    {
        SaveTemplate temp = new SaveTemplate();
        int size =Instances.gridManager.size;
        temp.gridSize = size;
        temp.levelName = levelName;
        temp.objects = new List<ObjectTemplate>();
       
        for (int i = 0; i < size; i++)
        {
          
            for (int j = 0; j < size; j++)
            {
               
                GameObject g = Instances.gridManager.getObject(new Vector2(i,j));
                if( g != null)
                {
                    ObjectTemplate t = new ObjectTemplate();
                    t.name = g.name;
                    t.x = i;
                    t.y = j;
                    t.rot = Mathf.RoundToInt(g.transform.rotation.eulerAngles.y);
                    temp.objects.Add(t);
                }
            }
           
        }
        SavedGameContainer s = new SavedGameContainer();
        s.savedTemplate=temp;
        s.Save("Assets/Resources/Saved Data/"+levelName+".xml");
    }
    public List<string> getSavedLevelNames()
    {
        List<string> levels = new List<string>();
        
       

        return levels;
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Load(string name)
    {
        Instances.gridManager.ClearGrid();
        SavedGameContainer sg = SavedGameContainer.Load("Assets/Resources/Saved Data/" + name + ".xml");
        SaveTemplate s = sg.savedTemplate;
        int size = s.objects.Count;
        for (int i = 0; i < size; i++)
        {
            ObjectTemplate o = s.objects[i];
            GameObject g = (GameObject)Resources.Load("Prefabs/" + o.name);
             GameObject h = (GameObject) Instantiate(g,new Vector3(o.x, 0.5f, o.y),Quaternion.Euler(new Vector3(0, o.rot, 0)));
            h.name = o.name;
            h.transform.parent = GameObject.Find("World Objects").transform;
            h.GetComponent<WorldObject>().IsActive = true;
            Instances.gridManager.setObject(h,o.x,o.y);


            
        }
    }
}
