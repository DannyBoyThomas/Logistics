using UnityEngine;
using System.Collections;

public class Splitter : WorldObject, ItemAcceptor
{
    public GameObject Item;
    public bool hasReachedMiddle = false;
    public Transform Center;
    public ItemAcceptor nextAcceptor;

    public int side = 0;
    Vector3[] sides = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };

    public void OnDestroy()
    {
        if (Item != null)
            Destroy(Item);
    }

	void Start () 
    {
	
	}
	
	public override void WorldUpdate () 
    {
        if (side > 3)
            side = 0;

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

            if(hasReachedMiddle)
            {
                if (nextAcceptor != null)
                {
                    Vector3 pos = transform.position + transform.TransformDirection(sides[side]);

                    pos = new Vector3(Mathf.RoundToInt(pos.x),0, Mathf.RoundToInt(pos.z));

                    if (nextAcceptor.CanAccept(Item.GetComponent<Item>(), (int)pos.x, (int)pos.z))
                    {
                        if (nextAcceptor.AcceptItem(Item))
                        {
                            this.Item = null;
                            hasReachedMiddle = false;
                            nextAcceptor = null;
                            side++;
                        }
                        else
                        {
                            nextAcceptor = null;
                            side++;
                        }
                    }
                    else
                    {
                        nextAcceptor = null;
                        side++;
                    }
                }
                else
                {
                    Vector2 furnacePos = Instances.gridManager.GetCoords(this.gameObject);
                    Vector3 pos = new Vector3(furnacePos.x, 0, furnacePos.y) + transform.TransformDirection(sides[side]);

                    pos = new Vector2(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.z));


                    if (Instances.gridManager.getObject(pos) != null)
                    {
                        if (Instances.gridManager.getObject(pos).transform.forward == -transform.TransformDirection(sides[side]))
                        {
                            side++;
                            nextAcceptor = null;
                            return;
                        }

                        Debug.Log("GetObject");
                        GameObject go = Instances.gridManager.getObject(pos);
                        if (go.GetComponent<ItemAcceptor>() != null)
                        {
                            ItemAcceptor acceptor = go.GetComponent<ItemAcceptor>();
                            nextAcceptor = acceptor;
                        }
                    }
                    else
                        side++;
                    
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
