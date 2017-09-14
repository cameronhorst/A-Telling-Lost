using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Teleport Vars
    private List<Transform> ValidTeleportLoc = new List<Transform>();
    public Transform LocParent;
    public Transform MyCurrentLoc;
    public Transform MyDesiredLoc;
    bool isDecidingTelport;

    public GameObject VRbase;

    public static GameManager _gameManager;

    // Use this for initialization
    void Awake()
    {
        _gameManager = this;
        SetStartingLocation();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetStartingLocation()
    {
        foreach (Transform T in LocParent)
        {
            ValidTeleportLoc.Add(T);
        }

        if (ValidTeleportLoc.Count > 0)
        {
            MyCurrentLoc = ValidTeleportLoc[0];
            VRbase.transform.position = MyCurrentLoc.position;
        }

        DisableTeleportMesh();
    }

    public void DisableTeleportMesh()
    {
        foreach (Transform t in ValidTeleportLoc)
        {
                t.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void EnableTeleportMesh()
    {
        foreach (Transform t in ValidTeleportLoc)
        {
            if (t.GetComponent<MeshRenderer>() != null && t != MyCurrentLoc)
            {
                t.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void StartDecideTeleport()
    {
        if (isDecidingTelport)
        {
            return;
        }

        isDecidingTelport = true;

        EnableTeleportMesh();
    }

    public void DecideTeleport()
    {
        if (MyDesiredLoc != null)
        {
            TeleportTo(MyDesiredLoc);
        }

        MyDesiredLoc = null;
        DisableTeleportMesh();
    }

    void TeleportTo(Transform desiredLoc)
    {
        VRbase.transform.position = new Vector3(desiredLoc.transform.position.x, desiredLoc.transform.position.y - .05f, desiredLoc.transform.position.z);
        MyCurrentLoc = desiredLoc;
        isDecidingTelport = false;
    }
}
