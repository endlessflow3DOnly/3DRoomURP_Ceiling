using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OverLookCamera;
using static UnityEngine.GraphicsBuffer;

public class OverLookCamera : MonoBehaviour
{
    #region 參考
    [SerializeField] GameManager GameManager;
    [SerializeField] UIManager UIManager;
    #endregion

    #region 通用
    [SerializeField] Camera thisCamera;
    [SerializeField] Transform focustarget; // 物體的 transform
    [SerializeField] float zoomSpeed = 2.5f; // 拉近/拉遠速度
    [SerializeField] float minZoom = 5.0f;
    [SerializeField] float maxZoom = 50.0f;
    private Vector3 lastMousePosition;
    private Vector3 StartPos;
    private Quaternion StartRotation;

    public enum OverlookCameraType
    {
        Default,
        HorizontalMoveMode,
        RotationMode,
        ScrollZoomMode
    }

    [Header("目前俯瞰相機模式")]
    public OverlookCameraType overlookCameraType;
    #endregion

    #region 平滑相機
    [Header("平滑相機")]
    [SerializeField] float moveSpeed = 0.1f; // 移動速度
    [SerializeField] Vector2 moveBoundaryMin = new Vector2(-10f, -10f); // 移動範圍的最小邊界
    [SerializeField] Vector2 moveBoundaryMax = new Vector2(10f, 10f); // 移動範圍的最大邊界
    [SerializeField] LayerMask floorLayer;
    private float offsetY;
    #endregion

    #region 旋轉相機
    [Header("旋轉相機速度")]
    [SerializeField] float rotationSpeed = 0.1f; // 旋轉速度
    
    [Header("限制最大旋轉速度")]
    [SerializeField] float MaxRotationSpeedX = 1.0f;
    [SerializeField] float MaxRotationSpeedY = 1.0f;

    [Header("限制最大旋轉角度")]
    [SerializeField] float MinVerticalRotation = 1f;
    [SerializeField] float MaxVerticalRotation = 85f;
    float mouseX;
    float mouseY;
    #endregion

    #region Debug用
    public bool isDebugMode = false;
    #endregion

    public void Init()
    {
        InitStartCamPos();
    }

    /// <summary>
    /// 初始化獲取相機位置 + 重置按鈕
    /// </summary>
    public void InitStartCamPos()
    {
        StartPos = transform.position;
        StartRotation = transform.rotation;
        offsetY = StartPos.y;
    }

    void Update()
    {
        if (GameManager.IsAllFinishInit == true)
        {
            ScrollMouseView();

            if (UIManager.UIOverLookManager.Toggle_HmoveMode.isOn)
            {
                overlookCameraType = OverlookCameraType.HorizontalMoveMode;
                MoveCamMode();
            }
            else if (UIManager.UIOverLookManager.Toggle_RotationMode.isOn)
            {
                overlookCameraType = OverlookCameraType.RotationMode;
                RotateCamMode();
            }
            else
            {
                overlookCameraType = OverlookCameraType.Default;
            }
        }

        if (isDebugMode)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            }
        }
    }

    void ScrollMouseView()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        thisCamera.fieldOfView = Mathf.Clamp(thisCamera.fieldOfView - zoom, minZoom, maxZoom);
    }

    void RotateCamMode()
    {
        if (thisCamera != null && thisCamera.enabled)
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            mouseX = Mathf.Clamp(deltaMouse.x * rotationSpeed, -MaxRotationSpeedX, MaxRotationSpeedX);
            mouseY = Mathf.Clamp(deltaMouse.y * rotationSpeed, -MaxRotationSpeedY, MaxRotationSpeedY);

            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;

            }
            else if (Input.GetMouseButton(0))
            {
                if (transform.eulerAngles.x < MinVerticalRotation)
                {
                    if (mouseY < 0)
                    {
                        transform.RotateAround(focustarget.position, Vector3.up, mouseX);
                        transform.RotateAround(focustarget.position, transform.right, -mouseY);
                    }
                    return;
                }
                else
                {
                    // 旋轉相機
                    transform.RotateAround(focustarget.position, Vector3.up, mouseX);
                    transform.RotateAround(focustarget.position, transform.right, -mouseY);
                }
            }

            lastMousePosition = Input.mousePosition;
        }
    }

    void MoveCamMode()
    {
        if (thisCamera != null && thisCamera.enabled)
        {
            Ray ray = thisCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, floorLayer))
            {
                Vector3 hitPoint = hitInfo.point;
                focustarget.position = new Vector3(hitPoint.x, 0, hitPoint.z);
            }

            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 deltaMouse = Input.mousePosition - lastMousePosition;

                Vector3 moveVector = transform.TransformDirection(new Vector3(-deltaMouse.x, 0, -deltaMouse.y)) * moveSpeed;
     
                Vector3 targetPosition = transform.position + new Vector3(moveVector.x, 0, moveVector.z);

                targetPosition.x = Mathf.Clamp(targetPosition.x, moveBoundaryMin.x, moveBoundaryMax.x);
                targetPosition.z = Mathf.Clamp(targetPosition.z, moveBoundaryMin.y, moveBoundaryMax.y);

                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

                lastMousePosition = Input.mousePosition;
            }
        }
    }


    /// <summary>
    /// Reset到預設位置
    /// </summary>
    public void ResetCamPosition()
    {
        //重置位置
        transform.DOMove(StartPos, 1);
        transform.DORotateQuaternion(StartRotation, 1);
        thisCamera.fieldOfView = maxZoom;

        StartPos.y = offsetY;
    }
}
