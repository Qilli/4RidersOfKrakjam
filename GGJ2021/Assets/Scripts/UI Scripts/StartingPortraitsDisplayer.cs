using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Displays portraits of persons we should look for in this game sessions
/// </summary>
public class StartingPortraitsDisplayer : MonoBehaviour
{
    [SerializeField] List<PrisonerReference> _references = new List<PrisonerReference>();

    public void DisplayStartingPortraits(List<PrisonerReference> references)
    {
        _references = references;
    }
}
