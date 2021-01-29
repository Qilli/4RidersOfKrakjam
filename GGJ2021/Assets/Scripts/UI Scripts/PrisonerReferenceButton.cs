using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains and displays prisoner detail info and his protrait
/// </summary>
public class PrisonerReferenceButton : MonoBehaviour
{
    [SerializeField] PrisonerReference _reference = null;

    public PrisonerReference Reference { get { return _reference; } }

    public void SetPrisonerInfo(PrisonerReference reference)
    {
        // Spawn prisoner portrait from given elements
        _reference = reference;
    }

    public void DisplayDetails()
    {
        Debug.Log("Displaying details");
    }
}
