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


    [SerializeField]
    CCTVCameraBoundary boundary;
    


    public Transform Position { get => position; }
    public CCTVCameraBoundary Boundary { get => boundary; }

    private void Awake()
    {
        position = GetComponent<Transform>();
    }

}
