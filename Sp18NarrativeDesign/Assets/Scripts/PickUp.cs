using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp : MonoBehaviour
{

    public GameObject tempParent;
    private GameObject item;
    public bool isHolding = false;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.tag == "Moveable" && Input.GetKey(KeyCode.F) && !isHolding)
        {
            isHolding = true;
            item = other.gameObject;
            item.transform.parent = tempParent.transform;
            item.transform.position = item.transform.parent.position + new Vector3(0,0.5f,0);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = true;
            item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
        else
        {
            if (item != null)
            {
                isHolding = false;
                item.transform.parent = null;
                item.GetComponent<Rigidbody>().useGravity = true;
                item = null;

            }
        }

    }

}

