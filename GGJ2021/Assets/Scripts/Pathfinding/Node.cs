using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The Node.
/// </summary>
[System.Serializable]
public class Node : MonoBehaviour
{

    /// <summary>
    /// The connections (neighbors).
    /// </summary>
    [SerializeField]
    protected List<Node> m_Connections = new List<Node>();

    [SerializeField]
    public List<Person> personsQueued = new List<Person>();

    public enum NodeType
    {
        Standard,
        POI,
        Sittable,
        Exit,
    }
    public float waitTime = 0f;
    public PointType type;

    /// <summary>
    /// Gets the connections (neighbors).
    /// </summary>
    /// <value>The connections.</value>
    public virtual List<Node> connections
    {
        get
        {
            return m_Connections;
        }
    }

    public Node this[int index]
    {
        get
        {
            return m_Connections[index];
        }
    }

    private void Start()
    {
        foreach(Node n in connections)
        {
            n.connections.Add(this);
        }
    }

    [ContextMenu("Pobierz Node")]
    public Node getRandomConnectedNodeWithoutFullPOI()
    {
        List<Node> availableNodes = new List<Node>();

        for(int i = 0; i < connections.Count; i += 1)
        {
            Debug.Log(connections[i].type);
            if(connections[i].type.TypeOfPoint != PointType.Type.POI || connections[i].personsQueued.Count < connections[i].type.maxPersons)
            {
                availableNodes.Add(connections[i]);
            }
        }

        //Debug.Log(personsQueued.Count + " < " + connections[i].type.maxPersons);
        Debug.Log(availableNodes.Count);
        //randomize
        if (availableNodes.Count <= 0)
        {
            return null;
        }

        return availableNodes[Random.Range(0, availableNodes.Count)];
    }


    public Node getRandomConnectedNode()
    {
        return m_Connections[Random.Range(0, m_Connections.Count)];
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < m_Connections.Count; i += 1)
        {
            Gizmos.DrawLine(gameObject.transform.position, m_Connections[i].transform.position);

        }

    }

}
