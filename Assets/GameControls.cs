using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {

        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameManager._gameManager.StartDecideTeleport();
            StartLaserPointer();
        }
        if (device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad))
        {
            GameManager._gameManager.DecideTeleport();
        }
    }

    void StartLaserPointer ()
    {
        RaycastHit objectHit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position, fwd * 50, Color.green);
        if (Physics.Raycast(transform.position, fwd, out objectHit, 50))
        {
            //do something if hit object ie
            if (objectHit.transform.tag == "TeleLoc")
            {
                GameManager._gameManager.MyDesiredLoc = objectHit.transform;
            }
            else
            {
                GameManager._gameManager.MyDesiredLoc = null;
            }
        }
    }

}
