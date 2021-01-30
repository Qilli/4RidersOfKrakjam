using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Map/ExitPoint")]
public class ExitPoint : PointType
{

    bool lastCall = false;
    public override void onEnter(Person p)
    {
        base.onEnter(p);
    }

    public override void onFinished(Person p)
    {
        if (!lastCall)
        {
            return;
        }

        p.PersonNavigator.follower.stopPerson();
        base.onEnter(p);
        p.gameObject.SetActive(false); //testowo
        //przyparentuj pod cos
    }

    public override void onExit(Person p)
    {
        Debug.LogError("NIE POWINIEN BYC EXIT");
    }

}
