using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

//模式切換
public class GameManager : MonoBehaviour
{
    [Header("房間資訊用")]
    public List<RoomInfo_Asset> RoomInfo_Assets;
    public enum GameMode
    {
        MainMenu,    //主選單
        FirstPerson, //第一人稱模式(自由導覽)
        Navgation,   //導航模式(空間導覽)
        Roaming,     //漫遊模式
        OverLooking, //俯瞰模式
        BuildingMaterialDisplay, //建材展示
        ConstructionMethodDisplay, //工法展示
        BuildingMaterialSelection, //建材選配
    }

    //初始為主選單模式
    [Header("預覽模式")]
    public GameMode currentGameMode = GameMode.MainMenu;

    [Header("主要管理器")]
    public UIManager UIManager;
    [SerializeField] TVManager TVManager;
    public CameraManager CameraManager;
    public SceneObjectsManager SceneObjectsManager;
    public MouseOutlineSelectionManager MOSManager;
    public TeleportManager TeleportManager;
    public SettingManager SettingManager;
    public VideoPlayerManager VideoPlayerManager;

    [Header("UI管理器")]
    public UI_OverLookManager UI_OverLookManager;
    public UI_MiniMapManager UI_MiniMapManager;
    public UI_BuildMaterialManager UI_BuildMaterialManager;
    public UI_ConstructMethodManager UI_ConstructMethodManager;

    [Header("場景電視輪播")]
    public TVCyclingManager TVCyclingManager;
    public TVCyclingManager TVCyclingManager2;
    public TVCyclingManager TVCyclingManager3;

    [Header("房間主要管理器")]
    [SerializeField] Restroom01Manager Restroom01Manager;
    [SerializeField] Restroom02Manager Restroom02Manager;
    [SerializeField] Bathroom01Manager Bathroom01Manager;
    [SerializeField] Balcony01Manager Balcony01Manager;
    [SerializeField] LivingRoomManager LivingRoomManager; //有互動電視 x1

    [Header("玩家")]
    public GameObject Player;
    public PlayerController PlayerController;

    public bool IsAllFinishInit = false;

    void Start()
    {
        PlayerController.InitPlayer();

        UIManager.Init();
        CameraManager.Init();

        VideoPlayerManager.Init();

        UI_MiniMapManager.Init();
        UI_OverLookManager.Init();
        UI_BuildMaterialManager.Init();
        UI_ConstructMethodManager.Init();

        TVManager.Init();
        MOSManager.Init();
        SettingManager.Init();
        TeleportManager.Init();

        TVCyclingManager.Init();
        TVCyclingManager2.Init();
        TVCyclingManager3.Init();

        Restroom01Manager.Init();
        Restroom02Manager.Init();
        Bathroom01Manager.Init();
        Balcony01Manager.Init();
        LivingRoomManager.Init();

        Init();

        IsAllFinishInit = true;

        Debug.Log("<color=white>" + this.name + " 初始化完成</color>");
    }

    /// <summary>
    /// 初始化設定
    /// </summary>
    void Init()
    {
        // 開始時設定初始模式
        SwitchToMainMenu();
    }

    /// <summary>
    /// 切換到主選單模式
    /// </summary>
    public void SwitchToMainMenu()
    {       
        currentGameMode = GameMode.MainMenu;

        EnablePlayer(true);

        Player.transform.position = RoomInfo_Assets[0].TelportPos;

        ResetAllSetting();

        UIManager.EnableRoomSwitchObject(true);

        CameraManager.SwitchToMainUICamera();
    }

    /// <summary>
    ///  切換到第一人稱模式(自由導覽)
    /// </summary>
    public void SwitchToFirstPersonMode()
    {
        currentGameMode = GameMode.FirstPerson;

        ResetAllSetting();

        EnablePlayer(true);
        SceneObjectsManager.ShowCeilingObject(true);
        SceneObjectsManager.EnableRoomManagerGroup(true);

        UI_MiniMapManager.ShowMiniMap(true);
        UI_MiniMapManager.EnableBtns_MiniMapMove(true);

        MOSManager.OutlineClickCamera = CameraManager.FPCamera;

        CameraManager.SwitchToFirstPersonCamera();
    }

    // 切換到暫停模式(目前沒有使用，之後有設定介面可以用)
    public void SwitchToPauseMode()
    {
        // 暫停遊戲，顯示暫停畫面，禁用玩家控制等
        //Time.timeScale = 0f;
        //uiManager.ShowPauseScreen();

        // 設定當前模式為暫停
        //currentGameMode = GameMode.Pause;
    }

    /// <summary>
    /// 切換到漫遊模式
    /// </summary>
    public void SwitchToRoamingMode()
    {        
        if(currentGameMode == GameMode.Roaming)
        {
            return;
        }

        currentGameMode = GameMode.Roaming;

        ResetAllSetting();

        SceneObjectsManager.ShowCeilingObject(true);

        CameraManager.SwitchToRoamingCameras();
    }

    /// <summary>
    /// 切換到俯瞰模式
    /// </summary>
    public void SwitchToOverLookMode()
    {
        currentGameMode = GameMode.OverLooking;

        ResetAllSetting();

        UIManager.UIOverLookManager.EnableOverLookUI(true);
        MOSManager.OutlineClickCamera = CameraManager.OverlookCamera;
        CameraManager.SwitchToOverLookCamera();
    }

