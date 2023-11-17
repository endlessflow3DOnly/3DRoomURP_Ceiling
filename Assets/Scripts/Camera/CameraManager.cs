using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//相機切換
public class CameraManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    public static CameraManager instance;

    [Header("主畫面UI用相機")]
    public Camera MainUICamera;

    [Header("小地圖用相機")]
    public Camera MiniMapCamera;

    [Header("第一人稱相機")]
    public Camera FPCamera;

    [Header("俯瞰模式相機")]
    public Camera OverlookCamera;
    [ReadOnlyInspector] public OverLookCamera OverlookCameraScript;

    [Header("漫遊模式相機")]
    public Camera PathCamera;
    [SerializeField] int currentRoamingCameraIndex = 0;

    private RoamingCamera RoamingCamera;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        if (FPCamera == null)
        {
            FPCamera = GameManager.PlayerController.FPCamera;
        }

        if (OverlookCameraScript == null)
        {
            OverlookCameraScript = OverlookCamera.GetComponent<OverLookCamera>();
            OverlookCameraScript.Init();
        }
        Debug.Log("<color=blue>" + this.name + " 初始化完成</color>");
    }

    #region 路徑相機
    public void ToTV_CameraDoPath(Action action)
    {
        PathCamera.GetComponent<PathCamera>().DoMoveCam(action);
    }
    #endregion

    #region 主選單相機功能
    /// <summary>
    /// 切換到主畫面相機
    /// </summary>
    public void SwitchToMainUICamera()
    {
        ResetAndCloseAllCameras();

        MainUICamera.enabled = true;
    }

    /// <summary>
    /// 切換到第一人稱相機 (自由模式)
    /// </summary>
    public void SwitchToFirstPersonCamera()
    {
        ResetAndCloseAllCameras();

        FPCamera.enabled = true;
    }

    /// <summary>
    /// 切換到漫遊模式相機
    /// </summary>
    public void SwitchToRoamingCameras()
    {
        ResetAndCloseAllCameras();

        InitRoamingCamera();
    }

    /// <summary>
    /// 切換到俯瞰視角相機
    /// </summary>
    public void SwitchToOverLookCamera()
    {
        ResetAndCloseAllCameras();

        OverlookCamera.enabled = true;
    }
    #endregion

    #region 第一人稱的相機開關
    public void EnableFPCamera(bool isShow)
    {
        FPCamera.enabled = isShow;
    }
    #endregion

    #region 漫遊模式
    /// <summary>
    /// 初始化漫遊相機行為
    /// </summary>
    /// <param name="index"></param>
    private void InitRoamingCamera()
    {
        //啟動漫遊相機
        RoamingCamera = PathCamera.GetComponent<RoamingCamera>();
        PathCamera.enabled = false;

        if (RoamingCamera != null) 
        {
            PathCamera.enabled = true;
            RoamingCamera.DoingPath();
        }
    }

    /// <summary>
    /// 在 Update 中隨時檢查
    /// </summary>
    public void CheckRoamingPath()
    {
        if (RoamingCamera.RoamingOver && RoamingCamera != null)
        {
            RoamingCamera.DoingPath();
        }
    }
    #endregion

    #region 路徑相機
    public void StopPathCamera()
    {
        PathCamera.transform.position = FPCamera.transform.position;
        PathCamera.transform.rotation = FPCamera.transform.rotation;
        PathCamera.enabled = false;
        PathCamera.GetComponent<RoamingCamera>().StopMoveCamera();
    }
    #endregion

    /// <summary>
    /// 重置並關閉所有相機
    /// </summary>
    public void ResetAndCloseAllCameras()
    {
        MainUICamera.enabled = false;
        FPCamera.enabled = false;
        OverlookCamera.enabled = false;
        PathCamera.enabled = false;

        StopPathCamera();
    }
}

