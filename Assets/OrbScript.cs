using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour {

    public Transform outputDir1;
    public Transform outputDir2;



    public class ConnectionAxis
    {
        public Vector3 In;
        public Vector3 Out;

        public ConnectionAxis(Transform OutDir)
        {
            In = -OutDir.forward;
            Out = OutDir.forward;
        }


    }

    public ConnectionAxis[] Axes = new ConnectionAxis[2];


    public ConnectableType.Connection Output(List<ConnectableType.Connection> Connections)
    {
        List<ConnectableType.Connection> poweredInputs  = new List<ConnectableType.Connection>();
        List<ConnectableType.Connection> potentialOutputs = new List<ConnectableType.Connection>();



        foreach(ConnectableType.Connection c in Connections)
        {
            if (c.powered)
            {
                Debug.Log(c.trans.name);
                poweredInputs.Add(c);
            }
            else
            {
                potentialOutputs.Add(c);
                Debug.Log(c.trans.name);
            }
        }



        foreach (ConnectableType.Connection input in poweredInputs)
        {
            Vector3 Input1 = -outputDir1.forward;
            Vector3 Input2 = -outputDir2.forward;

            Vector3 Output1 = outputDir1.forward;
            Vector3 Output2 = outputDir2.forward;


            float inputDiff1 = Vector3.Angle(input.InputDirection, Input1);
            float inputDiff2 = Vector3.Angle(input.InputDirection, Input2);
            if(inputDiff1 < 10)
            {
                foreach(ConnectableType.Connection Output in potentialOutputs)
                {
                    float outputDiff = Vector3.Angle(Output2, Output.OutputDirection);
                    if(outputDiff < 10)
                    {
                        return Output;
                    }
                }
            }
            else if(inputDiff2 < 10)
            {
                foreach (ConnectableType.Connection Output in potentialOutputs)
                {
                    float outputDiff = Vector3.Angle(Output1, Output.OutputDirection);
                    if (outputDiff < 10)
                    {
                        return Output;
                    }
                }
            }
        }

        Debug.Log("Null!!");
        return null;
    }

	// Use this for initialization
	void Start ()
    {

    }

    // Update is called once per frame
    void Update () {
		
	}
}
