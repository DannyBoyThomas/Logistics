using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SaveLayout))]
public class SaveEditor : Editor 
{
    string myString = "Save As...";
    string load = "Level Name...";
    bool showRecipes = false;

   
    public override void OnInspectorGUI()
    {
        myString = GUILayout.TextField(myString);
        load = GUILayout.TextField(load);

        if(GUILayout.Button("Save"))
        {
            if (myString == "Save As..." || myString == "" || myString == null)
            {
                myString = "Save As...";
            }
            else
            {
                Debug.Log("name: " + myString);
                ((SaveLayout)target).Save(myString);
            }
            
        }
        if (GUILayout.Button("Load"))
        {
            if (load == "Level Name..." || load == "" || load == null)
            {
                myString = "Level Name...";
            }
            else
            {
                //Debug.Log("name: " + myString);
                ((SaveLayout)target).Load(load);
            }
        }
        if (GUILayout.Button("Delete All"))
        {
            ((SaveLayout)target).DeleteAll();
        }
        DrawDefaultInspector();
    }

	
   

}
