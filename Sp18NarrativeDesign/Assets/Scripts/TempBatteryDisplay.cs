using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBatteryDisplay : MonoBehaviour
{
    PlayerXray PlayerXray
    {
        get => GameObject.FindObjectOfType<PlayerXray>();
    }
    [SerializeField] GameObject displayBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerXray != null)
        {
            displayBar.transform.localScale = new Vector3(PlayerXray.BatteryPercentage / 100, displayBar.transform.localScale.y, displayBar.transform.localScale.z);
        }
    }
}
