using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intersection : ConnectableType {


    public Transform orbSnapPoint;

    [System.Serializable]
    public class NewLightBeam
    {
        public NewLightBeam(Connection c, LightScript l)
        {
            connection = c;
            light = l;
        }

        public Connection connection;
        public LightScript light;
    }

    public List<NewLightBeam> newLightBeams = new List<NewLightBeam>();

    // Use this for initialization
    void Start ()
    {

	}

    public override void ReceivePower(Connection fromConnection)
    {
        Vector3 recievedPowerDir = fromConnection.OutputDirection;
        LightScript recievedLight = fromConnection.Light;

        Connection poweredInput = FindCorrespondingInputConnectionFromOutput(fromConnection);
        PowerConnection(poweredInput, recievedLight);
        recievedLight.AddLightBeamSegment(poweredInput, new Vector3[1] {transform.position });
    }



    public void CheckOrbOrientation(OrbScript Orb)
    {
        Connection outConnection = Orb.Output(Connections);
        if(outConnection != null)
        {
            foreach(NewLightBeam lb in newLightBeams)
            {
                Destroy(lb.light.gameObject);
            }
            newLightBeams.Clear();
            StartCoroutine(CreatingOutputLight(outConnection));
            
        }
    }

    public override void StopRecievingPower(Connection connection)
    {
        base.StopRecievingPower(connection);
        foreach(NewLightBeam lb in newLightBeams)
        {
           
        }
    }


    public void DestroyOutputLightBeams()
    {
        foreach (NewLightBeam lb in newLightBeams)
        {
            lb.light.killBeam();
        }
        newLightBeams.Clear();
    }
    

    IEnumerator CreatingOutputLight(Connection c)
    {
        LightScript light = LightBeamManager.instance.CreateLightBeam(transform.position);
        yield return null;
        light.initializeBeam(transform.position);
        newLightBeams.Add(new NewLightBeam(c, light));
        yield return null;
        light.AddLightBeamSegment(c, null);

        while (!light.ready)
        {
            yield return null;
        }
        PowerConnection(c, light);
        SendPowerFrom(c);
        yield break;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
