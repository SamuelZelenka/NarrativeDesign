using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interactible : MonoBehaviour
{


    //The point from the interaction check happens from
    public Vector3 InteractionPoint { get => transform.position + (transform.rotation * interactionOriginPointOffset); }


    //The offset from the objects origin the check happens from 
    [SerializeField] Vector3 interactionOriginPointOffset;
    //Interaction icon offset
    [SerializeField] Vector3 interactVisualiserObjectOffset;
    public Vector3 InteractVisualiserObjectOffset
    {
        get
        {
            if (rotateInteractionVisWithRotation)
            { return transform.position + transform.rotation * interactVisualiserObjectOffset; }
            else return transform.position + interactVisualiserObjectOffset;
        }
    }
    [Tooltip("Should the interaction icon be affected by the objects rotation")]
    [SerializeField] bool rotateInteractionVisWithRotation = true;
    [Tooltip("How close the player has to be to the interactionpoint in order to be able to interact")]
    [SerializeField] protected float interactionRadius = .5f;
    [SerializeField]
    protected bool interactible = true;
    [Tooltip("Leave on everything")]
    [SerializeField] protected LayerMask interactionMask = ~0;
    GameObject interactVisualiser;
    public UnityEvent interactionEvent = new UnityEvent();

    [SerializeField] Sprite customInteractionIcon;



    public virtual bool CanInteract
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

    public virtual void Start()
    {
        InstantiateInteractVisualiser();
    }
    //Creates the interaction visualiser in the scene
    void InstantiateInteractVisualiser()
    {
        interactVisualiser = Instantiate(Resources.Load("InteractionCanvas") as GameObject);
        interactVisualiser.transform.position = InteractVisualiserObjectOffset;
        interactVisualiser.SetActive(false);
        if (customInteractionIcon != null)
        {
            interactVisualiser.GetComponent<BillScript>().image.sprite = customInteractionIcon;
        }
    }

    //Show or hide the interaction icon
    public void ShowInteractIcon(bool show)
    {
        switch (show)
        {
            case (true):
                interactVisualiser.transform.position = InteractVisualiserObjectOffset;
                interactVisualiser.SetActive(true);
                break;
            case (false):
                interactVisualiser.SetActive(false);
                break;

        }
    }
    public virtual void OnDestroy()
    {
        Destroy(interactVisualiser);
    }
    public virtual void OnDisable()
    {
        interactVisualiser.SetActive(false);
    }


}
