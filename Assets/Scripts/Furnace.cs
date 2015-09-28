using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Processor))]

public class Furnace : WorldObject, ItemAcceptor
{
    public GameObject Item;
    public bool hasReachedMiddle = false;
    public Transform Center;
    public Transform OutputPoint;
    public ItemAcceptor nextAcceptor;
    public bool processed = false;
    public float MaxProcess = 3;
    public float Process = 0;

    public int ProductionCost = 10;


    Processor processor;

    public void OnDestroy()
    {
        if (Item != null)
            Destroy(Item);
    }

    void Start()
    {
        processor = GetComponent<Processor>();
    }

    public override void WorldUpdate()
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
                if(!processed)
                    Process += Time.deltaTime;

                if (Process >= MaxProcess || processed)
                {
                    if (!processed)
                    {
                        
                        Item item = processor.FindOutput(Item.GetComponent<Item>());
                        if (item != null)
                        {
                            Destroy(Item);
                            Item = GameObject.Instantiate(item.gameObject, OutputPoint.position, OutputPoint.rotation) as GameObject;
                            processed = true;
                            Process = 0;
                        }
                    }
                    
                    if (nextAcceptor != null && processed)
                    {
                        Vector2 furnacePos = Instances.gridManager.GetCoords(this.gameObject);
                        Vector3 pos = new Vector3(furnacePos.x, 0, furnacePos.y) + (Vector3)transform.forward;

                        pos = new Vector3(Mathf.RoundToInt(pos.x), 0, Mathf.RoundToInt(pos.z));

                        if (nextAcceptor.CanAccept(Item.GetComponent<Item>(), (int)pos.x, (int)pos.z))
                        {
                            if (nextAcceptor.AcceptItem(Item))
                            {
                                processed = false;
                                this.Item = null;
                                hasReachedMiddle = false;
                            }
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
                            print("No Object at pos");
                    }
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
        if(processor.FindOutput(item) != null && Item == null)
            return true;

        return false;
    }
}
