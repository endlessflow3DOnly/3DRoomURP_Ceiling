using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//漫遊相機用itween放置到路徑點上，並且移動
public class RoamingCamera : MonoBehaviour
{
    [Header("漫遊路徑組")]
    [SerializeField] Transform[] pathParentPoints;
    [SerializeField] Transform[] pathPoints;
    private float moveSpeed = 0.2f;

    [Header("相機移動指數")]
    public float cameraValue;

    [Header("相機是否當前path循環結束")]
    public bool RoamingOver = false;

    bool isPlayFadeOutAnim = false;
    private int currentIndex = 0;
    private IEnumerator moveCameraCoroutine;

    /// <summary>
    /// 開始路徑移動
    /// </summary>
    public void DoingPath()
    {
        Transform selectedPathParent = pathParentPoints[currentIndex];
        int childCount = selectedPathParent.childCount;

        pathPoints = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            pathPoints[i] = selectedPathParent.GetChild(i);
        }

        GetPathRotateAngle(selectedPathParent);

        // Start the MoveCamera coroutine
        moveCameraCoroutine = MoveCamera();
        StartCoroutine(moveCameraCoroutine);

        StartCoroutine(FadeInOutManager.instance.DoFadeOut(1f, true));

        currentIndex = (currentIndex + 1) % pathParentPoints.Length;
    }

    public void GetPathRotateAngle(Transform pathParent)
    {
        //先重置
        gameObject.transform.rotation = Quaternion.identity;

        PathViewRotate pathViewRotate = pathParent.GetComponent<PathViewRotate>();
        if (pathViewRotate != null)
        {
            // 使用 pathViewRotate 的 rotation 属性来设置当前对象的旋转
            gameObject.transform.rotation = pathViewRotate.rotation;
        }
    }

    private IEnumerator MoveCamera()
    {
        RoamingOver = false;
        cameraValue = 0;
        isPlayFadeOutAnim = false;

        while (cameraValue < 1f)
        {
            if (gameObject != null && pathPoints != null && pathPoints.Length > 0)
            {
                iTween.PutOnPath(gameObject, pathPoints, cameraValue);
            }

            if (cameraValue >= 0.7 && !isPlayFadeOutAnim)
            {
                isPlayFadeOutAnim = true;
                StartCoroutine(FadeInOutManager.instance.DoFadeIn(1f, true));
            }

            cameraValue += Time.deltaTime * moveSpeed;
            yield return null;
        }

        RoamingOver = true;
    }

    /// <summary>
    /// 停止路徑相機的行為
    /// </summary>
    public void StopMoveCamera()
    {
        if (moveCameraCoroutine != null)
        {
            StopCoroutine(moveCameraCoroutine);
        }
    }

    //繪製出路徑
    private void OnDrawGizmos()
    {
        if (pathPoints != null && RoamingOver == false)
        {
            iTween.DrawPath(pathPoints, Color.yellow);
        }
    }
}
