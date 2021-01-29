using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds data of prisoners and allows creating a prisoner lookalike
/// </summary>

[CreateAssetMenu(fileName = "PrisonerReference")]
public class PrisonerReference : ScriptableObject
{
    [SerializeField] List<GameObject> _prisonerElements = new List<GameObject>();

    // List of special features of that person with description and effect it applies to the person

    [Header("Prisoner Params")]
    [SerializeField] string _name = string.Empty;
    [SerializeField] string _offense = string.Empty;
    [SerializeField] int _age = 0;

    [SerializeField] List<SpecialAttribute> _specialAttributes = new List<SpecialAttribute>();

    public string Name { get { return _name; } }
    public string Offense { get { return _offense; } }
    public int Age { get { return _age; } }


    public string GetSpecialAttributesNames()
    {
        string result = string.Empty;

        for(int i = 0; i < _specialAttributes.Count; i++)
        {
            result += _specialAttributes[i].NameToDisplay;
            Debug.Log("Displaying attribute: " + _specialAttributes[i].NameToDisplay);

            if (i < _specialAttributes.Count - 1)
                result += ", ";
        }

        return result;
    }

    public void SetSpecialAttributes(List<SpecialAttribute> attributes)
    {
        _specialAttributes.Clear();
        _specialAttributes.AddRange(attributes);
    }

    public List<GameObject> GetPrisonerElements()
    {
        return _prisonerElements;
    }
}
