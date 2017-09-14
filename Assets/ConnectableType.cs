using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectableType : MonoBehaviour {

    [System.Serializable]
    public class Connection
    {
        public Connection(Transform connectPoint, ConnectableType connected)
        {
            trans = connectPoint;
            connectedObject = connected;
            OutputDirection = connectPoint.forward;
            InputDirection = -OutputDirection;
        }

        public Transform trans;
        public ConnectableType connectedObject;
        public Vector3 OutputDirection;
        public Vector3 InputDirection;
        public bool powered = false;
        public LightScript Light = null;
    }
    public List<Connection> Connections = new List<Connection>();


    LayerMask PuzzleMask;
    public float hitSphereRadius = 0.35f;

    private void Awake()
    {
        PuzzleMask = LayerMask.GetMask("Puzzle");
        FindConnections();
    }

    // Use this for initialization
    void Start ()
    {

	}

    public virtual void PowerConnection(Connection c, LightScript light)
    {
        c.powered = true;
        c.Light = light;
    }

    public virtual void ReceivePower(Connection fromConnection)
    {
        Connection poweredInput = FindCorrespondingInputConnectionFromOutput(fromConnection);
        PowerConnection(poweredInput, fromConnection.Light);
    }

    public virtual void StopRecievingPower(Connection connection)
    {
        connection.powered = false;
        connection.Light = null;
    }

    public virtual void SendPowerFrom(Connection fromConnection)
    {
        if(fromConnection.connectedObject != null)
        {
            fromConnection.connectedObject.ReceivePower(fromConnection);
        }
    }

    void FindConnections()
    {
        int numChildren = transform.childCount;
        for(int i = 0; i < numChildren; i++)
        {
            Collider[] hitPieces;
            hitPieces = Physics.OverlapSphere(transform.GetChild(i).position, hitSphereRadius, PuzzleMask);
            foreach(Collider c in hitPieces)
            {
                if(c.gameObject != this.gameObject)
                {
                    Connections.Add(new Connection(transform.GetChild(i), c.gameObject.GetComponent<ConnectableType>()));
                }
            }
        }
    }

    //Find the correct input corresponding to the given connections's output
    public virtual Connection FindCorrespondingInputConnectionFromOutput(Connection fromConnection)
    {
        ConnectableType fromPiece = fromConnection.connectedObject;
        Vector3 powerDir = fromConnection.OutputDirection;

        foreach (Connection c in Connections)
        {
            if (Vector3.Angle(powerDir, c.InputDirection) < 10)
            {
                return c;
            }
        }
        Debug.Log("No Valid Inputs Found!");
        return null;
    }

    public virtual Connection FindCorrespondingOutputConnectionForDirection(Vector3 powerDir)
    {
        foreach (Connection c in Connections)
        {
            if (Vector3.Angle(powerDir, c.OutputDirection) < 10f)
            {
                return c;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
