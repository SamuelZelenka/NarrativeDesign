using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXray : MonoBehaviour
{
    [SerializeField] float batteryPercentage = 0;
    public float BatteryPercentage { get => batteryPercentage; }
    public void PickUpBattery(float percentage)
    {
        
        batteryPercentage = Mathf.Clamp(batteryPercentage + percentage, 0, 100);

    }
}


