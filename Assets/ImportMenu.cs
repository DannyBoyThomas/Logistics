using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImportMenu : MonoBehaviour 
{

    public Importer importer;

    public Item[] Items;

    public Dropdown dropdown;

    public CanvasGroup group;

    public void Start()
    {
        dropdown.options.Clear();
        for(int i = 0; i < Items.Length; i++)
            dropdown.options.Add(new Dropdown.OptionData(Items[i].ItemName + " $" + Items[i].BuyValue));

        Hide();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Hide();
    }

    public void Set()
    {
        importer.ImportItem = Items[dropdown.value].gameObject;
        Hide();
    }

    public void Hide()
    {
        group.alpha = 0;
        group.interactable = false;
    }

    public void Show()
    {
        group.alpha = 1;
        group.interactable = true;
    }

}
