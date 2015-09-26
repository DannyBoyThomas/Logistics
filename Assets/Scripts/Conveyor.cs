using UnityEngine;
using System.Collections;

public class Conveyor : MonoBehaviour 
{
    public GameObject Item;
    public bool hasReachedMiddle = false;
    public Transform Center;
    public ItemAcceptor nextAcceptor;

	void Start () 
    {
	
	}
	
	void Update () 
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
                if (nextAcceptor != null)
                {
                    if (nextAcceptor.AcceptItem(Item))
                    {
                        this.Item = null;
                        hasReachedMiddle = false;
                    }
                }
                else
                {

                }
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
}
