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
    [SerializeField] GameObject _crossout = null;

    [Header("Runtime")]
    [SerializeField] Person _prisoner = null;

    Camera _renderCam = null;

    public PrisonerReference Reference { get { return _reference; } }

    ReferenceDetailsPanel _detailsPanel = null;

    public void Init(PrisonerReference reference, ReferenceDetailsPanel detailsPanel, Camera renderCam, Person prisoner)
    {
        _detailsPanel = detailsPanel;
        _reference = reference;
        _renderCam = renderCam;
        _prisoner = prisoner;
    }

    public void DisplayDetails()
    {
        _detailsPanel.DisplayDetailsOfReference(_reference, _renderCam);
    }

    internal void MarkAsCaught(Person caughtPrisoner)
    {
        if(caughtPrisoner == _prisoner)
        {
            _crossout.SetActive(true);
        }
    }
}
