﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    [Header("Prisoner Status")]
    [SerializeField] bool _isPrisoner = false;

    [SerializeField] PrisonerReference _prisonerReference = null;
    [Range(0.0f, 100.0f)]
    [SerializeField] float _chanceToEnterTransport = 50;

    [SerializeField] Color _prisonColor;

    [SerializeField] List<GameObject> _personElements = new List<GameObject>();

    [Header("Other")]
    [SerializeField] PoliceResponder _policeResponder = null;
    [SerializeField] PositionType.PositionsType _type;

    public bool HasLuggage = false; // Used for settings animator
    public bool IsPrisoner { get { return _isPrisoner; } }
    public PrisonerReference PrisonerReference { get { return _prisonerReference; } }

    Animator _animator = null;
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
        _animator = GetComponent<Animator>();
    }

    internal void TransportHasArrived(PositionType.PositionsType type)
    {
        if (type != _type) return;

        if(!_isPrisoner)
        {
            if (UnityEngine.Random.Range(0, 100) < _chanceToEnterTransport)
            {
                Debug.Log("Boarding transport: " + type);
                BoardTransport();
            }
            else
            {
                Debug.Log("Not interested in boarding: " + type);
            }
        }
        else
        {
            Debug.Log("Escaping as a prisoner via: " + type);
            BoardTransport();
        }
    }

    private void BoardTransport()
    {
        // Some logic to navigate us into transport
        // Call Board transprot on transport when arrived
    }

    private void Start()
    {
        _startingPosition = this.transform.position;

        if (HasLuggage)
            _animator.SetBool("HasSuitcase", true);

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

    internal void SetPositionType(PositionType.PositionsType type)
    {
        _type = type;
    }

    public GameObject GetPortraitOfPrisoner()
    {
        var copy = Instantiate(this.gameObject);

        Destroy(copy.GetComponent<Rigidbody2D>());
        Destroy(copy.GetComponent<Person>());
        Destroy(copy.GetComponent<BoxCollider2D>());
        Destroy(copy.GetComponent<Animator>());

        var components = copy.GetComponentsInChildren<BodyPart>();

        foreach(var bp in components)
        {
            if(bp.Type == BodyPart.BodyPartType.Luggage || bp.Type == BodyPart.BodyPartType.Backpack || bp.Type == BodyPart.BodyPartType.ShirtDecor)
            {
                bp.gameObject.SetActive(false);
            }

            if(bp.Type == BodyPart.BodyPartType.Shirt || bp.Type == BodyPart.BodyPartType.Trousers)
            {
                bp.GetComponent<SpriteRenderer>().color = _prisonColor;
            }
        }

        // Recolor person to prison color (orange)

        return copy;
    }
}
