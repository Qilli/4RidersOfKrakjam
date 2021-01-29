using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceDetailsPanel : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void DisplayDetailsOfReference(PrisonerReference reference)
    {


        this.gameObject.SetActive(true);
    }
}
