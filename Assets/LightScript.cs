using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightScript : MonoBehaviour {

    LineRenderer LR;
    float flowSpeed;
    public bool ready;

    public List<ConnectableType.Connection> Connections = new List<ConnectableType.Connection>();
    public List<Vector3> Points = new List<Vector3>();

    // Use this for initialization
    void Start ()
    {
        LR = GetComponent<LineRenderer>();
        flowSpeed = LightBeamManager.instance.LightFlowSpeed;
	}
	
    public void AddConnection(ConnectableType.Connection c)
    {
        Connections.Add(c);
    }

    public void AddLightBeamSegment(ConnectableType.Connection connection, Vector3[] positions)
    {
        ready = false;
        StartCoroutine(AddingLightBeamSegment(connection, positions));
    }

    public void initializeBeam(Vector3 pos)
    {
        LR.positionCount = 1;
        LR.SetPosition(0, pos);
    }

    public void killBeam()
    {
        foreach(ConnectableType.Connection c in Connections)
        {
            c.connectedObject.StopRecievingPower(c);
        }
        Destroy(gameObject);
    }

    IEnumerator AddingLightBeamSegment(ConnectableType.Connection connection, Vector3[] positions)
    {
        Vector3 connectionPosition = connection.trans.position;

        if(positions != null)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                Vector3 lastPos = LR.GetPosition(LR.positionCount - 1);
                float distanceToNextPos = Vector3.Distance(lastPos, positions[i]);
                LR.positionCount = LR.positionCount + 1;
                float flowTime = flowSpeed * distanceToNextPos;
                int positionIndex = LR.positionCount - 1;
                float t = 0;
                Points.Add(positions[i]);

                while (t <= flowTime)
                {
                    t += Time.deltaTime;
                    LR.SetPosition(positionIndex, Vector3.Lerp(lastPos, positions[i], t / flowTime));
                    yield return null;
                }
            }
        }
        else
        {
            Vector3 lastPos = LR.GetPosition(LR.positionCount - 1);
            float distanceToNextPos = Vector3.Distance(lastPos, connectionPosition);
            LR.positionCount = LR.positionCount + 1;
            float flowTime = flowSpeed * distanceToNextPos;
            int positionIndex = LR.positionCount - 1;
            float t = 0;
            Points.Add(connectionPosition);

            while (t <= flowTime)
            {
                t += Time.deltaTime;
                LR.SetPosition(positionIndex, Vector3.Lerp(lastPos, connectionPosition, t / flowTime));
                yield return null;
            }
        }
        
     

        Connections.Add(connection);

        ready = true;
        yield break;
    }



	// Update is called once per frame
	void Update () {
		
	}
}
