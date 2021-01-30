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

    public Node getRandomConnectedNode()
    {
        try
        {
            return m_Connections[Random.Range(0, m_Connections.Count)];
        }
        catch
        {
            Debug.LogError("Fucked up on node: " + this.gameObject.name);
            return null;
        }
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
