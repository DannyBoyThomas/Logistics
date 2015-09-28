using UnityEngine;
using System.Collections;

public class WorldObject : MonoBehaviour {
    public bool IsActive;
    public int Cost;
    public void Update()

    {
        if (IsActive)
        {
            for (int i = 0; i < (int)TimeManager.currentSpeed; i++)
                WorldUpdate();
        }
    }

    public virtual void WorldUpdate() { }
}
