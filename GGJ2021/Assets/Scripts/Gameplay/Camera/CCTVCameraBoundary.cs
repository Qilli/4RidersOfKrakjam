using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="CCTV/CameraBoundary")]
public class CCTVCameraBoundary : ScriptableObject
{
    public Vector2 upperLeftBoundary;
    public Vector2 upperRightBoundary;
}
