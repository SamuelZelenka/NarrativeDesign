using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInteraction : MonoBehaviour
{
    Interactible closestInteractible;
    public Interactible ClosestInteractible { get => closestInteractible; }
    [SerializeField] List<Interactible> interactibles;
    void FixedUpdate()
    {
        if (FindClosestInteractible() != null && FindClosestInteractible().CanInteract)
        {
            closestInteractible.ShowInteractIcon(true);
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact") && closestInteractible != null)
        {
            closestInteractible.Interact();
        }
    }
    Interactible FindClosestInteractible()
    {
        closestInteractible = null;
        interactibles = GameObject.FindObjectsOfType<Interactible>().ToList();
        Interactible[] inter = interactibles.ToArray();
        foreach (var item in inter)
        {
            if (!item.CanInteract) interactibles.Remove(item);
        }


        if (interactibles.Count> 0)
        {
            closestInteractible = interactibles[0];
            float dist = Vector3.Distance(transform.position, closestInteractible.transform.position);
            for (int i = 0; i < interactibles.Count; i++)
            {
                if (Vector3.Distance(transform.position, interactibles[i].InteractionPoint) < dist)
                {
                    dist = Vector3.Distance(transform.position, interactibles[i].InteractionPoint);
                    closestInteractible = interactibles[i];
                }
            }
            if (closestInteractible.CanInteract)
                return closestInteractible;
        }
        return null;

    }

    //public void AddInteractible(Interactible interactible)
    //{
    //    if (!interactibles.Contains(interactible))
    //        interactibles.Add(interactible);
    //}
    //public void RemoveInteractible(Interactible interactible)
    //{
    //    if (interactibles.Contains(interactible)) { interactibles.Remove(interactible); }
    //}

}
