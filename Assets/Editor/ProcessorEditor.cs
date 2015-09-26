using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Processor))]
public class ProcessorEditor : Editor 
{
	string myString = "Hello World";
    bool showRecipes = false;


    public override void OnInspectorGUI()
    {
        OnGUI();
    }

	// Add menu named "My Window" to the Window menu
/*	[MenuItem ("Window/Recipe Editor")]

	static void Init () 
    {
		// Get existing open window or if none, make a new one:
		ProcessorEditor window = (ProcessorEditor)EditorWindow.GetWindow (typeof (ProcessorEditor));
		window.Show();
	}
    */
    Item input, output;
    Vector2 scrollPos = new Vector2();
	void OnGUI () 
    {
        EditorGUILayout.LabelField("Input/Output");
        EditorGUILayout.BeginHorizontal();
        input = EditorGUILayout.ObjectField(input, typeof(Item)) as Item;
        output = EditorGUILayout.ObjectField(output, typeof(Item)) as Item;
        EditorGUILayout.EndHorizontal();

        if(Selection.activeGameObject == null)
            return;

        if(GUILayout.Button("Create Recipe"))
        {

            if(Selection.activeGameObject.GetComponent<Processor>())
            {
                Selection.activeGameObject.GetComponent<Processor>().AddRecipe(new Recipe() { Input = input.ItemName, Output = output.ItemName });
            }
        }

        
        if(Selection.activeGameObject.GetComponent<Processor>() == null)
        return;

        showRecipes = EditorGUILayout.Foldout(showRecipes, "Recipes");

        if (showRecipes)
        {
            Processor process = Selection.activeGameObject.GetComponent<Processor>();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            for (int i = 0; i < process.Recipes.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(string.Format("{0} = {1}", process.Recipes[i].Input, process.Recipes[i].Output));
                if (GUILayout.Button("X"))
                {
                    process.Recipes.RemoveAt(i);
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

	}

}
