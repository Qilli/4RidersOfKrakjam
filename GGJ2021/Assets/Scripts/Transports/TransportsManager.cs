using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles transport arrival / departures and displays their time to leave. 
/// Notifies peoples that a transport is leaving
/// </summary>
public class TransportsManager : MonoBehaviour
{
    [SerializeField] List<Transport> _transports = new List<Transport>();

    [SerializeField] List<Vector2> _minMaxArrivalInterval = new List<Vector2>();

    [SerializeField] float _arrivalTimer = 0;
    [SerializeField] float _arrivalThreshold = 0;

    [SerializeField] MainGameManager _gameManager = null;

    [Header("UI")]
    [SerializeField] GameObject _arrivalPanel = null;
    [SerializeField] Text _trainArrivalText = null;
    [SerializeField] Text _busArrivalText = null;
    [SerializeField] Text _planeArrivalText = null;

    bool _countTrainArrival = false;
    bool _countBusArrival = false;
    bool _countPlaneArrival = false;

    [SerializeField] float _busTimer = 0;
    [SerializeField] float _busThreshold = 0;

    [SerializeField] float _trainTimer = 0;
    [SerializeField] float _trainThreshold = 0;

    [SerializeField] float _planeTimer = 0;
    [SerializeField] float _planeThreshold = 0;

    private void Start()
    {
        ShuffleTransports();
        SetNewTimer(_transports[0].Type);
    }

    private void ShuffleTransports()
    {
        System.Random rng = new System.Random();

        int n = _transports.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var temp = _transports[k];
            _transports[k] = _transports[n];
            _transports[n] = temp;
        }
    }

    private void Update()
    {
        _arrivalTimer += Time.deltaTime;

        if (_arrivalTimer > _arrivalThreshold && _transports.Count > 0)
        {
            SetNewTimer(_transports[0].Type);
            _transports[0].StartArriving();
            _transports.RemoveAt(0);
        }

        NotifyOfLastTransport();

        FeedArrivalTimers();
    }

    private void SetTransportTimer(PositionType.PositionsType type)
    {
        if(type == PositionType.PositionsType.Airport)
        {
            _countPlaneArrival = true;
            _planeThreshold = _arrivalThreshold + 10;
        }

        if(type == PositionType.PositionsType.Bus)
        {
            _countBusArrival = true;
            _busThreshold = _arrivalThreshold + 10;
        }

        if(type == PositionType.PositionsType.Train)
        {
            _countTrainArrival = true;
            _trainThreshold = _arrivalThreshold + 10;
        }
    }

    private void FeedArrivalTimers()
    {
        if(_countPlaneArrival)
        {
            _planeTimer += Time.deltaTime;
            _planeArrivalText.text = (_planeThreshold - _planeTimer).ToString("F") + "s";
        }
        else
        {
            _planeArrivalText.text = "DELAYED";
        }

        if(_countBusArrival)
        {
            _busTimer += Time.deltaTime;
            _busArrivalText.text = (_busThreshold - _busTimer).ToString("F") + "s";
        }
        else
        {
            _busArrivalText.text = "DELAYED";
        }

        if (_countTrainArrival)
        {
            _trainTimer += Time.deltaTime;
            _trainArrivalText.text = (_trainThreshold - _trainTimer).ToString("F") + "s";
        }
        else
        {
            _trainArrivalText.text = "DELAYED";
        }

        if(_planeTimer > _planeThreshold && _countPlaneArrival)
        {
            _countPlaneArrival = false;
            _planeArrivalText.text = "NO ARRIVALS SOON";
        }

        if (_trainTimer > _trainThreshold && _countTrainArrival)
        {
            _countTrainArrival = false;
            _trainArrivalText.text = "NO ARRIVALS SOON";
        }

        if (_busTimer > _busThreshold && _countBusArrival)
        {
            _countBusArrival = false;
            _busArrivalText.text = "NO ARRIVALS SOON";
        }
    }

    private void NotifyOfLastTransport()
    {
        if (_transports.Count == 1)
        {
            _gameManager.SetLastTransport(_transports[0]);
        }
    }

    private void SetNewTimer(PositionType.PositionsType type)
    {
        if(_minMaxArrivalInterval.Count > 0)
        {
            var newMinMax = _minMaxArrivalInterval[0];
            _minMaxArrivalInterval.RemoveAt(0);
            _arrivalTimer = 0;
            _arrivalThreshold = UnityEngine.Random.Range(newMinMax.x, newMinMax.y);
            SetTransportTimer(type);

        }
        else
        {
            // End game when this transport leaves
        }
    }
}
