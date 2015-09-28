using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour 
{
    public enum Speed { Paused = 0, Normal = 1, Fast = 3, Super = 7 };

    public static Speed currentSpeed = Speed.Normal;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            currentSpeed = Speed.Paused;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentSpeed = Speed.Normal;

        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentSpeed = Speed.Fast;

        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentSpeed = Speed.Super;

    }
}
