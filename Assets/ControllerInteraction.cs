using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInteraction : MonoBehaviour {

    [HideInInspector]
    public PlatformControlsWrapper controller;
    [HideInInspector]
    public List<GameObject> InteractablesInRange = new List<GameObject>();
    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public Vector3 angularVelocity;

    public GameObject controllerModel;
    public Transform holdPoint;
    GameObject heldObject;
    public FixedJoint holdJoint;


    private void Awake()
    {
        controller = GetComponent<PlatformControlsWrapper>();
        holdJoint = GetComponent<FixedJoint>();
    }

    private int CompareDistance(GameObject g1, GameObject g2)
    {
        float d1 = Vector3.Distance(gameObject.transform.position, g1.transform.position);
        float d2 = Vector3.Distance(gameObject.transform.position, g2.transform.position);

        return d1.CompareTo(d2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Interactable")
        {
            if (!InteractablesInRange.Contains(other.gameObject))
            {
                InteractablesInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            if (InteractablesInRange.Contains(other.gameObject))
            {
                InteractablesInRange.Remove(other.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
    void HoldObject(GameObject go)
    {
        if (go.GetComponent<InteractableObject>())
        {
            go.GetComponent<InteractableObject>().Hold(this);
            return;
        }

        controllerModel.SetActive(false);
        go.transform.position = holdPoint.position;
        go.transform.rotation = holdPoint.rotation;
        Rigidbody rb = go.GetComponent<Rigidbody>();
        holdJoint.connectedBody = rb;
    }

    void ThrowObject(GameObject go)
    {
        if (go.GetComponent<InteractableObject>())
        {
            go.GetComponent<InteractableObject>().Release(this);
            heldObject = null;
            return;
        }

        controllerModel.SetActive(true);
        holdJoint.connectedBody = null;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.velocity = controller.velocity;
        rb.angularVelocity = controller.angularVeloctiy;
        heldObject = null;
    }

 

    // Update is called once per frame
    void Update ()
    {
        if (InteractablesInRange.Count > 0)
        {
            InteractablesInRange.Sort(CompareDistance);

            if (controller.TriggerPressedDown())
            {
                heldObject = InteractablesInRange[0];
                HoldObject(InteractablesInRange[0]);
            }

           
        }
        if (controller.TriggerReleased() && heldObject != null)
        {
            ThrowObject(heldObject);
            heldObject = null;
        }

    }
}
