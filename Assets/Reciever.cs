using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : ConnectableType {

    public bool recieverIsPowered;

	// Use this for initialization
	void Start () {
		
	}

    public override void ReceivePower(Connection fromConnection)
    {
        Vector3 recievedPowerDir = fromConnection.OutputDirection;
        LightScript recievedLight = fromConnection.Light;

        Connection poweredInput = FindCorrespondingInputConnectionFromOutput(fromConnection);
        PowerConnection(poweredInput, recievedLight);
        recievedLight.AddConnection(poweredInput);

        BroadcastMessage("Powered");
        recieverIsPowered = true;
    }

    public override void StopRecievingPower(Connection connection)
    {
        Debug.Log("Reciever stopped recieving power");
        base.StopRecievingPower(connection);
        recieverIsPowered = false;
        BroadcastMessage("Unpowered");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
