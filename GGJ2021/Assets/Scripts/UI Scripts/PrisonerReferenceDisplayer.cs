using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerReferenceDisplayer : MonoBehaviour
{
    [SerializeField] PrisonerReferenceButton _buttonPrefab = null;

    [SerializeField] List<PrisonerReferenceButton> _buttonsSpawned = new List<PrisonerReferenceButton>();

    public void CreatePrisonerReferences(List<PrisonerReference> references)
    {
        foreach(var r in references)
        {
            var newButton = Instantiate(_buttonPrefab, this.transform);
            newButton.SetPrisonerInfo(r);
            _buttonsSpawned.Add(newButton);
        }
    }
}
