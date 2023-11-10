using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3[] cameraPositions;
    private Coroutine coroutine;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        cameraPositions = new Vector3[3];
        cameraPositions[0] = new Vector3(-1f, 0f, -10f);
        cameraPositions[1] = new Vector3(-36.5f, 12.5f, -10f);
        cameraPositions[2] = new Vector3(-50f, -8f, -10f);
        coroutine = StartCoroutine(MoveCameraCoroutine(0, 0));
    }

    public void MoveCamera(int posId, float duration)
    {
        StopCoroutine(coroutine);
        coroutine = StartCoroutine(MoveCameraCoroutine(posId, duration));
    }

    private IEnumerator MoveCameraCoroutine(int posID, float transitionTime)
    {
        if (posID > cameraPositions.Length) yield return null;

        var goalPos = cameraPositions[posID];
        var initPos = cam.transform.position;
        var distance = (goalPos - initPos).magnitude;
        if (distance < 0.05f) yield return null;

        if (transitionTime <= 0f) transitionTime = 0.01f;
        var timer = 0f;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;

            cam.transform.position = Vector3.Lerp(initPos, goalPos, timer / transitionTime);
            yield return null;
        }

        yield return null;
    }
}