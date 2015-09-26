using UnityEngine;
using System.Collections;

public class Splitter : WorldObject, ItemAcceptor
{
    public GameObject Item;
    public bool hasReachedMiddle = false;
    public Transform Center;
    public ItemAcceptor nextAcceptor;

    int side = 0;

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
                            transform.forward *= -1;
                            nextAcceptor = null;
                        }
                        else
                        {
                            transform.forward *= -1;
                            nextAcceptor = null;
                        }
                    }
                    else
                    {
                        transform.forward *= -1;
                        nextAcceptor = null;
                    }
                }
                else
                {
                    Vector2 furnacePos = Instances.gridManager.GetCoords(this.gameObject);
                    Vector3 pos = new Vector3(furnacePos.x, 0, furnacePos.y) + (Vector3)transform.forward;

                    pos = new Vector2(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));


                    if (Instances.gridManager.getObject(pos) != null)
                    {
                        Debug.Log("GetObject");
                        GameObject go = Instances.gridManager.getObject(pos);
                        if (go.GetComponent<ItemAcceptor>() != null)
                        {
                            ItemAcceptor acceptor = go.GetComponent<ItemAcceptor>();
                            nextAcceptor = acceptor;
                        }
                    }
                    else
                        transform.forward *= -1;
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
