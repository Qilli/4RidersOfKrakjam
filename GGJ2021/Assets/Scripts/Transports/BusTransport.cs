using UnityEngine;

public class BusTransport : Transport
{
    public override void StartArriving()
    {
        base.StartArriving();
        Debug.Log("Bus arriving!");
    }

    public override void StartDeparting()
    {
        base.StartDeparting();
    }
}
