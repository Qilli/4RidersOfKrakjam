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
    [SerializeField] bool _isMinigameCamera = false;
    [SerializeField] float _minigameCameraOrtho = 10.0f;

    [SerializeField]
    CCTVCameraBoundary boundary;

    [SerializeField] AudioClip _clip = null;


    public Transform Position { get => position; }
    public CCTVCameraBoundary Boundary { get => boundary; }
    public bool IsMinigameCamera { get => _isMinigameCamera; }
    public float MinigameCameraOrtho { get => _minigameCameraOrtho; }
    public AudioClip CameraAudioClip { get => _clip; }

    private void Awake()
    {
        position = GetComponent<Transform>();
    }
}
