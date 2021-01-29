using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Klasa odpowiadająca za działanie kamery CCTV (Gameplay)
/// </summary>
public class CCTVCamera : MonoBehaviour
{
    //Dodaj oznakowanie kamery? int albo cuś


    Transform position;


    public Transform Position { get => position; }

    private void Awake()
    {
        position = GetComponent<Transform>();
    }

}
