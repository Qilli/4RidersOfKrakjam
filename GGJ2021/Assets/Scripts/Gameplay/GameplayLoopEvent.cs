using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName ="Gameplay/GameplayLoopEvent")]
public class GameplayLoopEvent : ScriptableObject
{
    UnityEvent desiredEvent;
    /// <summary>
    /// Pole odpowiadające za wywoływanie eventu
    /// </summary>
    [SerializeField]
    bool Enabled = true;

    public void Invoke()
    {
        if(Enabled)
        desiredEvent.Invoke();
    }

    public void Register(UnityAction action )
    {
        desiredEvent.AddListener(action);
    }

    public void UnRegister(UnityAction action)
    {
        desiredEvent.RemoveListener(action);
    }

}
