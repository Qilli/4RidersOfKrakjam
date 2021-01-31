using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Map/ExitPoint")]
public class ExitPoint : PointType
{

    public override void onEnter(Person p)
    {
        base.onEnter(p);
    }

    public override void onFinished(Person p)
    {
        base.onFinished(p); 

        p.PersonNavigator.follower.stopPerson();
        base.onEnter(p);
        //m_Speed
        //przyparentuj pod cos
    }

    public override void onExit(Person p)
    {
    }

}
