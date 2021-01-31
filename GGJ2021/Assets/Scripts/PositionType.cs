using UnityEngine;

public class PositionType : MonoBehaviour
{
    public enum PositionsType { Bus, Train, Airport}

    
    Node nodeUsed;

    [SerializeField] PositionsType _type;

    public PositionsType Type { get { return _type; } }

    public Node NodeUsed { get => nodeUsed;  }

    public bool BlockSpawn = false;

    private void Awake()
    {
        nodeUsed = GetComponent<Node>();
    }
}
