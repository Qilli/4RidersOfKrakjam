using UnityEngine;

public class Person : MonoBehaviour
{
    [SerializeField] bool _isPrisoner = false;
    [SerializeField] PrisonerReference _prisonerReference = null;

    public bool IsPrisoner { get { return _isPrisoner; } }

    public void SetAsPrisoner(PrisonerReference reference)
    {
        _isPrisoner = true;
        _prisonerReference = reference;
    }
}
