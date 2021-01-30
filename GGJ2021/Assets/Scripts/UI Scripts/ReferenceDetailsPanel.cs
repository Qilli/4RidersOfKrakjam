using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferenceDetailsPanel : MonoBehaviour
{
    [SerializeField] Text _nameText = null;
    [SerializeField] Text _ageText = null;
    [SerializeField] Text _offenseText = null;
    [SerializeField] Text _specialText = null;
    [SerializeField] RawImage _renderImage = null;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void DisplayDetailsOfReference(PrisonerReference reference, Camera _renderCam)
    {
        _nameText.text = reference.Name;
        _ageText.text = reference.Age.ToString();
        _offenseText.text = reference.Offense;
        _specialText.text = reference.GetSpecialAttributesNames();
        _renderImage.texture = _renderCam.targetTexture;
        this.gameObject.SetActive(true);
    }
}
