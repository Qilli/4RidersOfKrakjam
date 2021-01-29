using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerReferenceDisplayer : MonoBehaviour
{
    [SerializeField] PrisonerReferenceButton _buttonPrefab = null;
    [SerializeField] ReferenceDetailsPanel _detailsPanel = null;

    [SerializeField] List<PrisonerReferenceButton> _buttonsSpawned = new List<PrisonerReferenceButton>();

    public void CreatePrisonerReferences(List<PrisonerReference> references)
    {
        foreach(var r in references)
        {
            Debug.Log("Creatng buttons for references: " + r.name);
            var newButton = Instantiate(_buttonPrefab, this.transform);
            newButton.Init(r, _detailsPanel);
            _buttonsSpawned.Add(newButton);
        }
    }
}
