using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {
    public bool IsActive;

    public void Update()

    {
        if(IsActive)
            WorldUpdate();
    }

    public virtual void WorldUpdate() { }
}
