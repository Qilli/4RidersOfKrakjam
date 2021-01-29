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



    private void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
        initCameraMoveSpeed = cameraMoveSpeed;
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
        Camera.main.transform.position = new Vector2(Camera.main.transform.position.x + x * Time.deltaTime * cameraMoveSpeed, Camera.main.transform.position.y + y*Time.deltaTime*cameraMoveSpeed);

    }

    private void Update()
    {
        handleScroll();
        moveCamera(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
