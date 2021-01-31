using System;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [Header("Locations")]
    [SerializeField] protected Transform _placeToSpawn = null;
    [SerializeField] protected Transform _placeToArrive = null;
    [SerializeField] protected Transform _placeToDepart = null;

    [Header("Params")]
    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _departureDelay = 5.0f;
    [SerializeField] MainGameManager _gameManager = null;
    [SerializeField] PositionType.PositionsType _type;

    [Header("Runtime")]
    [SerializeField] List<Person> _passengersOnBoard = new List<Person>();

    [Header("Audio")]
    [SerializeField] AudioPlayer _player = null;
    [SerializeField] AudioClip _announcmentsClip = null;
    [SerializeField] AudioClip _arrivalClip = null;
    [SerializeField] AudioClip _departureClip = null;

    protected bool _ArrivdeAndWaiting = false;
    protected bool _canDepart = false;

    public bool IsDeparted { get { return _canDepart; } }
    public bool IsArrived { get { return _ArrivdeAndWaiting; } }

    private void Awake()
    {
        this.enabled = false;
        persons = FindObjectsOfType<Person>();
    }

    public virtual void BoardTransport(Person person)
    {
        //person.transform.SetParent(this.transform);
        if (person.ExitNodes.Contains(person.PersonNavigator.follower.lastNode))
        {
            person.gameObject.SetActive(false);
        }

        _passengersOnBoard.Add(person);
    }

    public virtual void StartArriving()
    {
        this.transform.position = _placeToSpawn.position;
        this.enabled = true;

        if (_announcmentsClip)
            _player.PlayTransportAudio(_announcmentsClip);

        var persons = FindObjectsOfType<Person>();

        foreach (var p in persons)
        {
            p.TransportWillAriveSoon(_type);
        }
    }

    public virtual void StartDeparting()
    {
        _canDepart = true;

        foreach(var p in _passengersOnBoard)
        {
            if(p.IsPrisoner)
            {
                _gameManager.NotifyPrisonerEscapedWithTransport(p, this);
            }
        }

        if (_departureClip)
            _player.PlayTransportAudio(_departureClip);
    }

    public virtual void Update()
    {
        MoveToArrival();
        //handleGetPersonsInside();
        MoveToDeparture();
    }

    private void MoveToArrival()
    {
        if (!IsArrived)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _placeToArrive.position, _speed * Time.deltaTime);

            if(this.transform.position == _placeToArrive.position)
            {
                _ArrivdeAndWaiting = true;

                Invoke(nameof(StartDeparting), _departureDelay);

                var persons = FindObjectsOfType<Person>();

                foreach (var p in persons)
                {
                    p.TransportHasArrived(_type, this);
                }

                if (_arrivalClip)
                    _player.PlayTransportAudio(_arrivalClip);
            }
        }
    }

    Person[] persons;
    /*
    void handleGetPersonsInside()
    {
        if (_ArrivdeAndWaiting)
        {
            foreach (var p in persons)
            {
                if(p == null)
                {
                    return;
                }
                    p.TransportHasArrived(_type, this);
            }
        }
    }
    */

    private void MoveToDeparture()
    {
        if(_canDepart)
        {
            _ArrivdeAndWaiting = false;
            this.transform.position = Vector3.MoveTowards(this.transform.position, _placeToDepart.position, _speed * Time.deltaTime);

            if(this.transform.position == _placeToDepart.position)
            {
                this.gameObject.SetActive(false);

                foreach(var p in _passengersOnBoard)
                {
                    if(!p.IsPrisoner)
                    {
                        p.GetBackToStart();
                    }
                }
            }
        }
    }
}
