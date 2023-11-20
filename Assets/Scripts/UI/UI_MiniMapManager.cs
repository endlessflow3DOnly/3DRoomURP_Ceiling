using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_MiniMapManager : MonoBehaviour
{
    /*�B�z�p�a�Ϫ��a��m���*/

    [SerializeField] GameManager GameManager;
    [SerializeField] CameraManager CameraManager;
    [SerializeField] Camera MinimapCamera;
    [SerializeField] Camera FPCamera;

    [Header("�p�a�Ϫ�")]
    [SerializeField] GameObject UI_MiniMap;
    [SerializeField] RenderTexture RenderTexture;
    public List<Button> Btns_MovePos;
    [SerializeField] GameObject BMDEMarksParent;
    [SerializeField] GameObject CMDEMarksParent;
    [SerializeField] List<Button> Btns_ExclamationMark_BMD;
    [SerializeField] List<Button> Btns_ExclamationMark_CMD;
    bool isMiniMapOpen = false;
    private PlayerController playerController;

    [Header("���a��m���")]
    [SerializeField] Image PlayerIcon;         // Canvas �W�� Image ����A�ΨӪ�ܪ��a��m
    [SerializeField] Vector3 relativeOffset;   // �۹�󪱮a��m�������q
    [SerializeField] float zRotationOffset = 0f; // Z�b���઺������

    [Header("�����ܶq�����Y��")]
    [SerializeField] float scaleFactor = 0.66f;

    public bool isEnable;

    private RectTransform playerIconRectTransform; // playerIcon �� RectTransform

    public void Init()
    {
        playerIconRectTransform = PlayerIcon.GetComponent<RectTransform>();
        playerController = GameManager.PlayerController;
        FPCamera = CameraManager.FPCamera;

        //�p�a�ϫ��s
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
                // �ˬd�O�_���t�F���a�۾�
                if (FPCamera != null && FPCamera.enabled == true)
                {
                    // ������a�۾���Y�b����
                    float cameraRotationY = FPCamera.transform.eulerAngles.y;

                    // �p��s����m�A�۹�󪱮a��m
                    Vector3 newPosition = (FPCamera.transform.position + relativeOffset) * scaleFactor;

                    // �]�w�s����m
                    playerIconRectTransform.position = MinimapCamera.WorldToScreenPoint(newPosition);

                    // �]�w�s�����ਤ�סA�NY�b�����ഫ��Z�b����A�å[�W������
                    playerIconRectTransform.rotation = Quaternion.Euler(0f, 0f, -cameraRotationY + zRotationOffset);
                }
            }
        }
    }

    #region �p�a��
    /// <summary>
    /// �O�_�q�X�p�a�� (Toggle��)
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
    /// �O�_�q�X�p�a�� (�\��)
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowMiniMap(bool isShow)
    {
        EnableBtns_MiniMapExclamationMark(isShow);
        UI_MiniMap.SetActive(isShow);
    }

    /// <summary>
    /// �O�_�������ʥΪ����s
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
    /// �O�_�}�Ҥp�a����ĸ� (�̷ӫا��B�u�k)
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


