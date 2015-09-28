using UnityEngine;
using System.Collections;

public class OutputBox : WorldObject, ItemAcceptor
{
    public GameObject Item;
    public bool hasReachedMiddle = false;
    public Transform Center;
    public ItemAcceptor nextAcceptor;

	void Start () 
    {
	
	}
	
	public override void WorldUpdate () 
    {
        if (Item != null)
        {
            if (!hasReachedMiddle)
            {
                Vector3 pos = Item.transform.position;
                pos = Vector3.MoveTowards(pos, Center.position, ConveyorInfo.GetConveyorSpeed() * Time.deltaTime);
                Item.transform.position = pos;

                if (pos == Center.position)
                    hasReachedMiddle = true;
            }
            else
            {
                
                Instances.moneyManager.AddFunds(Item.GetComponent<Item>().Value, transform.position);
                Destroy(Item.gameObject);
            }
        }
	}

    public bool AcceptItem(GameObject go)
    {
        if (Item == null)
        {
            Item = go;
            hasReachedMiddle = false;
            return true;
        }
        
        return false;
    }



    public bool CanAccept(Item item, int x, int y)
    {
        if (Item == null)
            return true;

        return false;
    }
}
