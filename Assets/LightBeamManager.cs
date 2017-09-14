using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBeamManager : MonoBehaviour {


    public GameObject LightBeamPrefab;
    public float LightFlowSpeed;

    public static LightBeamManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    public LightScript CreateLightBeam(Vector3 position)
    {
        GameObject lightBeam = (GameObject)Instantiate(LightBeamPrefab, position, Quaternion.identity);
        return lightBeam.GetComponent<LightScript>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
