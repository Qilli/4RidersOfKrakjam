using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrisonerReferenceDisplayer : MonoBehaviour
{
    [SerializeField] PrisonerReferenceButton _buttonPrefab = null;
    [SerializeField] ReferenceDetailsPanel _detailsPanel = null;

    [SerializeField] List<Camera> _renderCams = null;
    [SerializeField] float _camYOffset = -1.0f;

    [SerializeField] List<PrisonerReferenceButton> _buttonsSpawned = new List<PrisonerReferenceButton>();

    public void CreatePrisonerReferences(List<Person> prisoners)
    {
        Debug.Log("Creating prisoner references: " + prisoners.Count);

        int iterator = 0;

        foreach(var prisoner in prisoners)
        {
            var portrait = prisoner.GetPortraitOfPrisoner();

            portrait.transform.SetParent(this.transform);

            var renderCam = _renderCams[iterator];
            iterator++;

            portrait.transform.position = new Vector3(renderCam.transform.position.x, renderCam.transform.position.y + _camYOffset, 0);
            // Move portrait to button

            var newButton = Instantiate(_buttonPrefab, this.transform);
            newButton.Init(prisoner.PrisonerReference, _detailsPanel);
            newButton.GetComponent<RawImage>().texture = renderCam.targetTexture;
            _buttonsSpawned.Add(newButton);
        }
    }
}
