using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Person : MonoBehaviour
{
    [SerializeField] PrisonerReference _prisonerReference = null;
    [Range(0.0f, 100.0f)]
    [SerializeField] float _chanceToEnterTransport = 50;

    [SerializeField] Color _prisonColor;

    [SerializeField] List<GameObject> _personElements = new List<GameObject>();

    [Header("Other")]
    [SerializeField] PoliceResponder _policeResponder = null;
    [SerializeField] PositionType.PositionsType _type;

    [SerializeField] AudioClip _clickedClip = null;


    [Header("Runtime")]
    public bool HasLuggage = false; // Used for settings animator
    [SerializeField] bool _isPrisoner = false;
    [SerializeField] bool _willTakeTransport = false;

    public bool IsEscaped { get { return _isEscaped; } }
    public bool IsPrisoner { get { return _isPrisoner; } }
    public PrisonerReference PrisonerReference { get { return _prisonerReference; } }

    bool _isEscaped = false;
    bool _isCaught = false;
    Animator _animator = null;
    Vector3 _startingPosition = new Vector3();
    CatchingConfirmator _confirmator = null;

    public PersonNavigator PersonNavigator;
    float initScaleX;
    Transport _transport = null;

    public void SetAsPrisoner(PrisonerReference reference)
    {
        _isPrisoner = true;
        _prisonerReference = reference;
    }

    public void SetPersonLooks(List<GameObject> elements)
    {
        SpawnAndParentLookElements(elements);
    }

    internal void StopWalking()
    {

        // Stop navigating
        // Stop listening to transports
        PersonNavigator.follower.stopPerson();
        _animator.SetBool("IsWalking", false);
    }

    public void controlDirection(float x)
    {
        float personPosX = gameObject.transform.position.x;
        Vector3 scale = gameObject.transform.localScale;
        if (x < personPosX)
        {
            gameObject.transform.localScale = new Vector3(-initScaleX, scale.y, scale.z);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(initScaleX, scale.y, scale.z);
        }
    }

    private void SpawnAndParentLookElements(List<GameObject> elements)
    {
        foreach(var e in elements)
        {
            if (!e) continue;

            _personElements.Add(Instantiate(e, this.transform));
        }
    }

    internal void SetEscapeStatus()
    {
        _isEscaped = true;
    }

    private void Awake()
    {
        _policeResponder = FindObjectOfType<PoliceResponder>();
        _animator = GetComponent<Animator>();

        PersonNavigator = GetComponent<PersonNavigator>();
        PersonNavigator.p = this;
        initScaleX = gameObject.transform.localScale.x;
    }

    internal void TransportWillAriveSoon(PositionType.PositionsType type)
    {
        if (type != _type) return;

        if (!_isPrisoner)
        {
            if (UnityEngine.Random.Range(0, 100) < _chanceToEnterTransport)
            {
                _willTakeTransport = true;
                GoToPlatform();
            }
        }
        else
        if(!_isEscaped)
        {
            _willTakeTransport = true;
            GoToPlatform();
        }
    }



    internal void TransportHasArrived(PositionType.PositionsType type, Transport transport)
    {
        if (type != _type) return;

        if(_willTakeTransport && !_isEscaped)
        {
            _transport = transport;
            BoardTransport();
        }
    }

    private void GoToPlatform()
    {
        // Move into designated position of waiting for transport NEAR it.
        Debug.Log("Moving to platform");
    }

    private void BoardTransport()
    {
        // Some logic to navigate us INSIDE transport
        // Call Board transprot on transport when arrived
        Debug.Log("Boarding");
        _transport.BoardTransport(this);
    }

    private void Start()
    {
        _startingPosition = this.transform.position;

        if (HasLuggage)
            _animator.SetBool("HasSuitcase", true);

    }

    public void setWalking(bool status)
    {
        _animator.SetBool("IsWalking", status);
    }

    private void OnMouseDown()
    {
        if (_isCaught) return;
        // Dont allow it, if we are being caught
        var player = FindObjectOfType<AudioPlayer>();
        player.PlayUIClick(_clickedClip);

        _confirmator.DisplayConfirmationPrompt(this);
    }

    public void GetCaught()
    {
        _isCaught = true;
        _policeResponder.CatchThatGuy(this);

        StopWalking();
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
        Destroy(copy.GetComponent<PersonNavigator>());

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
