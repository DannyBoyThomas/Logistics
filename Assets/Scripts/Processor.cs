using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Processor : MonoBehaviour
{
    public List<Recipe> Recipes = new List<Recipe>();
    public void AddRecipe(Recipe recipe)
    {
        Recipes.Add(recipe);
    }

    public Item FindOutput(Item input)
    {
        for (int i = 0; i < Recipes.Count; i++)
        {
            if (Recipes[i].Input.ItemName == input.ItemName)
                return Recipes[i].Output;
        }
        return null;
    }
}

[System.Serializable]
public class Recipe
{
    public Item Input, Output;
}
