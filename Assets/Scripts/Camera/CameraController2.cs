using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 移動速度
    public float zoomSpeed = 1.0f; // 拉近/拉遠速度
    public float minZoomDistance = 1.0f; // 最小拉近距離
    public float maxZoomDistance = 10.0f; // 最大拉遠距離
    public Vector2 moveBoundaryMin = new Vector2(-10f, -10f); // 移動範圍的最小邊界
    public Vector2 moveBoundaryMax = new Vector2(10f, 10f); // 移動範圍的最大邊界
    public KeyCode resetKey = KeyCode.R; // 重置相機位置的按鍵

    public Camera thisCamera;

    private Vector3 lastMousePosition;
    private Vector3 offset;
    private float offsetY;

    void Start()
    {
        // 計算相機的初始偏移
        offset = transform.position;
        offsetY = offset.y;
    }

    void Update()
    {
        // 檢查相機是否存在並已啟用
        if (thisCamera != null && thisCamera.enabled)
        {
            // 檢查滑鼠滾輪，並調整相機距離
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            float zoomAmount = scrollWheel * zoomSpeed;
            float newDistance = Mathf.Clamp(offset.y - zoomAmount, minZoomDistance, maxZoomDistance);
            offset.y = newDistance;

            transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);


            // 檢查滑鼠左鍵是否按住，並平滑移動相機位置
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
                float mouseX = deltaMouse.x * moveSpeed;
                float mouseY = deltaMouse.y * moveSpeed;

                // 計算平滑移動向量，並根據滑鼠移動方向進行對應的移動
                Vector3 moveVector = new Vector3(-mouseX, 0, -mouseY);

                // 更新相機位置
                transform.position += moveVector * Time.deltaTime;

                // 限制相機不能越界
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, moveBoundaryMin.x, moveBoundaryMax.x),
                    transform.position.y,
                    Mathf.Clamp(transform.position.z, moveBoundaryMin.y, moveBoundaryMax.y)
                );

                // 記住新的滑鼠位置
                lastMousePosition = Input.mousePosition;
            }

            // 重置相機位置的按鍵
            if (Input.GetKeyDown(resetKey))
            {
                // 回到初始位置
                ResetCamPosition();
            }
        }
    }

    public void ResetCamPosition()
    {
        // 回到初始位置
        transform.position = offset;
        offset.y = offsetY;
    }
}
