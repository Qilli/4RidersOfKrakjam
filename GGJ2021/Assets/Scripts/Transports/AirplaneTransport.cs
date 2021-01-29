using UnityEngine;

public class AirplaneTransport : Transport
{
    public override void Arrive()
    {
        base.Arrive();
        Debug.Log("Airplane arriving!");
    }

    public override void Depart()
    {
        base.Depart();
    }
}
