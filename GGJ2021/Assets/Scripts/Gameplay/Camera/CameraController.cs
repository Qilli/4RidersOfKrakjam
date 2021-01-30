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

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
        initCameraMoveSpeed = cameraMoveSpeed;
    }

    public void controlCameraBoundary()
    {

    }

    public void moveCameraToPosition(int index)
    {

    }

    public void moveCameraToPosition()
    {

    }
    public void handleScroll()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);

            if (UsedCCTVCamera != null)
                diffrence = UsedCCTVCamera.transform.position - Camera.main.transform.position;
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);

        float cameraSize = Camera.main.orthographicSize;
        //cameraMoveSpeed = initCameraMoveSpeed * (cameraSize / maxOrtho);
        /*
        if (scroll < 0 && cameraSize <= maxOrtho)
        {
            moveCamera(diffrence.x * cameraSize / maxOrtho, diffrence.y * cameraSize / maxOrtho);
        }
        */
        diffrence = new Vector2(0, 0);

    }

    public void moveCamera(float x, float y)
    {
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

        //Debug.Log(Input.mousePosition);

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
        if (UsedCCTVCamera.IsMinigameCamera) return;


        handleScroll();
        float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");


        moveCameraOnMouse();
        moveCamera(x, y);
    }
}
