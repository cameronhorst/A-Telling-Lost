using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSegment : ConnectableType {



    // Use this for initialization
    void Start()
    {

    }

    public override void ReceivePower(Connection fromConnection)
    {
        Vector3 recievedPowerDir = fromConnection.OutputDirection;
        LightScript recievedLight = fromConnection.Light;

        Connection poweredInput = FindCorrespondingInputConnectionFromOutput(fromConnection);
        PowerConnection(poweredInput, recievedLight);
        recievedLight.AddConnection(poweredInput);

        Connection outputToPower = FindCorrespondingOutputConnectionForDirection(recievedPowerDir);
        recievedLight.AddLightBeamSegment(outputToPower, null);
        StartCoroutine(WaitingForLightBeam(outputToPower, recievedLight));
    }

    IEnumerator WaitingForLightBeam(Connection output, LightScript light)
    {
        while (!light.ready)
        {
            yield return null;
        }

        PowerConnection(output, light);
        SendPowerFrom(output);
        yield break;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
