using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

//�Ҧ�����
public class GameManager : MonoBehaviour
{
    [Header("�ж���T��")]
    public List<RoomInfo_Asset> RoomInfo_Assets;
    public enum GameMode
    {
        MainMenu,    //�D���
        FirstPerson, //�Ĥ@�H�ټҦ�(�ۥѾ���)
        Navgation,   //�ɯ�Ҧ�(�Ŷ�����)
        Roaming,     //���C�Ҧ�
        OverLooking, //�����Ҧ�
        BuildingMaterialDisplay, //�ا��i��
        ConstructionMethodDisplay, //�u�k�i��
        BuildingMaterialSelection, //�ا���t
    }

    //��l���D���Ҧ�
    [Header("�w���Ҧ�")]
    public GameMode currentGameMode = GameMode.MainMenu;

    [Header("�D�n�޲z��")]
    public UIManager UIManager;
    [SerializeField] TVManager TVManager;
    public CameraManager CameraManager;
    public SceneObjectsManager SceneObjectsManager;
    public MouseOutlineSelectionManager MOSManager;
    public TeleportManager TeleportManager;
    public SettingManager SettingManager;
    public VideoPlayerManager VideoPlayerManager;

    [Header("UI�޲z��")]
    public UI_OverLookManager UI_OverLookManager;
    public UI_MiniMapManager UI_MiniMapManager;
    public UI_BuildMaterialManager UI_BuildMaterialManager;
    public UI_ConstructMethodManager UI_ConstructMethodManager;

    [Header("�����q������")]
    public TVCyclingManager TVCyclingManager;
    public TVCyclingManager TVCyclingManager2;
    public TVCyclingManager TVCyclingManager3;

    [Header("�ж��D�n�޲z��")]
    [SerializeField] Restroom01Manager Restroom01Manager;
    [SerializeField] Restroom02Manager Restroom02Manager;
    [SerializeField] Bathroom01Manager Bathroom01Manager;
    [SerializeField] Balcony01Manager Balcony01Manager;
    [SerializeField] LivingRoomManager LivingRoomManager; //�����ʹq�� x1

    [Header("���a")]
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

        Debug.Log("<color=white>" + this.name + " ��l�Ƨ���</color>");
    }

    /// <summary>
    /// ��l�Ƴ]�w
    /// </summary>
    void Init()
    {
        // �}�l�ɳ]�w��l�Ҧ�
        SwitchToMainMenu();
    }

    /// <summary>
    /// ������D���Ҧ�
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
    ///  ������Ĥ@�H�ټҦ�(�ۥѾ���)
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

    // ������Ȱ��Ҧ�(�ثe�S���ϥΡA���ᦳ�]�w�����i�H��)
    public void SwitchToPauseMode()
    {
        // �Ȱ��C���A��ܼȰ��e���A�T�Ϊ��a���
        //Time.timeScale = 0f;
        //uiManager.ShowPauseScreen();

        // �]�w��e�Ҧ����Ȱ�
        //currentGameMode = GameMode.Pause;
    }

    /// <summary>
    /// �����캩�C�Ҧ�
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
    /// ����������Ҧ�
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
    /// ������ا��i�ܼҦ� BuildingMatDisplay (BMD)
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
    /// ������u�k�i�ܼҦ� ConstructionMethodDisplay (CMD)
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
    /// ������ا���t�Ҧ�
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
    /// �O�_�Ұʪ��a�H�ΥL����v��
    /// </summary>
    /// <param name="enable"></param>
    public void EnablePlayer(bool enable)
    {
        //�T�Ϊ��a���
        if (Player != null && PlayerController != null)
        {
            Player.SetActive(enable);
            PlayerController.EnablePlayerControl(enable);
            CameraManager.EnableFPCamera(enable);
        }
    }

    #region �ا��i�ܭ����Ҧ��I�}��ĸ�
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
    /// �����Ҧ��]�w����
    /// </summary>
    void ResetAllSetting()
    {
        //�����оǭ��O
        UIManager.CloseAllTutorialPanel();

        //�����Ŷ������\��
        UIManager.EnableRoomSwitchObject(false);

        //�����p�a��
        UI_MiniMapManager.ShowMiniMap(false);

        //����UI��������
        UI_OverLookManager.EnableOverLookUI(false);

        //�����ӼҦ��Ҧ��۾�
        CameraManager.ResetAndCloseAllCameras();

        //�������a�B���a��v��
        EnablePlayer(false);
        CameraManager.EnableFPCamera(false); //�n�������M�ܯӰO����

        //�����Ѫ�O�B���������W����ĸ����� (�]�A�Ĥ@�H�٬ݪ���ĸ� �ا��B�u�k)
        SceneObjectsManager.ShowCeilingObject(false);
        SceneObjectsManager.CloseAllEMark();
        SceneObjectsManager.EnableRoomManagerGroup(false);

        //�q���ܦ��}�i�ܥΪ�
        LivingRoomManager.ShowInteractableObjects(false);
        LivingRoomManager.ShowDisplayForniture(true);

        //�����p�a�ϡB�������Ҧ� (���|�I�B�ا��B�u�k ��ĸ�)
        UI_MiniMapManager.EnableBtns_MiniMapMove(false);
        UI_MiniMapManager.EnableBtns_MiniMapExclamationMark(false);

        //�����Ҧ����� �p(�ا��B�u�kUI) �A�����u�kUI�i�H���m������toggle�MRelease�v��
        UI_BuildMaterialManager.EnableBMDUI(false);
        UI_ConstructMethodManager.EnableCMDUI(false);
        VideoPlayerManager.CMD_ResetTogglePauseOrPlayState();

        //����RoomManagers
        SceneObjectsManager.EnableRoomManagerGroup(false);

        //���m��v��
        CameraManager.OverlookCameraScript.ResetCamPosition();

        //�H�J�H�X����
        FadeInOutManager.instance.StopDoingFade();
    }

    void Update()
    {
        switch (currentGameMode)
        {
            case GameMode.MainMenu:
                // �b�D���Ҧ��U���޿�B�z
                break;

            case GameMode.FirstPerson:
                // �b�C���Ҧ��U���޿�B�z
                break;

            case GameMode.Navgation:
                // �b�Ȱ��Ҧ��U���޿�B�z
                break;

            case GameMode.Roaming:
                // �b���C�Ҧ��U���޿�B�z
                CameraManager.CheckRoamingPath();
                break;
        }
    }
}

