using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionPoint : MonoBehaviour {

    public bool powered;
    public ConnectionPoint connectedPoint = null;
    public ConnectableType connectedObject = null;
    public Vector3 Axis = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
