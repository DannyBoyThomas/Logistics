using UnityEngine;
using System.Collections;

public class Importer : MonoBehaviour
{
    public GameObject Item;
    public ItemAcceptor nextAcceptor;
    public GameObject ImportItem;
    public Transform SpawnPoint;

	void Start () 
    {
	
	}
	
	void Update () 
    {
        if (Item != null)
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

                     Vector3 itempos = transform.position + transform.forward;
                    itempos = new Vector3(Mathf.RoundToInt(itempos.x),0, Mathf.RoundToInt(itempos.z));

                    if (nextAcceptor.CanAccept(Item.GetComponent<Item>(), (int)itempos.x, (int)itempos.z))
                    {
                        if (nextAcceptor.AcceptItem(Item))
                        {
                            this.Item = null;
                        }
                    }
            }
        else
        {
            Item = GameObject.Instantiate(ImportItem, SpawnPoint.position, Quaternion.identity) as GameObject;
        }
        
	}

}
