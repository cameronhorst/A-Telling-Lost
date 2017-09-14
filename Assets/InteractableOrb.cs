using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableOrb : InteractableObject {

    bool snappedToIntersection = false;
    public float unsnapFromIntersectionDistance = .7f;
    Collider col;
    public float snapSpeed = 2f;
    Intersection connectedIntersection = null;

	// Use this for initialization
	void Start ()
    {
        base.Start();
        col = GetComponent<SphereCollider>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Intersection" && beingHeld)
        {
            snappedToIntersection = true;
            StartCoroutine(SnappingToIntersection(other.GetComponent<Intersection>()));
        }
    }

    IEnumerator SnappingToIntersection(Intersection i)
    {
        Transform snapPoint = i.orbSnapPoint;
        Vector3[] snapOrientations = new Vector3[4] {snapPoint.up, -snapPoint.up, snapPoint.right, -snapPoint.right};
        Vector3 bestUp = snapOrientations[0];
        rb.isKinematic = true;
        holdJoint.connectedBody = null;
        connectedIntersection = i;

        while (beingHeld)
        {
            snappedToIntersection = true;
            Vector3 controllerPos = heldController.transform.position;
            float dist = Vector3.Distance(controllerPos, snapPoint.position);

            Vector3 newPos = Vector3.Lerp(transform.position, snapPoint.position, Time.deltaTime * snapSpeed);
            transform.position = newPos;

            float bestAngle = 90;
            bestUp = Vector3.up;
            foreach(Vector3 v in snapOrientations)
            {
                float angle = Vector3.Angle(transform.up, v);
                if(angle < bestAngle)
                {
                    bestAngle = angle;
                    bestUp = v;
                }
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(snapPoint.forward, bestUp), Time.deltaTime * snapSpeed);

            if(dist > unsnapFromIntersectionDistance)
            {
                rb.isKinematic = false;
                transform.position = heldController.holdPoint.position;
                transform.rotation = heldController.holdPoint.rotation;
                holdJoint.connectedBody = rb;
                snappedToIntersection = false;
                connectedIntersection = null;
                yield break;
            }

            yield return null;
        }


        transform.rotation = Quaternion.LookRotation(snapPoint.forward, bestUp);
        transform.position = snapPoint.position;

        yield break;
    }

    public override void Hold(ControllerInteraction _controller)
    {
        if (snappedToIntersection)
        {
            rb.isKinematic = false;
            snappedToIntersection = false;
            connectedIntersection.DestroyOutputLightBeams();
            connectedIntersection = null;
        }
        base.Hold(_controller);
    }

    public override void Release(ControllerInteraction _controller)
    {
        if (snappedToIntersection)
        {
            rb.isKinematic = true;
            connectedIntersection.CheckOrbOrientation(GetComponent<OrbScript>());
        }

        base.Release(_controller);
    }

    public void FinalizePlacement(Intersection i)
    {

    }

    // Update is called once per frame
    void Update ()
    {
        if (beingHeld)
        {
            if (heldController.controller.JoystickPressRight())
            {
                holdJoint.connectedBody = null;
                transform.Rotate(0, 0, 90, Space.Self);
                if (!snappedToIntersection)
                {
                    holdJoint.connectedBody = rb;
                }
            }

            if (heldController.controller.JoystickPressLeft())
            {
                holdJoint.connectedBody = null;
                transform.Rotate(0, 0, -90, Space.Self);
                if (!snappedToIntersection)
                {
                    holdJoint.connectedBody = rb;
                }
            }
        }
    }
}
