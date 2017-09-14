using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRInputWrapper : MonoBehaviour {

    public static VRInputWrapper input;

    public PlatformControlsWrapper LeftController;
    public PlatformControlsWrapper RightController;

    private void Awake()
    {
        if (input == null)
        {
            input = this;
        }
        else if(input != null)
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
