using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : ConnectableType {

    Vector3 powerDirection;
    public Transform origin;
    



    // Use this for initialization
    void Start ()
    {
        powerDirection = origin.forward;
        TurnOnPowerSource();
    }
	
    public void TurnOnPowerSource()
    {
        StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        yield return new WaitForSeconds(1f);
        LightScript lightBeam = LightBeamManager.instance.CreateLightBeam(transform.position);
        yield return null;
        lightBeam.initializeBeam(origin.transform.position);
        yield return null;
        lightBeam.AddLightBeamSegment(Connections[0], null);
        StartCoroutine(WaitingForLightBeam(lightBeam));
    }

    IEnumerator WaitingForLightBeam(LightScript light)
    {
        PowerConnection(Connections[0], light);

        while (!light.ready)
        {
            yield return null;
        }
        Debug.Log("SendingPower");
        SendPowerFrom(Connections[0]);
        yield break;
    }
   

   


	// Update is called once per frame
	void Update () {
		
	}
}
