using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�۾�����
public class CameraManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    public static CameraManager instance;

    [Header("�D�e��UI�ά۾�")]
    public Camera MainUICamera;

    [Header("�p�a�ϥά۾�")]
    public Camera MiniMapCamera;

    [Header("�Ĥ@�H�٬۾�")]
    public Camera FPCamera;

    [Header("�����Ҧ��۾�")]
    public Camera OverlookCamera;
    [ReadOnlyInspector] public OverLookCamera OverlookCameraScript;

    [Header("���C�Ҧ��۾�")]
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
        Debug.Log("<color=blue>" + this.name + " ��l�Ƨ���</color>");
    }

    #region ���|�۾�
    public void ToTV_CameraDoPath(Action action)
    {
        PathCamera.GetComponent<PathCamera>().DoMoveCam(action);
    }
    #endregion

    #region �D���۾��\��
    /// <summary>
    /// ������D�e���۾�
    /// </summary>
    public void SwitchToMainUICamera()
    {
        ResetAndCloseAllCameras();

        MainUICamera.enabled = true;
    }

    /// <summary>
    /// ������Ĥ@�H�٬۾� (�ۥѼҦ�)
    /// </summary>
    public void SwitchToFirstPersonCamera()
    {
        ResetAndCloseAllCameras();

        FPCamera.enabled = true;
    }

    /// <summary>
    /// �����캩�C�Ҧ��۾�
    /// </summary>
    public void SwitchToRoamingCameras()
    {
        ResetAndCloseAllCameras();

        InitRoamingCamera();
    }

    /// <summary>
    /// ��������������۾�
    /// </summary>
    public void SwitchToOverLookCamera()
    {
        ResetAndCloseAllCameras();

        OverlookCamera.enabled = true;
    }
    #endregion

    #region �Ĥ@�H�٪��۾��}��
    public void EnableFPCamera(bool isShow)
    {
        FPCamera.enabled = isShow;
    }
    #endregion

    #region ���C�Ҧ�
    /// <summary>
    /// ��l�ƺ��C�۾��欰
    /// </summary>
    /// <param name="index"></param>
    private void InitRoamingCamera()
    {
        //�Ұʺ��C�۾�
        RoamingCamera = PathCamera.GetComponent<RoamingCamera>();
        PathCamera.enabled = false;

        if (RoamingCamera != null) 
        {
            PathCamera.enabled = true;
            RoamingCamera.DoingPath();
        }
    }

    /// <summary>
    /// �b Update ���H���ˬd
    /// </summary>
    public void CheckRoamingPath()
    {
        if (RoamingCamera.RoamingOver && RoamingCamera != null)
        {
            RoamingCamera.DoingPath();
        }
    }
    #endregion

    #region ���|�۾�
    public void StopPathCamera()
    {
        PathCamera.transform.position = FPCamera.transform.position;
        PathCamera.transform.rotation = FPCamera.transform.rotation;
        PathCamera.enabled = false;
        PathCamera.GetComponent<RoamingCamera>().StopMoveCamera();
    }
    #endregion

    /// <summary>
    /// ���m�������Ҧ��۾�
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

