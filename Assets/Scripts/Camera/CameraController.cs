using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // 物體的 transform
    public float rotationSpeed = 1.0f; // 旋轉速度
    public float zoomSpeed = 1.0f; // 拉近/拉遠速度
    public float minZoomDistance = 1.0f; // 最小拉近距離
    public float maxZoomDistance = 10.0f; // 最大拉遠距離

    public float minPositionY = 0.5f; // 相機最低點限制
    public float maxPositionY = 9.95f; // 相機最高點限制

    public Camera thisCamera;

    private Vector3 offset;
    private Vector3 lastMousePosition;

    [Header("限制最大旋轉速度")]
    public float MaxRotationSpeedX = 1.0f;
    public float MaxRotationSpeedY = 1.0f;

    void Start()
    {
        // 計算相機的初始偏移
        offset = transform.position - target.position;
    }

    void Update()
    {
        // 檢查相機是否存在並已啟用
        if (thisCamera != null && thisCamera.enabled)
        {
            // 執行您的程式碼
            // 檢查滑鼠滾輪，並調整相機距離
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            float zoomAmount = scrollWheel * zoomSpeed;
            float newDistance = Mathf.Clamp(offset.magnitude - zoomAmount, minZoomDistance, maxZoomDistance);
            offset = offset.normalized * newDistance;

            // 檢查滑鼠左鍵是否按住，並旋轉相機視角
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
                float mouseX = deltaMouse.x * rotationSpeed;
                float mouseY = deltaMouse.y * rotationSpeed;

                //限制最大旋轉速度
                if(mouseX > MaxRotationSpeedX)
                {
                    mouseX = MaxRotationSpeedX;
                }
                else if (mouseX < -MaxRotationSpeedX)
                {
                    mouseX = -MaxRotationSpeedX;
                }
                
                if (mouseY > MaxRotationSpeedY)
                {
                    mouseY = MaxRotationSpeedY;
                }
                else if(mouseY < -MaxRotationSpeedY)
                {
                    mouseY = -MaxRotationSpeedY;
                }

                // 旋轉相機
                transform.RotateAround(target.position, Vector3.up, mouseX);

                //確保 Y 軸位置不小於零
                if(transform.position.y >= minPositionY)
                {
                    if(transform.position.y <= maxPositionY)
                    {
                        transform.RotateAround(target.position, transform.right, -mouseY); // 使用負號反轉上下滑動
                    }
                    else
                    {
                        if (-mouseY < 0)
                        {
                            transform.RotateAround(target.position, transform.right, -mouseY);
                        }
                        else
                        {
                            transform.RotateAround(target.position, transform.right, 0);
                        }
                    }

                }
                else
                {
                    if(-mouseY > 0)
                    {
                        transform.RotateAround(target.position, transform.right, -mouseY);
                    }
                    else
                    {
                        transform.RotateAround(target.position, transform.right, 0);
                    }
                    
                }
                
                // 調整相機位置
                offset = transform.position - target.position;
            }

            // 更新相機位置
            transform.position = target.position + offset;

            lastMousePosition = Input.mousePosition;
        }
    }
}
