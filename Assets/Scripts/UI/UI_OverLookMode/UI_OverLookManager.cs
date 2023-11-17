using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OverLookManager : MonoBehaviour
{
    [Header("�����Ҧ� (OverLookMode)")]
    [SerializeField] GameObject UI_OverLookOperation;
    public Toggle Toggle_HmoveMode;
    public Toggle Toggle_RotationMode;
    public Button Btn_ResetPosRot;

    [Header("�ѦҺ޲z��")]
    [SerializeField] CameraManager CameraManager;

    public void Init()
    {
        InitButtonListener();

        //���������Ҧ�UI
        EnableOverLookUI(false);
    }

    /// <summary>
    /// ��l�ƫ��s�ƥ�
    /// </summary>
    void InitButtonListener()
    {
        Btn_ResetPosRot.onClick.AddListener(CameraManager.OverlookCameraScript.ResetCamPosition);
    }

    #region �����Ҧ�����
    /// <summary>
    /// �O�_���}�����Ҧ����s����
    /// </summary>
    /// <param name="enable"></param>
    public void EnableOverLookUI(bool enable)
    {
        UI_OverLookOperation.SetActive(enable);
    }
    #endregion


}
