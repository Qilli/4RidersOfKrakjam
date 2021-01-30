using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Map/POI")]
public class POIPoint : PointType
{


    public override void onEnter(Person p)
    {
        base.onEnter(p);
    }

    public override void onFinished(Person p)
    {
        base.onEnter(p);
        //toimplement
        p.setWalking(false);
        p.setSitting(true);
        Debug.Log("Postać w POI, Siedzi");
    }

    public override void onExit(Person p)
    {
        base.onEnter(p);
        //toimplement
        p.setWalking(true);
        p.setSitting(false);
        Debug.Log("Postać wyszła z POI");
    }

}
