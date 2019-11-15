using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform nodeOne;
    [SerializeField] Transform nodeTwo;
    Vector3 targetNode;
    int currentNode = 1;
    [SerializeField] float speed = 1;
    public void ToggleNode()
    {
        if (currentNode == 1)
        {
            currentNode = 2;
            targetNode = nodeTwo.transform.position;
        }
        else
        {
            currentNode = 1;
            targetNode = nodeOne.transform.position;
        }
    }
    private void Start()
    {
        targetNode = nodeOne.transform.position;
        nodeOne.transform.parent = null;
        nodeTwo.transform.parent = null;
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, targetNode) > 3)
        {
            Vector3 direction = (targetNode - transform.position).normalized;
            transform.position = transform.position + direction * (speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetNode, 1 * Time.deltaTime);
        }
    }

}
