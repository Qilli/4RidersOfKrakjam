using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds data of prisoners and allows creating a prisoner lookalike
/// </summary>

[CreateAssetMenu(fileName = "PrisonerReference")]
public class PrisonerReference : ScriptableObject
{
    [SerializeField] List<GameObject> _prisonerElements = new List<GameObject>();

    public List<GameObject> GetPrisonerElements()
    {
        return _prisonerElements;
    }
}
