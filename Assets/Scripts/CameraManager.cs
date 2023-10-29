using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Vector3[] cameraPositions;
    private Coroutine coroutine;

    void Start()
    {
        cameraPositions = new Vector3[3];
        cameraPositions[0] = new Vector3(-1f, 0f, -10f);
        cameraPositions[1] = new Vector3(-36.5f, 12.5f, -10f);
        cameraPositions[2] = new Vector3(-50f, -8f, -10f);
        coroutine = StartCoroutine(moveCameraCoroutine(0, 0));
    }

    public void moveCamera(int posId, float duration)
    {
        StopCoroutine(coroutine);
        coroutine = StartCoroutine(moveCameraCoroutine(posId, duration));
    }

    private IEnumerator moveCameraCoroutine(int posID, float transitionTime)
    {
        if (posID > cameraPositions.Length) yield return null;

        Vector3 goalPos = cameraPositions[posID];
        Vector3 initPos = Camera.main.transform.position;
        float distance = (goalPos - initPos).magnitude;
        if (distance < 0.05f) yield return null;

        if(transitionTime <= 0f) transitionTime = 0.01f;
        float timer = 0f;

        while (timer < transitionTime)
        {
            timer += Time.deltaTime;

            Camera.main.transform.position = Vector3.Lerp(initPos, goalPos, timer/transitionTime);
            yield return null;
        }
        yield return null;
    }
}
