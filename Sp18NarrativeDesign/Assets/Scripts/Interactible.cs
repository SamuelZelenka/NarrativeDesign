using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public Vector3 InteractionPoint { get => transform.position + (transform.rotation * interactionOriginPointOffset); }
    [SerializeField] Vector3 interactionOriginPointOffset;
    [SerializeField] Vector3 interactVisualiserObjectOffset;
    public Vector3 InteractVisualiserObjectOffset
    {
        get
        {
            if (rotateInteractionVisWithRotation)
                return transform.position + transform.rotation * interactVisualiserObjectOffset;
            else return transform.position + interactionOriginPointOffset;
        }
    }
    [SerializeField] bool rotateInteractionVisWithRotation = true;

    [SerializeField] float interactionRadius = .5f;
    [SerializeField]
    bool interactible = true;
    [SerializeField] LayerMask interactionMask = ~0;
    GameObject interactVisualiser;
    public UnityEvent interactionEvent = new UnityEvent();
    public bool CanInteract
    {
        get
        {
            if (interactible)
            {
                Collider[] objs = Physics.OverlapSphere(InteractionPoint, interactionRadius, interactionMask);
                foreach (Collider item in objs)
                {
                    if (item.gameObject.CompareTag("Player"))
                    {
                        return true;
                    }
                }
                return false;

            }
            else return false;
        }
    }

    public virtual void Interact()
    {
        interactionEvent.Invoke();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!CanInteract || GameObject.FindObjectOfType<PlayerInteraction>().ClosestInteractible != this)
        {
            ShowInteractIcon(false);
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(InteractVisualiserObjectOffset, new Vector3(.5f, .5f, .5f));
        Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawSphere(InteractionPoint, interactionRadius);
    }

    void Start()
    {
        InstantiateInteractVisualiser();
    }
    void InstantiateInteractVisualiser()
    {
        interactVisualiser = Instantiate(Resources.Load("InteractionCanvas") as GameObject);
        interactVisualiser.transform.position = transform.position + transform.rotation * interactVisualiserObjectOffset;
        interactVisualiser.SetActive(false);
    }
    public void ShowInteractIcon(bool show)
    {
        switch (show)
        {
            case (true):
                interactVisualiser.transform.position = transform.position + transform.rotation * interactVisualiserObjectOffset;
                interactVisualiser.SetActive(true);
                break;
            case (false):
                interactVisualiser.SetActive(false);
                break;

        }
    }
    void OnDestroy()
    {
        Destroy(interactVisualiser);
    }



}
