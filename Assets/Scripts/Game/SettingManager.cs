using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("管理員")]
    [SerializeField] CameraManager CameraManager;
    [SerializeField] UIManager UIManager;

    [SerializeField] bool isDebugMode = false;

    private bool IsFullscreen = false;
    private Resolution[] Resolutions;
    private int currentResolutionIndex = 0;

    [SerializeField] TMP_Dropdown ResulotionDropDown;
    [SerializeField] GameObject SettingUI;
    [SerializeField] List<Texture2D> CursorTexs;

    public bool isHoverHighlightObj = false;

    // Start is called before the first frame update
    public void Init()
    {
        //InitResulotion();
        //Debug.Log("<color=blue>" + this.name + " 初始化完成</color>");
    }

    // Update is called once per frame
    void Update()
    {
        SetFullScreen();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
#endif
        SetCursorTexture();

        if (isHoverHighlightObj == true)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }


    #region 解析度設定
    void InitResulotion()
    {
        Resolutions = Screen.resolutions;

        ResulotionDropDown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < Resolutions.Length; i++)
        {
            string resoltionOption = Resolutions[i].width + "x" + Resolutions[i].height;
            options.Add(resoltionOption);
            if (Resolutions[i].width == Screen.currentResolution.width && Resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResulotionDropDown.AddOptions(options);
        ResulotionDropDown.value = currentResolutionIndex;
        ResulotionDropDown.RefreshShownValue();

        Screen.fullScreen = true;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    /// <summary>
    /// 全螢幕
    /// </summary>
    void SetFullScreen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsFullscreen)
            {
                Screen.fullScreen = false;
                IsFullscreen = false;
            }
            else
            {
                Screen.fullScreen = true;
                IsFullscreen = true;
            }
        }
    }
    #endregion

    public void SetCursorTexture()
    {
        //進入俯瞰相機模式 就變
        //摸到UI不要 變成原本的 變成其他相機也不要

        if (CameraManager.OverlookCamera.enabled == true)
        {
            if (UIDetector.instance.IsPointerOverUIElement())
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                if (UIManager.UIOverLookManager.Toggle_RotationMode.isOn == true)
                {
                    Cursor.SetCursor(CursorTexs[0], Vector3.zero, CursorMode.ForceSoftware);
                }
                else if (UIManager.UIOverLookManager.Toggle_HmoveMode.isOn == true)
                {
                    Cursor.SetCursor(CursorTexs[1], Vector3.zero, CursorMode.ForceSoftware);
                }
            }
        }
        else if (CameraManager.OverlookCamera.enabled == false || UIDetector.instance.IsPointerOverUIElement())
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
