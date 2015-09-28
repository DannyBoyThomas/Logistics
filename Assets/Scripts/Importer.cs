using UnityEngine;
using System.Collections;

public class Importer : WorldObject
{
    public GameObject Item;
    public ItemAcceptor nextAcceptor;
    public GameObject ImportItem;
    public Transform SpawnPoint;

	void Start () 
    {
	
	}

    public void OnDestroy()
    {
        ImportItem = null;
        if (Item != null)
            Destroy(Item);
    }
	
	public override void WorldUpdate () 
    {
        
                if (nextAcceptor == null)
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
                else
                {
                    if(Item == null)    
                    Item = GameObject.Instantiate(ImportItem, SpawnPoint.position, Quaternion.identity) as GameObject;
                    Item.hideFlags = HideFlags.HideInHierarchy;
                }

                     Vector3 itempos = transform.position + transform.forward;
                    itempos = new Vector3(Mathf.RoundToInt(itempos.x),0, Mathf.RoundToInt(itempos.z));

                    if (Item == null)
                        return;

                    if (nextAcceptor.CanAccept(Item.GetComponent<Item>(), (int)itempos.x, (int)itempos.z))
                    {
                        if (nextAcceptor.AcceptItem(Item))
                        {
                            Instances.moneyManager.AddFunds(-Item.GetComponent<Item>().BuyValue, transform.position);
                            this.Item = null;
                        }
                    }


    }

}
