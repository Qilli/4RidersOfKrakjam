using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTransport : Transport
{
    public override void Arrive()
    {
        base.Arrive();
        Debug.Log("Train arriving!");
    }

    public override void Depart()
    {
        base.Depart();
    }
}
