using UnityEngine;
using System.Collections;

public class ScrollOffset : MonoBehaviour 
{

	void Update () 
    {
        GetComponent<Renderer>().sharedMaterial.mainTextureOffset = new Vector2(0, ConveyorInfo.ConveyorTextureOffset);
	}
}
