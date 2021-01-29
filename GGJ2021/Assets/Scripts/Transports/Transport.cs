using UnityEngine;

public abstract class Transport : MonoBehaviour
{
    [Header("Locations")]
    [SerializeField] Transform _placeToSpawn = null;
    [SerializeField] Transform _placeToArrive = null;
    [SerializeField] Transform _placeToDepart = null;

    public bool IsDeparted = false;

    public abstract void Arrive();
    public abstract void Depart();
}
