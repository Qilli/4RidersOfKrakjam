using UnityEngine;

public class AirplaneTransport : Transport
{
    public override void StartArriving()
    {
        base.StartArriving();
        Debug.Log("Airplane arriving!");
    }

    public override void StartDeparting()
    {
        base.StartDeparting();
    }
}
