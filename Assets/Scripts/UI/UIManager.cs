using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("參考的其他主管理員")]
    [SerializeField] GameManager GameManager;
    [SerializeField] TVManager TVManager;
    [SerializeField] CameraManager CameraManager;
    [SerializeField] VideoPlayerManager VideoPlayerManager;
    [SerializeField] SceneObjectsManager SceneObjectsManager;
    [SerializeField] SettingManager SettingManager;

    [Header("參考的UI管理員")]
    public UI_OverLookManager UIOverLookManager;
    public UI_BuildMaterialManager UI_BuildMaterialManager;
    public UI_ConstructMethodManager UI_ConstructMethodManager;
    public UI_MiniMapManager UI_MiniMapManager;

    [Header("主UI物件")]
    public GameObject UI_Main;

    [Header("主畫面上方按鈕")]
    [SerializeField] List<Button> TopMenuBtns;
    public Toggle Toggle_ShowMiniMap;
    public Toggle Toggle_ShowTutorial;

    [Header("空間按鈕顯示切換")]
    public GameObject UI_RoomSwitch;
    public Image Img_RoomView;
    [SerializeField] Button Btn_SwitchNextRoom;
    [SerializeField] Button Btn_SwitchPreviousRoom;
    [SerializeField] TMP_Text Text_SwitchRoomName;
    public int currentRoomIndex = 0;

    [Header("主選單按鈕和切換")]
    [SerializeField] GameObject[] Btns_MainMenu;
    [Range(0.0f, 1f)]
    [SerializeField] float delayBetweenAnimations = 0.05f; // 每个动画之间的延迟时间
    [Range(0.0f, 1f)]
    [SerializeField] float fadeDuration = 1.0f; // 淡入淡出的持续时间

    [SerializeField] Button Btn_MainMenu;
    private Image Img_Btn_MainMenu;
    [SerializeField] Button Btn_CloseMainMenu;
    private Image Img_Btn_CloseMainMenu;
    [SerializeField] Button Btn_FirstPersonMode;
    [SerializeField] Button Btn_RoamingMode;
    [SerializeField] Button Btn_OverLookMode;
    [SerializeField] Button Btn_BuildingMaterialsDisplay;
    [SerializeField] Button Btn_ConstructionMethodDisplay;
    [SerializeField] Button Btn_BuildingMaterialsSelection;
    [SerializeField] List<Button> MenuButtons;

    [Header("教學面板")]
    [SerializeField] List<GameObject> UI_TutorialPanel;
    [SerializeField] bool isPanelOpen = false;
    int panelIndex = 999;

    [Header("離開遊戲")]
    [SerializeField] Button Btn_Exit;

    bool isInitUI = false;

    public void Init()
    {
        InitComponent();
        InitButtonListener();
        InitToggleState();
        InitUIDefault();

        Debug.Log("<color=blue>" + this.name + " 初始化完成</color>");
    }

    #region 初始化設定
    /// <summary>
    /// 初始化Component
    /// </summary>
    void InitComponent()
    {
        Img_Btn_CloseMainMenu = Btn_CloseMainMenu.GetComponent<Image>();
        Img_Btn_MainMenu = Btn_MainMenu.GetComponent<Image>();
    }

    void InitToggleState()
    {
        Toggle_ShowMiniMap.isOn = false;
        Toggle_ShowTutorial.isOn = false;
    }

    /// <summary>
    /// 初始化Button事件
    /// </summary>
    void InitButtonListener()
    {
        Toggle_ShowMiniMap.onValueChanged.AddListener(UI_MiniMapManager.ShowMiniMap_ToggleUsed);
        Toggle_ShowTutorial.onValueChanged.AddListener(ShowTutorialPanel);

        //主選單按鈕切換顏色
        foreach (Button button in MenuButtons)
        {
            button.onClick.AddListener(() => MainMenuSelectedButtonChangeColorToYellow(button));
        }

        Btn_MainMenu.onClick.AddListener(PlayOpenMainMenuAnimation);
        Btn_CloseMainMenu.onClick.AddListener(PlayCloseMainMenuAnimation);
        Btn_Exit.onClick.AddListener(ExitGame);

        Btn_SwitchNextRoom.onClick.AddListener(ShowNextRoom);
        Btn_SwitchPreviousRoom.onClick.AddListener(ShowPreviousRoom);

        //主選單按鈕功能
        Btn_FirstPersonMode.onClick.AddListener(GameManager.SwitchToFirstPersonMode);
        Btn_RoamingMode.onClick.AddListener(GameManager.SwitchToRoamingMode);
        Btn_OverLookMode.onClick.AddListener(GameManager.SwitchToOverLookMode);
        Btn_BuildingMaterialsDisplay.onClick.AddListener(GameManager.SwitchToBuildingMatDisplayMode);
        Btn_ConstructionMethodDisplay.onClick.AddListener(GameManager.SwitchToConstructionMethodDisplayMode);
        //Btn_BuildingMaterialsSelection.onClick.AddListener(GameManager.SwitchToBuildingMaterialsSelectionMode);
    }

    /// <summary>
    /// 初始化介面
    /// </summary>
    void InitUIDefault()
    {
        PlayCloseMainMenuAnimation();
        
        currentRoomIndex = 0;

        //關閉教學
        CloseAllTutorialPanel();

        isInitUI = true;
    }
    #endregion

    /// <summary>
    /// 開關主UI畫面
    /// </summary>
    /// <param name="isShow"></param>
    public void EnableUIMain(bool isShow)
    {
        UI_Main.SetActive(isShow);
    }

    #region 教學面板
    /// <summary>
    /// 顯示教學面板
    /// </summary>
    /// <param name="mode"></param>
    public void ShowTutorialPanel(bool isOn)
    {
        if (GameManager.currentGameMode == GameManager.GameMode.Roaming || GameManager.currentGameMode == GameManager.GameMode.MainMenu)
        {
            return;
        }

        //關閉所有面板
        CloseAllTutorialPanel();

        if (GameManager.currentGameMode == GameManager.GameMode.FirstPerson)
        {
            UI_TutorialPanel[0].SetActive(isOn);
        }
        else if (CameraManager.OverlookCameraScript.overlookCameraType == OverLookCamera.OverlookCameraType.HorizontalMoveMode)
        {
            UI_TutorialPanel[1].SetActive(isOn);
        }
        else if (CameraManager.OverlookCameraScript.overlookCameraType == OverLookCamera.OverlookCameraType.RotationMode)
        {
            UI_TutorialPanel[2].SetActive(isOn);
        }

    }

    /// <summary>
    /// 關閉所有教學面板 btn用
    /// </summary>
    public void CloseAllTutorialPanel()
    {
        foreach (GameObject obj in UI_TutorialPanel)
        {
            obj.SetActive(false);
        }
    }

    /// <summary>
    /// 關閉所有教學面板 toggle用
    /// </summary>
    /// <param name="isOn"></param>
    public void CloseAllTutotialByToggle(bool isOn)
    {
        if (isOn)
        {
            foreach (GameObject obj in UI_TutorialPanel)
            {
                obj.SetActive(false);
            }
        }
    }
    #endregion

    #region 切換空間按鈕
    public void ShowNextRoom()
    {
        currentRoomIndex = (currentRoomIndex + 1) % GameManager.RoomInfo_Assets.Count;
        ShowRoom();
    }

    public void ShowPreviousRoom()
    {
        currentRoomIndex = (currentRoomIndex - 1 + GameManager.RoomInfo_Assets.Count) % GameManager.RoomInfo_Assets.Count;
        ShowRoom();
    }

    private void ShowRoom()
    {
        Img_RoomView.sprite = GameManager.RoomInfo_Assets[currentRoomIndex].Spr_MainTex;
        Text_SwitchRoomName.text = GameManager.RoomInfo_Assets[currentRoomIndex].RoomName;

        GameManager.TeleportManager.SwitchToRoom(GameManager.RoomInfo_Assets[currentRoomIndex].teleportRoomPlace);
    }
    #endregion


    #region 主選單
    /// <summary>
    /// 開啟主選單
    /// </summary>
    void PlayOpenMainMenuAnimation()
    {
        StartCoroutine(OpenMainButtons());
    }

    /// <summary>
    /// 關閉主選單
    /// </summary>
    void PlayCloseMainMenuAnimation()
    {
        StartCoroutine(CloseMainButtons());
    }

    #region DGTween主選單底下按鈕打開
    IEnumerator OpenMainButtons()
    {
        StartCoroutine(FadeImage(Img_Btn_CloseMainMenu, Img_Btn_CloseMainMenu.color, true, 2f, 0.1f, () =>
        {
            Btn_CloseMainMenu.gameObject.SetActive(true);
        }));

        if (isInitUI)
        {
            StartCoroutine(FadeImage(Img_Btn_MainMenu, Img_Btn_MainMenu.color, false, 0.5f, 0.5f,() =>
            {
                Btn_MainMenu.gameObject.SetActive(false);
            }));
        }

        foreach (GameObject button in Btns_MainMenu)
        {
            DoAnimOpen(button);
            yield return new WaitForSeconds(delayBetweenAnimations);
            DoFadeIn(button);
        }

        foreach (GameObject button in Btns_MainMenu)
        {
            button.GetComponent<Button>().enabled = true;
        }
    }

    void DoAnimOpen(GameObject button)
    {
        //初始按鈕位置
        Vector2 initialPos = button.GetComponent<InitialPositionMainButtons>().initialPosition;

        //移動
        button.transform.DOMove(initialPos, fadeDuration).SetEase(Ease.InOutQuad);
    }

    void DoFadeIn(GameObject button)
    {
        Image buttonImage = button.GetComponent<Image>();

        float targetAlpha = 0.1f;

        DOTween.To(() => buttonImage.color.a,
            alpha => {
                Color newColor = buttonImage.color;
                newColor.a = alpha;
                buttonImage.color = newColor;

                if (alpha >= targetAlpha)
                {
                    button.SetActive(true); //透明度到target值
                }
            },
            1, //目標透明度
            fadeDuration)
            .SetEase(Ease.OutQuad);
    }
    #endregion

    #region DGTween主選單底下按鈕關閉
    IEnumerator CloseMainButtons()
    {
        foreach (GameObject button in Btns_MainMenu)
        {
            button.GetComponent<Button>().enabled = false;
        }

        StartCoroutine(FadeImage(Img_Btn_CloseMainMenu, Img_Btn_CloseMainMenu.color, false, 1f, 0.9f,() =>
        {
            Btn_CloseMainMenu.gameObject.SetActive(false);
        }));

        // 初始化完成后才可以正常關閉
        if (isInitUI)
        {
            StartCoroutine(FadeImage(Img_Btn_MainMenu, Img_Btn_MainMenu.color, true, 1f, 0.1f,() =>
            {
                Btn_MainMenu.gameObject.SetActive(true);
            }));
        }

        foreach (GameObject button in Btns_MainMenu)
        {
            DoAnimClose(button);
            yield return new WaitForSeconds(delayBetweenAnimations);
            DoFadeOut(button);
        }
    }

    void DoAnimClose(GameObject button)
    {
        Vector2 targetPos = new Vector2(23.7f, button.transform.position.y);

        // 使用 DOTween 移动按钮到目标位置
        button.transform.DOMove(targetPos, fadeDuration).SetEase(Ease.InOutQuad);
    }

    void DoFadeOut(GameObject button)
    {
        // 获取按钮的 Image 组件
        Image buttonImage = button.GetComponent<Image>();

        float initialAlpha = buttonImage.color.a; // 初始透明度
        float targetAlpha = 0.1f; // 目标透明度（约90%透明度）

        // 使用 DOVirtual.Float 监视透明度变化
        DOTween.To(() => buttonImage.color.a, // 从当前透明度开始
            alpha =>
            {
                Color newColor = buttonImage.color;
                newColor.a = alpha;
                buttonImage.color = newColor;

                if (alpha <= targetAlpha)
                {
                    button.SetActive(false); // 当透明度达到目标时，设置为 false

                }
            },
            0, // 到达目标透明度
            fadeDuration)
            .SetEase(Ease.OutQuad);
    }

    #region 上方按鈕開關
    /// <summary>
    /// 顯示主選單的上方按鈕與否
    /// </summary>
    /// <param name="isShow"></param>
    public void EnableTopMenu(bool isShow)
    {
        Toggle_ShowMiniMap.gameObject.SetActive(isShow);

        foreach (Button btn in TopMenuBtns) 
        {
            btn.gameObject.SetActive(isShow);
        }
    }

    #endregion
    #endregion

    /// <summary>
    /// 讓主選單選中的按鈕呈現黃色，而且點擊其他地方不會跑掉
    /// </summary>
    /// <param name="button"></param>
    private void MainMenuSelectedButtonChangeColorToYellow(Button button)
    {
        ColorBlock colorBlockWhite = button.colors;

        colorBlockWhite.normalColor = Color.white;
        colorBlockWhite.highlightedColor = Color.white;
        colorBlockWhite.selectedColor = Color.white;

        foreach (Button _button in MenuButtons)
        {
            _button.colors = colorBlockWhite;
        }

        ColorBlock colorBlock = button.colors;

        colorBlock.normalColor = Color.yellow;
        colorBlock.highlightedColor = Color.yellow;
        colorBlock.selectedColor = Color.yellow;

        button.colors = colorBlock;
    }
    #endregion

    #region 電視
    public void OpenTV()
    {
        StartCoroutine(FadeInOutManager.instance.DoFadeOut(1f, false));
        TVManager.ShowTV(true);
    }

    #endregion

    #region 淡入淡出功能
    IEnumerator FadeImage(Image targetImage, Color startColor, bool fadeIn, float fadeDuration, float targetAlphaInvoke ,Action onFadeComplete)
    {
        float startTime = Time.time;
        float elapsedTime = 0;

        float targetAlpha = fadeIn ? 1.0f : 0.0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            float currentAlpha = Mathf.Lerp(startColor.a, targetAlpha, t);
            Color currentColor = new Color(startColor.r, startColor.g, startColor.b, currentAlpha);

            targetImage.color = currentColor;

            if (Mathf.Abs(currentAlpha - targetAlphaInvoke) < 0.05f)
            {
                onFadeComplete?.Invoke();
            }

            yield return null;
        }

        targetImage.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        onFadeComplete?.Invoke();
    }

    #endregion

    #region 場景上驚嘆號
    /// <summary>
    /// 執行場景上的驚嘆號前往對應位置
    /// </summary>
    public void ClickToRoom(TeleportRoomPlace teleportRoomPlace)
    {
        ToolTipSystem.Hide();

        //啟動房間管理員
        SceneObjectsManager.EnableRoomManagerGroup(true);

        //開啟天花版
        SceneObjectsManager.EnableGameObject(SceneObjectsManager.CeilingObject, true);

        GameManager.OverLookMode_ClickMark(teleportRoomPlace);
    }
    #endregion

    /// <summary>
    /// 主畫面空間切換的物件
    /// </summary>
    /// <param name="isShow"></param>
    public void EnableRoomSwitchObject(bool isShow)
    {
        if (UI_RoomSwitch != null)
        {
            UI_RoomSwitch.SetActive(isShow);
        }
        else
        {
            Debug.LogError($"Please double-check the {UI_RoomSwitch} object, it seems to be missing.");
        }
    }

    /// <summary>
    /// 離開專案
    /// </summary>
    void ExitGame()
    {
        Application.Quit();
    }
}
