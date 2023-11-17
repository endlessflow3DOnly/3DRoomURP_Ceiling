using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamUI : MonoBehaviour
{
    [SerializeField] bool isFirstPersonView;

    [SerializeField] Camera camera;
    [SerializeField] float minScale = 0.1f; // 最小缩放
    [SerializeField] float maxScale = 0.5f; // 最大缩放
    float minDistance = 0.5f; // 最小距离
    float maxDistance = 1.0f; // 最大距离

    void Update()
    {
        if (isFirstPersonView)
        {
            // 获取UI对象与相机的距离
            float distance = Vector3.Distance(transform.position, camera.transform.position);

            // 根据距离来计算缩放比例，使用反比例关系
            float scale = Mathf.Lerp(maxScale, minScale, Mathf.InverseLerp(minDistance, maxDistance, distance));

            // 计算新的距离，限制在minDistance和maxDistance之间
            float clampedDistance = Mathf.Clamp(distance, minDistance, maxDistance);

            // 根据新的距离来计算新的缩放比例
            float newScale = Mathf.Lerp(minScale, maxScale, Mathf.InverseLerp(minDistance, maxDistance, clampedDistance));

            // 根据相机的朝向旋转UI对象
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);

            // 应用新的缩放
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
        else
        {
            // 根据相机的朝向旋转UI对象
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.back, camera.transform.rotation * Vector3.up);
        }
    }
}
