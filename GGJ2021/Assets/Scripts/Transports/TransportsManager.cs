using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        ShuffleTransports();
        SetNewTimer();
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

        // Handle close to arrival case ONCE

        if (_arrivalTimer > _arrivalThreshold && _transports.Count > 0)
        {
            SetNewTimer();

            Debug.Log("Something arrives");
            _transports[0].Arrive();
            _transports.RemoveAt(0);
        }

        NotifyOfLastTransport();
    }

    private void NotifyOfLastTransport()
    {
        if (_transports.Count == 1)
        {
            _gameManager.SetLastTransport(_transports[0]);
        }
    }

    private void SetNewTimer()
    {
        if(_minMaxArrivalInterval.Count > 0)
        {
            var newMinMax = _minMaxArrivalInterval[0];
            _minMaxArrivalInterval.RemoveAt(0);
            _arrivalTimer = 0;
            _arrivalThreshold = UnityEngine.Random.Range(newMinMax.x, newMinMax.y);
        }
        else
        {
            Debug.Log("Out of timers");
            // End game when this transport leaves
        }
    }
}
