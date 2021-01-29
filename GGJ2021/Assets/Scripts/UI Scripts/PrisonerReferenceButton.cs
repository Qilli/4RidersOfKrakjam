using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains and displays prisoner detail info and his protrait
/// </summary>
public class PrisonerReferenceButton : MonoBehaviour
{
    [SerializeField] PrisonerReference _reference = null;
    [SerializeField] Transform _portraitParent = null;

    public PrisonerReference Reference { get { return _reference; } }

    ReferenceDetailsPanel _detailsPanel = null;

    public void Init(PrisonerReference reference, ReferenceDetailsPanel detailsPanel)
    {
        _detailsPanel = detailsPanel;
        _reference = reference;
        CreatePrisonerPortrait();
    }

    public void DisplayDetails()
    {
        Debug.Log("Displaying details");
        _detailsPanel.DisplayDetailsOfReference(_reference);
    }

    private void CreatePrisonerPortrait()
    {
        // Create new child GO
        foreach(var e in _reference.GetPrisonerElements())
        {
            Instantiate(e, _portraitParent);
        }

        // Spawn prion uniform element OR spawn last seen elements
    }
}
