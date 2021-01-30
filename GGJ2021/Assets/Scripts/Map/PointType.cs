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

    public PositionType.PositionsType positionType;

    public Type TypeOfPoint;

    public float waitTime = 0f;
    [SerializeField]
    protected bool randomizeTime = true;
    public float minTimeWait = -5f;
    public float maxTimeWait = 5f;

    /// <summary>
    /// Postać zatrzyma się na tym node do konca gry
    /// </summary>
    public bool finalNode = false;

    /// <summary>
    /// Maksymalna ilość osób mogąca oczekiwać w punkcie
    /// </summary>
    public int maxPersons = 1;
    


    public virtual void onEnter(Person p)
    {
        //toimplement
        //p.setWalking(false) ;
        Debug.Log("Postać przeszła przez punkt");

    }

    public virtual void onFinished(Person p)
    {
        //toimplement
        p.setWalking(false);
        /*
        if (!p.PersonNavigator.currentNode.personsQueued.Contains(p))
            p.PersonNavigator.currentNode.personsQueued.Add(p);
        Debug.Log("Postać zakonczyla sciezke w punkcie");
        */
    }

    public virtual void onExit(Person p)
    {
        //toimplement
        p.setWalking(true);
        Debug.Log("Postać wyszła z punktu");
        p.PersonNavigator.currentNode.personsQueued.Remove(p);
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
