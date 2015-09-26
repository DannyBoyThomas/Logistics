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
}

[System.Serializable]
public class Recipe
{
    public string Input, Output;
}
