using UnityEngine;

public class BusTransport : Transport
{
    public override void Arrive()
    {
        base.Arrive();
        Debug.Log("Bus arriving!");
    }

    public override void Depart()
    {
        base.Depart();
    }
}
