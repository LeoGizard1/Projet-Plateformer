using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector2[] cameraPositions;

    void Start()
    {
        cameraPositions = new Vector2[2];
        cameraPositions[0] = new Vector2(0,0);
        cameraPositions[1] = new Vector2(-30,10);
    }

    public void moveCamera(int posID)
    {
        Camera.main.transform.position = cameraPositions[posID];
    }
}
