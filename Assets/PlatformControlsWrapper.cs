using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.VR;

public class PlatformControlsWrapper : MonoBehaviour {

    public enum ControlScheme
    {
        Vive,
        Oculus,
    }

    public enum Hand
    {

    }

    public Hand heldHand;
    public ControlScheme System = ControlScheme.Vive;

    public Vector3 velocity;
    public Vector3 angularVeloctiy;
    public int controllerIndex;
    SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device;
    ControllerInteraction interactionController;

    //General Controls / Cross Platform

    public bool TriggerPressed()
    {
        return device.GetPress(EVRButtonId.k_EButton_SteamVR_Trigger);
    }

    public bool TriggerPressedDown()
    {
        return device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Trigger);
    }

    public bool TriggerReleased()
    {
        return device.GetPressUp(EVRButtonId.k_EButton_SteamVR_Trigger);
    }

    public float TriggerValue()
    {
        return device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x;
    }

    public bool JoystickPressRight()
    {
        if(System == ControlScheme.Vive)
        {
            return (device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.6f &&
                    device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad));
        }

        return false;
    }

    public bool JoystickPressLeft()
    {
        if (System == ControlScheme.Vive)
        {
            return (device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.6f &&
                    device.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad));
        }

        return false;
    }

    //Vive Controls


    //Oculus Controls


    // Use this for initialization
    void Start ()
    {
	    if(System == ControlScheme.Vive)
        {
            trackedObject = GetComponent<SteamVR_TrackedObject>();
        }
        interactionController = GetComponent<ControllerInteraction>();
	}

    private void FixedUpdate()
    {
        if (System == ControlScheme.Vive)
        {
            controllerIndex = (int)GetComponent<SteamVR_TrackedObject>().index;
            device = device = SteamVR_Controller.Input(controllerIndex);
        }

        velocity = device.velocity;
        angularVeloctiy = device.angularVelocity;

        interactionController.velocity = velocity;
        interactionController.angularVelocity = angularVeloctiy;
    }

    // Update is called once per frame
    void Update ()
    {
	    if(System == ControlScheme.Vive)
        {
            controllerIndex = (int)GetComponent<SteamVR_TrackedObject>().index;
            device = device = SteamVR_Controller.Input(controllerIndex);
        }
       
    }
}
