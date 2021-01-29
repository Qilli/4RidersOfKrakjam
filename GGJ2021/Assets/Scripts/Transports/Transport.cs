using System;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [Header("Locations")]
    [SerializeField] protected Transform _placeToSpawn = null;
    [SerializeField] protected Transform _placeToArrive = null;
    [SerializeField] protected Transform _placeToDepart = null;

    [SerializeField] float _speed = 1.0f;
    [SerializeField] float _departureDelay = 5.0f;
    [SerializeField] MainGameManager _gameManager = null;

    [Header("Runtime")]
    [SerializeField] List<Person> _passengersOnBoard = new List<Person>();

    protected bool _isArrived = false;
    protected bool _canDepart = false;

    public bool IsDeparted { get { return _canDepart; } }
    public bool IsArrived { get { return _isArrived; } }

    private void Awake()
    {
        this.enabled = false;
    }

    public virtual void BoardTransport(Person person)
    {
        person.transform.SetParent(this.transform);
        _passengersOnBoard.Add(person);
    }

    public virtual void Arrive()
    {
        this.transform.position = _placeToSpawn.position;

        this.enabled = true;
        
        // Notify persons transport is arriving
    }

    public virtual void Depart()
    {
        _canDepart = true;

        foreach(var p in _passengersOnBoard)
        {
            if(p.IsPrisoner)
            {
                _gameManager.NotifyPrisonerEscaped(p, this);
            }
        }
    }

    public virtual void Update()
    {
        MoveToArrival();

        MoveToDeparture();
    }

    private void MoveToArrival()
    {
        if (!IsArrived)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, _placeToArrive.position, _speed * Time.deltaTime);

            if(this.transform.position == _placeToArrive.position)
            {
                Debug.Log("Transport Arrived at destination!");
                _isArrived = true;

                Invoke(nameof(Depart), _departureDelay);
                // Notify passengers we arrived
            }
        }
    }

    private void MoveToDeparture()
    {
        if(_canDepart)
        {
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