    // <summary>
    /// 切換到建材展示模式 BuildingMatDisplay (BMD)
    /// </summary>
    public void SwitchToBuildingMatDisplayMode()
    {
        currentGameMode = GameMode.BuildingMaterialDisplay;

        ResetAllSetting();

        UIManager.UIOverLookManager.EnableOverLookUI(true);
        MOSManager.OutlineClickCamera = CameraManager.OverlookCamera;
        SceneObjectsManager.EnableGameObject(SceneObjectsManager.OL_BMDMode_EMarkGroup, true);
        CameraManager.SwitchToOverLookCamera();
    }

    // <summary>
    /// 切換到工法展示模式 ConstructionMethodDisplay (CMD)
    /// </summary>
    public void SwitchToConstructionMethodDisplayMode()
    {
        currentGameMode = GameMode.ConstructionMethodDisplay;

        ResetAllSetting();

        UIManager.UIOverLookManager.EnableOverLookUI(true);
        MOSManager.OutlineClickCamera = CameraManager.OverlookCamera;
        SceneObjectsManager.EnableGameObject(SceneObjectsManager.OL_CMDMode_EMarkGroup, true);
        CameraManager.SwitchToOverLookCamera();
    }

    /// <summary>
    /// 切換到建材選配模式
    /// </summary>
    public void SwitchToBuildingMaterialsSelectionMode()
    {
        currentGameMode = GameMode.BuildingMaterialSelection;

        ResetAllSetting();

        UIManager.UIOverLookManager.EnableOverLookUI(true);
        MOSManager.OutlineClickCamera = CameraManager.OverlookCamera;
        SceneObjectsManager.EnableGameObject(SceneObjectsManager.OL_CMDMode_EMarkGroup, true);
        CameraManager.SwitchToOverLookCamera();
    }

    /// <summary>
    /// 是否啟動玩家以及他的攝影機
    /// </summary>
    /// <param name="enable"></param>
    public void EnablePlayer(bool enable)
    {
        //禁用玩家控制等
        if (Player != null && PlayerController != null)
        {
            Player.SetActive(enable);
            PlayerController.EnablePlayerControl(enable);
            CameraManager.EnableFPCamera(enable);
        }
    }

    #region 建材展示俯瞰模式點開驚嘆號
    public void OverLookMode_ClickMark(TeleportRoomPlace teleportRoomPlace)
    {
        UIManager.CloseAllTutorialPanel();
        UIManager.UI_MiniMapManager.ShowMiniMap(true);
        UIManager.UIOverLookManager.EnableOverLookUI(false);

        CameraManager.SwitchToFirstPersonCamera();

        FadeInOutManager.instance.StopDoingFade();

        if(currentGameMode == GameMode.BuildingMaterialDisplay)
        {
            SceneObjectsManager.EnableGameObject(SceneObjectsManager.FP_BMDMode_EMarkGroup, true);
        }
        else if (currentGameMode == GameMode.ConstructionMethodDisplay)
        {
            SceneObjectsManager.EnableGameObject(SceneObjectsManager.FP_CMDMode_EMarkGroup, true);
        }

        MOSManager.OutlineClickCamera = CameraManager.FPCamera;

        EnablePlayer(true);
        PlayerController.MoveBtnExclamationMark(teleportRoomPlace, this);
    }
    #endregion

    /// <summary>
    /// 關掉所有設定物件
    /// </summary>
    void ResetAllSetting()
    {
        //關掉教學面板
        UIManager.CloseAllTutorialPanel();

        //關閉空間切換功能
        UIManager.EnableRoomSwitchObject(false);

        //關掉小地圖
        UI_MiniMapManager.ShowMiniMap(false);

        //關掉UI俯瞰介面
        UI_OverLookManager.EnableOverLookUI(false);

        //關閉該模式所有相機
        CameraManager.ResetAndCloseAllCameras();

        //關掉玩家、玩家攝影機
        EnablePlayer(false);
        CameraManager.EnableFPCamera(false); //要關掉不然很耗記憶體

        //關閉天花板、關閉場景上的驚嘆號物件 (包括第一人稱看的驚嘆號 建材、工法)
        SceneObjectsManager.ShowCeilingObject(false);
        SceneObjectsManager.CloseAllEMark();
        SceneObjectsManager.EnableRoomManagerGroup(false);

        //電視變成開展示用的
        LivingRoomManager.ShowInteractableObjects(false);
        LivingRoomManager.ShowDisplayForniture(true);

        //關閉小地圖、並關閉所有 (路徑點、建材、工法 驚嘆號)
        UI_MiniMapManager.EnableBtns_MiniMapMove(false);
        UI_MiniMapManager.EnableBtns_MiniMapExclamationMark(false);

        //關閉所有介面 如(建材、工法UI) ，關閉工法UI可以重置播放鍵toggle和Release影片
        UI_BuildMaterialManager.EnableBMDUI(false);
        UI_ConstructMethodManager.EnableCMDUI(false);
        VideoPlayerManager.CMD_ResetTogglePauseOrPlayState();

        //關閉RoomManagers
        SceneObjectsManager.EnableRoomManagerGroup(false);

        //重置攝影機
        CameraManager.OverlookCameraScript.ResetCamPosition();

        //淡入淡出停止
        FadeInOutManager.instance.StopDoingFade();
    }

    void Update()
    {
        switch (currentGameMode)
        {
            case GameMode.MainMenu:
                // 在主選單模式下的邏輯處理
                break;

            case GameMode.FirstPerson:
                // 在遊戲模式下的邏輯處理
                break;

            case GameMode.Navgation:
                // 在暫停模式下的邏輯處理
                break;

            case GameMode.Roaming:
                // 在漫遊模式下的邏輯處理
                CameraManager.CheckRoamingPath();
                break;
        }
    }
}

