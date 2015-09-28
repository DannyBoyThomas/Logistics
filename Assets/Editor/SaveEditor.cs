using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SaveLayout))]
public class SaveEditor : Editor 
{
	string myString = "";
    bool showRecipes = false;

   
    public override void OnInspectorGUI()
    {
        myString = GUILayout.TextField(myString);

        if(GUILayout.Button("Save"))
        {
            if (myString == "Save As..." || myString == "" || myString == null)
            {
                myString = "Save As...";
            }
            else
            {
                ((SaveLayout)target).Save(myString);
            }
            
        }
        if (GUILayout.Button("Load"))
        {
            ((SaveLayout)target).Load();
        }
        if (GUILayout.Button("Delete All"))
        {
            ((SaveLayout)target).DeleteAll();
        }
        DrawDefaultInspector();
    }

	
   

}
