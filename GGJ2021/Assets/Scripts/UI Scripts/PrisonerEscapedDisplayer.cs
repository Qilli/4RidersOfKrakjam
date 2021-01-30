using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerEscapedDisplayer : MonoBehaviour
{
    [SerializeField] float _timeToDisable = 5.0f;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Invoke(nameof(SelfDisable), _timeToDisable);
    }

    void SelfDisable()
    {
        this.gameObject.SetActive(false);
    }
}
