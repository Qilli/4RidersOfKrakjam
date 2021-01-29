using System;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    [Header("Prisoner Status")]
    [SerializeField] bool _isPrisoner = false;
    [SerializeField] PrisonerReference _prisonerReference = null;

    [SerializeField] List<GameObject> _personElements = new List<GameObject>();

/*    [Header("Clothes")] // Switch it just to a list of decorations to be spawned?
    [SerializeField] GameObject _trousers = null;
    [SerializeField] GameObject _shirts = null;
    [SerializeField] GameObject _shirtDecor = null;
    [SerializeField] GameObject _glasses = null;
    [SerializeField] GameObject _inHandDecors = null;
    [SerializeField] GameObject _neckDecor = null;
    [SerializeField] GameObject _headDecor = null;

    [Header("Body")]
    [SerializeField] GameObject _skinTone = null;
    [SerializeField] GameObject _leftHandTatoo = null;
    [SerializeField] GameObject _rightHandTatoo = null;
    [SerializeField] GameObject _hairAndBeard = null;*/

    [Header("Other")]
    [SerializeField] PoliceResponder _policeResponder = null; // Maybe inject this

    public bool IsPrisoner { get { return _isPrisoner; } }
    public PrisonerReference PrisonerReference { get { return _prisonerReference; } }

    Vector3 _startingPosition = new Vector3();
    CatchingConfirmator _confirmator = null;

    public void SetAsPrisoner(PrisonerReference reference)
    {
        _isPrisoner = true;
        _prisonerReference = reference;
    }

    public void SetPersonLooks(List<GameObject> elements)
    {
        SpawnAndParentLookElements(elements);
    }

    private void SpawnAndParentLookElements(List<GameObject> elements)
    {
        foreach(var e in elements)
        {
            if (!e) continue;

            _personElements.Add(Instantiate(e, this.transform));
        }
    }

    private void Awake()
    {
        _policeResponder = FindObjectOfType<PoliceResponder>();
    }

    private void Start()
    {
        _startingPosition = this.transform.position;
    }

    private void OnMouseDown()
    {
        _confirmator.DisplayConfirmationPrompt(this);
    }

    public void GetCaught()
    {
        _policeResponder.CatchThatGuy(this);
    }

    internal void GetBackToStart()
    {
        this.transform.position = _startingPosition;

        // Start walking again to POIs
        Debug.Log("Person walking again");
    }

    internal void SetConfirmator(CatchingConfirmator confirmator)
    {
        _confirmator = confirmator;
    }
}
