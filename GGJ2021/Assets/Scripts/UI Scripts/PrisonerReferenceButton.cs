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
    [SerializeField] GameObject _escapedLabel = null;

    [Header("Runtime")]
    [SerializeField] Person _prisoner = null;

    Camera _renderCam = null;
    AudioPlayer _player = null;

    public PrisonerReference Reference { get { return _reference; } }

    ReferenceDetailsPanel _detailsPanel = null;

    public void Init(PrisonerReference reference, ReferenceDetailsPanel detailsPanel, Camera renderCam, Person prisoner, AudioPlayer player)
    {
        _detailsPanel = detailsPanel;
        _reference = reference;
        _renderCam = renderCam;
        _prisoner = prisoner;
        _player = player;
    }

    public void DisplayDetails()
    {
        _detailsPanel.DisplayDetailsOfReference(_reference, _renderCam);
    }

    internal void MarkAsEscaped(Person escapedPerson)
    {
        if (escapedPerson == _prisoner)
        {
            _escapedLabel.SetActive(true);
        }
    }

    internal void MarkAsCaught(Person caughtPrisoner)
    {
        if(caughtPrisoner == _prisoner)
        {
            _crossout.SetActive(true);
        }
    }

    public void PlayClickSound(AudioClip clip)
    {
        _player.PlayUIClick(clip);
    }
}
