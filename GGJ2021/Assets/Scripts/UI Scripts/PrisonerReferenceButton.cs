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

    Camera _renderCam = null;

    public PrisonerReference Reference { get { return _reference; } }

    ReferenceDetailsPanel _detailsPanel = null;

    public void Init(PrisonerReference reference, ReferenceDetailsPanel detailsPanel, Camera renderCam)
    {
        _detailsPanel = detailsPanel;
        _reference = reference;
        _renderCam = renderCam;
        CreatePrisonerPortrait();
    }

    public void DisplayDetails()
    {
        _detailsPanel.DisplayDetailsOfReference(_reference, _renderCam);
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
