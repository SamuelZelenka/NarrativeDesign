using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXray : MonoBehaviour
{
    [SerializeField] float batteryPercentage = 0;
    [SerializeField] float batteryChargeSpeed = 1;
    public float BatteryPercentage { get => batteryPercentage; }
    [SerializeField] float orbActiveTime = 5;
    [SerializeField] float growthRate = 1;
    [SerializeField] float lightGrowthRate = 1;
    [SerializeField] float lerp = 1;
    [SerializeField] Light light;
    [SerializeField] GameObject sphereObject;
    [SerializeField] Vector3 sphereObjectOrigin = new Vector3();
    bool scanning;
    [SerializeField] float scanTime = 3;
    float scanTimer = 0;
    float scanRange = 0;
    [SerializeField] float scanrangeShrinker = -10;
    float intesity;

    float activeTimer = 0;
    Xray[] xRayObjects = new Xray[0];

    [SerializeField] MeshRenderer chargedDisplay;
    [SerializeField] Material chargedMaterial;
    [SerializeField] Material notChargedMaterial;

    private void Start()
    {
        sphereObject.transform.parent = null;
        intesity = light.intensity;
    }
    public void PickUpBattery(float percentage)
    {

        batteryPercentage = Mathf.Clamp(batteryPercentage + percentage, 0, 100);

    }
    private void Update()
    {
        if (Input.GetButtonDown("Xray"))
        {
            Xray();
        }
        if (activeTimer < orbActiveTime)
        {
            sphereObject.transform.localScale += new Vector3(1, 1, 1) * growthRate * Time.deltaTime;
            light.range += growthRate * Time.deltaTime;
            activeTimer += Time.deltaTime * 1;
            light.intensity = Mathf.Lerp(light.intensity, 0, Time.deltaTime * lerp);

        }
        else
        {
            sphereObject.SetActive(false);
        }
        if (scanning)
        {
            if (scanTimer < scanTime)
            {
                scanTimer += Time.deltaTime;
                scanRange += growthRate * Time.deltaTime;
                foreach (var item in xRayObjects)
                {
                    if (Vector3.Distance(sphereObject.transform.position, item.transform.position) <= scanRange/2 - scanrangeShrinker)
                    {

                        item.ShowThroughWalls(true);
                    }
                }
            }
            else
            {
                scanning = false;
                foreach (var item in xRayObjects)
                {


                        item.ShowThroughWalls(false);
                    
                }
            }
        }


        batteryPercentage += batteryChargeSpeed * Time.deltaTime;
        if (batteryPercentage >= 100)
        {
            fullyCharged();
        }


    }

    void fullyCharged()
    {
        chargedDisplay.material = chargedMaterial;
    }
    void unCharged()
    {
        chargedDisplay.material = notChargedMaterial;
    }
    void Xray()
    {
        if (batteryPercentage >= 100 && !scanning)
        {
            unCharged();
            batteryPercentage = 0;
            sphereObject.transform.position = transform.position + sphereObjectOrigin;
            light.range = 0;
            activeTimer = 0;
            sphereObject.transform.localScale = Vector3.zero;
            sphereObject.SetActive(true);
            light.intensity = intesity;
            scanning = true;
            scanTimer = 0;
            scanRange = 0;
            xRayObjects = GameObject.FindObjectsOfType<Xray>();
            foreach (var item in xRayObjects)
            {


                item.ShowThroughWalls(false);

            }
        }



    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + sphereObjectOrigin, .25f);
    }
}


