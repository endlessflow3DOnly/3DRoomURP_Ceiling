using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathCamera : MonoBehaviour
{
    [SerializeField] CameraManager cameraManager;
    [SerializeField] Transform[] pathPoints;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;

    float cameraValue = 0f;

    public bool isMoving = false;
    public bool isStartMoving = false;
    bool isPlayFadeOutAnim = false;

    private void Start()
    {
       
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.C)))
        {
            StartCoroutine(MoveCameraAndRotate());
        }
    }

    public void DoMoveCam(Action action)
    {
        Camera camera = GetComponent<Camera>();
        if (camera != null)
        {
            camera.enabled = true;
        }

        transform.position = cameraManager.FPCamera.transform.position;
        transform.rotation = cameraManager.FPCamera.transform.rotation;

        StartCoroutine(MoveCameraAndRotate(action));
    }

    #region Debug
    private IEnumerator MoveCameraAndRotate()
    {
        cameraValue = 0f;

        while (cameraValue < 1.0f)
        {
            isMoving = true;

            if (pathPoints != null && pathPoints.Length > 0)
            {
                iTween.PutOnPath(this.gameObject, pathPoints, cameraValue);
            }

            // 計算下一個路徑點的方向
            Vector3 currentPoint = iTween.PointOnPath(pathPoints, cameraValue);
            Vector3 nextPoint = iTween.PointOnPath(pathPoints, cameraValue + 0.01f); // 小的增量以計算下一個點
            Vector3 direction = nextPoint - currentPoint;
            Quaternion newRotation = Quaternion.LookRotation(direction);

            // 旋轉相機
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

            cameraValue += Time.deltaTime * moveSpeed;

            if (cameraValue >= 0.7 && !isPlayFadeOutAnim)
            {
                isPlayFadeOutAnim = true;
                StartCoroutine(FadeInOutManager.instance.DoFadeIn(1f, false));
            }

            yield return null;
        }

        transform.position = pathPoints[pathPoints.Length - 1].position;

        isMoving = false;
    }
    #endregion

    private IEnumerator MoveCameraAndRotate(Action action)
    {
        isPlayFadeOutAnim = false;
        cameraValue = 0f;

        while (cameraValue < 1.0f)
        {
            isMoving = true;

            if (pathPoints != null && pathPoints.Length > 0)
            {
                iTween.PutOnPath(this.gameObject, pathPoints, cameraValue);
            }

            // 計算下一個路徑點的方向
            Vector3 currentPoint = iTween.PointOnPath(pathPoints, cameraValue);
            Vector3 nextPoint = iTween.PointOnPath(pathPoints, cameraValue + 0.01f); // 小的增量以計算下一個點
            Vector3 direction = nextPoint - currentPoint;
            Quaternion newRotation = Quaternion.LookRotation(direction);

            // 旋轉相機
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);

            cameraValue += Time.deltaTime * moveSpeed;

            yield return null;
        }

        if (cameraValue >= 1 && !isPlayFadeOutAnim)
        {
            isPlayFadeOutAnim = true;
            StartCoroutine(FadeInOutManager.instance.DoFadeIn(1f, false));
        }

        transform.position = pathPoints[pathPoints.Length - 1].position;

        StartCoroutine(InvokeAction(2, action));
        isMoving = false;
    }

    IEnumerator InvokeAction(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }

    //繪製出路徑
    private void OnDrawGizmos()
    {
        if (pathPoints != null)
        {
            //iTween.DrawPath(pathPoints, Color.yellow);
        }
    }
}
