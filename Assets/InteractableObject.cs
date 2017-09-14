using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour {

    public bool beingHeld;

    [HideInInspector]
    public FixedJoint holdJoint;
    [HideInInspector]
    public ControllerInteraction heldController;
    [HideInInspector]
    public Rigidbody rb;

	// Use this for initialization
	public void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
    public virtual void Hold(ControllerInteraction _controller)
    {
        _controller.controllerModel.SetActive(false);
        beingHeld = true;
        heldController = _controller;
        transform.position = _controller.holdPoint.position;
        transform.rotation = _controller.holdPoint.rotation;
        holdJoint = _controller.holdJoint;
        holdJoint.connectedBody = rb;
    }

    public virtual void Release(ControllerInteraction _controller)
    {
        _controller.controllerModel.SetActive(true);
        _controller.holdJoint.connectedBody = null;
        holdJoint = null;
        rb.velocity = _controller.velocity;
        rb.angularVelocity = _controller.angularVelocity;
        beingHeld = false;
        heldController = null;
    }

	// Update is called once per frame
	void Update ()
    {
        
	}
}
