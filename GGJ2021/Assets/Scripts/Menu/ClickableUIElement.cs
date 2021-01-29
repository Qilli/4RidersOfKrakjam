using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableUIElement : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        clickEvent.Invoke();
    }

    [System.Serializable]
    public class onClickEvent : UnityEvent { }

    [SerializeField]
    private onClickEvent clickEvent = new onClickEvent();

}
