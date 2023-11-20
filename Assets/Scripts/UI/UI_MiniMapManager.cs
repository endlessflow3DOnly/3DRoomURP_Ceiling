using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_MiniMapManager : MonoBehaviour
{
    /*處理小地圖玩家位置顯示*/

    [SerializeField] GameManager GameManager;
    [SerializeField] CameraManager CameraManager;
    [SerializeField] Camera MinimapCamera;
    [SerializeField] Camera FPCamera;

    [Header("小地圖的")]
    [SerializeField] GameObject UI_MiniMap;
    [SerializeField] RenderTexture RenderTexture;
    public List<Button> Btns_MovePos;
    [SerializeField] GameObject BMDEMarksParent;
    [SerializeField] GameObject CMDEMarksParent;
    [SerializeField] List<Button> Btns_ExclamationMark_BMD;
    [SerializeField] List<Button> Btns_ExclamationMark_CMD;
    bool isMiniMapOpen = false;
    private PlayerController playerController;

    [Header("玩家位置顯示")]
    [SerializeField] Image PlayerIcon;         // Canvas 上的 Image 元件，用來表示玩家位置
    [SerializeField] Vector3 relativeOffset;   // 相對於玩家位置的偏移量
    [SerializeField] float zRotationOffset = 0f; // Z軸旋轉的偏移值

    [Header("移動變量等比縮放")]
    [SerializeField] float scaleFactor = 0.66f;

    public bool isEnable;

    private RectTransform playerIconRectTransform; // playerIcon 的 RectTransform

    public void Init()
    {
        playerIconRectTransform = PlayerIcon.GetComponent<RectTransform>();
        playerController = GameManager.PlayerController;
        FPCamera = CameraManager.FPCamera;

        //小地圖按鈕
        Btns_MovePos[0].onClick.AddListener(() => playerController.ClickToMove(0));
        Btns_MovePos[1].onClick.AddListener(() => playerController.ClickToMove(1));
        Btns_MovePos[2].onClick.AddListener(() => playerController.ClickToMove(2));
        Btns_MovePos[3].onClick.AddListener(() => playerController.ClickToMove(3));
        Btns_MovePos[4].onClick.AddListener(() => playerController.ClickToMove(4));
        Btns_MovePos[5].onClick.AddListener(() => playerController.ClickToMove(5));
        Btns_MovePos[6].onClick.AddListener(() => playerController.ClickToMove(6));
        Btns_MovePos[7].onClick.AddListener(() => playerController.ClickToMove(7));
        Btns_MovePos[8].onClick.AddListener(() => playerController.ClickToMove(8));

        Btns_ExclamationMark_BMD[0].onClick.AddListener(() => playerController.MoveBtnExclamationMark(TeleportRoomPlace.Restroom01, GameManager));
        Btns_ExclamationMark_BMD[1].onClick.AddListener(() => playerController.MoveBtnExclamationMark(TeleportRoomPlace.Bathroom01, GameManager));
        Btns_ExclamationMark_BMD[2].onClick.AddListener(() => playerController.MoveBtnExclamationMark(TeleportRoomPlace.Balcony01, GameManager));

        Btns_ExclamationMark_CMD[0].onClick.AddListener(() => playerController.MoveBtnExclamationMark(TeleportRoomPlace.Bathroom01, GameManager));
        Btns_ExclamationMark_CMD[1].onClick.AddListener(() => playerController.MoveBtnExclamationMark(TeleportRoomPlace.Balcony01, GameManager));

        CameraManager.MiniMapCamera.enabled = true;

        //MinimapCamera.targetTexture.Release();
    }

    void Update()
    {
        if (GameManager != null)
        {
            if (GameManager.IsAllFinishInit == false)
                return;
            else
            {
                // 檢查是否分配了玩家相機
                if (FPCamera != null && FPCamera.enabled == true)
                {
                    // 獲取玩家相機的Y軸旋轉
                    float cameraRotationY = FPCamera.transform.eulerAngles.y;

                    // 計算新的位置，相對於玩家位置
                    Vector3 newPosition = (FPCamera.transform.position + relativeOffset) * scaleFactor;

                    // 設定新的位置
                    playerIconRectTransform.position = MinimapCamera.WorldToScreenPoint(newPosition);

                    // 設定新的旋轉角度，將Y軸旋轉轉換為Z軸旋轉，並加上偏移值
                    playerIconRectTransform.rotation = Quaternion.Euler(0f, 0f, -cameraRotationY + zRotationOffset);
                }
            }
        }
    }

    #region 小地圖
    /// <summary>
    /// 是否秀出小地圖 (Toggle用)
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowMiniMap_ToggleUsed(bool isOn)
    {
        if (CameraManager.FPCamera.enabled == false)
        {
            return;
        }

        ShowMiniMap(isOn);
    }

    /// <summary>
    /// 是否秀出小地圖 (功能)
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowMiniMap(bool isShow)
    {
        EnableBtns_MiniMapExclamationMark(isShow);
        UI_MiniMap.SetActive(isShow);
    }

    /// <summary>
    /// 是否關閉移動用的按鈕
    /// </summary>
    /// <param name="isShow"></param>
    public void EnableBtns_MiniMapMove(bool isShow)
    {
        foreach (Button btn in Btns_MovePos)
        {
            btn.gameObject.SetActive(isShow);
        }
    }
    #endregion

    /// <summary>
    /// 是否開啟小地圖驚嘆號 (依照建材、工法)
    /// </summary>
    /// <param name="isShow"></param>
    public void EnableBtns_MiniMapExclamationMark(bool isShow)
    {
        foreach (var btn in Btns_ExclamationMark_BMD)
        {
            btn.gameObject.SetActive(false);
        }

        foreach (var btn in Btns_ExclamationMark_CMD)
        {
            btn.gameObject.SetActive(false);
        }

        GameManager.GameMode currentGameMode = GameManager.currentGameMode;

        switch (currentGameMode)
        {
            case GameManager.GameMode.BuildingMaterialDisplay:
                foreach (var btn in Btns_ExclamationMark_BMD)
                {
                    btn.gameObject.SetActive(isShow);
                }
                break;
            case GameManager.GameMode.ConstructionMethodDisplay:
                foreach (var btn in Btns_ExclamationMark_CMD)
                {
                    btn.gameObject.SetActive(isShow);
                }
                break;
        }
    }
}


