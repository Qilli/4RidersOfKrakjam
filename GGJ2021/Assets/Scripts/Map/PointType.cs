using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "Map/PointType")]
public class PointType : ScriptableObject
{
    public enum Type
    {
        DEFAULT,
        SPAWN,
        POI,
        EXIT
    }

    public Type TypeOfPoint;

    public float waitTime = 0f;
    [SerializeField]
    bool randomizeTime = true;
    public float minTimeWait = -5f;
    public float maxTimeWait = 5f;


    public void onEnter(Person p)
    {
        //toimplement
        Debug.Log("Postać przeszła przez punkt");
    }

    public void onFinished(Person p)
    {
        //toimplement
        Debug.Log("Postać zakonczyla sciezke w punkcie");
    }

    public void onExit(Person p)
    {
        //toimplement
        Debug.Log("Postać wyszła z punktu");
    }

    float getRandomTime()
    {
        return Random.Range(minTimeWait, maxTimeWait);
    }

    internal float GetTimeWait()
    {
        if (randomizeTime)
        {
            return getRandomTime();
        }
        return waitTime;
    }
}
