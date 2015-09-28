using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class MenuCategory : MonoBehaviour
{
    public RectTransform Panel;

    public float Alpha;

    public bool Open;

    public void Switch()
    {
        
        Open = !Open;
        if(Open)
        {
            foreach (MenuCategory cat in Instances.itemMenu[0].GetComponentsInChildren<MenuCategory>())
            {
                cat.Open = false;
            }
            this.Open = true;
        }
    }

    public void Update()
    {
        if (Open && Alpha < 1)
        {
            Alpha += Time.deltaTime * 5f;
            Panel.GetComponent<CanvasGroup>().interactable = true;
        }

        Panel.GetComponent<CanvasGroup>().alpha = Alpha;

        if (!Open && Alpha > 0)
        {
            Alpha -= Time.deltaTime * 5f;
            Panel.GetComponent<CanvasGroup>().interactable = false;
        }

        

        

    }
}
