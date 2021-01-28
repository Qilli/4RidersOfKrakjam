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

    private void OnMouseDown()
    {
        // Set up this person to be caught by police forces

        Debug.Log(this.gameObject.name + "got clicked!");
    }
}
