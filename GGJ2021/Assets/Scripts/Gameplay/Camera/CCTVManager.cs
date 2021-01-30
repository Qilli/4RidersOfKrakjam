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

    [SerializeField] AudioPlayer _audioPlayer = null;

    CCTVCamera _lastCamera;

    private void Awake()
    {
        moveGameCameraTo(0);
    }

    public void moveGameCameraTo(int index)
    {
        gameCamera.transform.position = cameras[index].transform.position;
        gameCamera.UsedCCTVCamera = cameras[index];

        _audioPlayer.PlayAmbientSound(cameras[index].CameraAudioClip);
    }

    public void moveGameCameraTo(CCTVCamera cam)
    {
        int index = cameras.FindIndex(x => x == cam);
        moveGameCameraTo(index);
    }

    #region Minigame Camera handling

    public void StoreLastCamera()
    {
        _lastCamera = gameCamera.UsedCCTVCamera;
    }

    public void RestoreToLastCamera()
    {
        moveGameCameraTo(_lastCamera);
    }

    public void MoveToMinigameCamera()
    {
        moveGameCameraTo(3);
    }

    #endregion
}
