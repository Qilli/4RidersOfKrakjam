using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Klasa odpowiadająca za działanie kamery
/// </summary>
public class CameraController : MonoBehaviour
{
    public float zoomSpeed = 1;
    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;

    public float cameraMoveSpeed = 10f;
    float initCameraMoveSpeed = 10f;
    Vector2 diffrence = new Vector2();

    public CCTVCamera UsedCCTVCamera;
    Camera cam;

    [SerializeField]
    float cameraMouseMoveOffset = 5f;

    [Header("Audio")]
    [SerializeField] AudioPlayer _player = null;
    [SerializeField] AudioClip _zoomInClip = null;
    [SerializeField] AudioClip _zoomOutClip = null;

    [SerializeField] AudioClip _movingClip = null;

    float _zoomSoundLength = 0;
    float _zoomSoundTimer = 0;
    bool _canPlayZoomSound = true;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        _zoomSoundLength = Mathf.Max(_zoomInClip.length, _zoomOutClip.length);
    }
    private void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
        initCameraMoveSpeed = cameraMoveSpeed;
    }

    public void handleScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);

            if(scroll > 0.0f && _canPlayZoomSound)
            {
                _canPlayZoomSound = false;
                _zoomSoundTimer = 0.0f;
                PlayZoomInSound();
            }
            else
            if(_canPlayZoomSound)
            {
                _canPlayZoomSound = false;
                _zoomSoundTimer = 0.0f;
                PlayZoomOutSound();
            }


            if (UsedCCTVCamera != null)
                diffrence = UsedCCTVCamera.transform.position - Camera.main.transform.position;
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

        float cameraSize = Camera.main.orthographicSize;
        diffrence = new Vector2(0, 0);
    }

    private void PlayZoomOutSound()
    {
        _player.PlayCameraZoomingSound(_zoomOutClip);
    }

    private void PlayZoomInSound()
    {
        _player.PlayCameraZoomingSound(_zoomInClip);
    }

    public void moveCamera(float x, float y)
    {
        if(!Mathf.Approximately(x, 0.0f) || !Mathf.Approximately(y, 0.0f))
        {
            _player.PlayCameraMovingSound(_movingClip);
        }

        float recalculatedX = cam.transform.position.x + x * Time.deltaTime * cameraMoveSpeed;
        float recalculatedY = cam.transform.position.y + y * Time.deltaTime * cameraMoveSpeed;

        if (!checkCameraInsideXBoundary(x))
        {
            recalculatedX = cam.transform.position.x;
        }

        if (!checkCameraInsideYBoundary(y))
        {
            recalculatedY = cam.transform.position.y;
        }
        cam.transform.position = new Vector3(recalculatedX, recalculatedY, cam.transform.position.z);
    }


    public void moveCameraOnMouse()
    {
        float x = 0;
        float y = 0;

        if (Input.mousePosition.x < cameraMouseMoveOffset)
        {
            x = -cameraMoveSpeed;
        }
        else if (Input.mousePosition.x > Screen.width - cameraMouseMoveOffset)
        {
            x = cameraMoveSpeed;
        }

        if (Input.mousePosition.y < cameraMouseMoveOffset)
        {
            y = -cameraMoveSpeed;
        }
        else if (Input.mousePosition.y > Screen.height - cameraMouseMoveOffset)
        {
            y = cameraMoveSpeed;
        }

        moveCamera(x * 0.1f, y * 0.1f);
    }


    bool checkCameraInsideXBoundary(float x)
    {
        return UsedCCTVCamera.Boundary.upperLeftBoundary.x < cam.transform.position.x + x * Time.deltaTime * cameraMoveSpeed &&
             UsedCCTVCamera.Boundary.upperRightBoundary.x > cam.transform.position.x + cam.rect.width + x * Time.deltaTime * cameraMoveSpeed;
    }

    bool checkCameraInsideYBoundary(float y)
    {
        return UsedCCTVCamera.Boundary.upperLeftBoundary.y < cam.transform.position.y + y * Time.deltaTime * cameraMoveSpeed &&
              UsedCCTVCamera.Boundary.upperRightBoundary.y > cam.transform.position.y + cam.rect.height + y * Time.deltaTime * cameraMoveSpeed;
    }

    private void Update()
    {
        if (UsedCCTVCamera.IsMinigameCamera)
        {
            Camera.main.orthographicSize = UsedCCTVCamera.MinigameCameraOrtho;
            return;
        }


        handleScroll();
        float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");


        moveCameraOnMouse();
        moveCamera(x, y);

        _zoomSoundTimer += Time.deltaTime;
        if(_zoomSoundTimer > _zoomSoundLength)
        {
            _canPlayZoomSound = true;
        }
    }
}
