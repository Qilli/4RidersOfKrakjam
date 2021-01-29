using UnityEngine;

public class PositionType : MonoBehaviour
{
    public enum PositionsType { Bus, Train, Airport}

    [SerializeField] PositionsType _type;

    public PositionsType Type { get { return _type; } }
}
