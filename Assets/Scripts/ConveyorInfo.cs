﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ConveyorInfo : MonoBehaviour
{
    public static float ConveyorTextureOffset = 0;
    public float ConveyorSpeed = 0.6f;
    public Material globalMaterial;

    public static float GetConveyorSpeed()
    {
        return GameObject.FindObjectOfType<ConveyorInfo>().ConveyorSpeed;
    }   

    public void Update()
    {
        ConveyorTextureOffset += ConveyorSpeed * Time.deltaTime * (int)TimeManager.currentSpeed;
        globalMaterial.mainTextureOffset = new Vector2(0, ConveyorTextureOffset);
    }
}
