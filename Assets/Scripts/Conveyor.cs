using UnityEngine;
using System.Collections;

public class Conveyor : MonoBehaviour, ItemAcceptor
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
                    Vector3 pos = transform.position + transform.forward;
                    pos = new Vector3(Mathf.RoundToInt(pos.x),0, Mathf.RoundToInt(pos.z));

                    if (nextAcceptor.CanAccept(Item.GetComponent<Item>(), (int)pos.x, (int)pos.z))
                    {
                        if (nextAcceptor.AcceptItem(Item))
                        {
                            this.Item = null;
                            hasReachedMiddle = false;
                        }
                    }
                }
                else
                {
                    Vector3 pos = transform.position + transform.forward;
                    pos = new Vector3(Mathf.RoundToInt(pos.x), 0, Mathf.RoundToInt(pos.z));
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



    public bool CanAccept(Item item, int x, int y)
    {
        if (Item == null)
            return true;

        return false;
    }
}
