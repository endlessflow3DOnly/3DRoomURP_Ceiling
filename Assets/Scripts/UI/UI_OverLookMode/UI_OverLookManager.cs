using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OverLookManager : MonoBehaviour
{
    [Header("俯瞰模式 (OverLookMode)")]
    [SerializeField] GameObject UI_OverLookOperation;
    public Toggle Toggle_HmoveMode;
    public Toggle Toggle_RotationMode;
    public Button Btn_ResetPosRot;

    [Header("參考管理員")]
    [SerializeField] CameraManager CameraManager;

    public void Init()
    {
        InitButtonListener();

        //關閉俯瞰模式UI
        EnableOverLookUI(false);
    }

    /// <summary>
    /// 初始化按鈕事件
    /// </summary>
    void InitButtonListener()
    {
        Btn_ResetPosRot.onClick.AddListener(CameraManager.OverlookCameraScript.ResetCamPosition);
    }

    #region 俯瞰模式介面
    /// <summary>
    /// 是否打開俯瞰模式按鈕介面
    /// </summary>
    /// <param name="enable"></param>
    public void EnableOverLookUI(bool enable)
    {
        UI_OverLookOperation.SetActive(enable);
    }
    #endregion


}
