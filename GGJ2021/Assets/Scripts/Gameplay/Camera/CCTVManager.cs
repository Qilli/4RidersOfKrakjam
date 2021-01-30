using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Klasa odpowiadająca za obsługiwanie kamer CCTV (Gameplay) - tj zmiana
/// </summary>

public class CCTVManager : MonoBehaviour
{
    [SerializeField]
    CameraController gameCamera;

    public List<CCTVCamera> cameras = new List<CCTVCamera>();

    private void Awake()
    {
        moveGameCameraTo(0);
    }

    public void moveGameCameraTo(int index)
    {
        gameCamera.transform.position = cameras[index].transform.position;
        gameCamera.UsedCCTVCamera = cameras[index];
    }

    public void moveGameCameraTo(CCTVCamera cam)
    {
        int index = cameras.FindIndex(x => x == cam);
        moveGameCameraTo(index);
    }

}
