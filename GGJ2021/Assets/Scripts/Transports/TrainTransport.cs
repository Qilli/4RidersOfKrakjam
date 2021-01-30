using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTransport : Transport
{
    public override void StartArriving()
    {
        base.StartArriving();
        Debug.Log("Train arriving!");
    }

    public override void StartDeparting()
    {
        base.StartDeparting();
    }
}
