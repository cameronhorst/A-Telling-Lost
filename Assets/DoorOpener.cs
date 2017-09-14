using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour {

    public Animator Door;

	// Use this for initialization
	void Start () {
		
	}
	
    public void Powered()
    {
        Door.SetBool("Open", true);
    }

    public void Unpowered()
    {
        Door.SetBool("Open", false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
