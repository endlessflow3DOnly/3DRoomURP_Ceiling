using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TeleportRoomPlace
{
    Restroom01,
    Restroom02,
    Bathroom01,
    LivingRoom,
    JPStyleRoom,
    StudyRoom,
    Balcony01,
    Balcony02,
    Balcony03,
}


//���a�ǰe��S�w�I�A�ë��w���ਤ��
public class TeleportManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("�ж��ˬd�I")]
    public Transform[] TeleportTransforms;

    GameObject Player;
    CharacterController CharacterController;
    Camera FPPlayerCam;
    FPMouseRotateCamera FPMouseRotateCamera;

    private int currentLocationIndex = 0;
    int roomIndex = 0;

    Transform targetLocation;
    Vector3 newPosition;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            TeleportToNextLocation();
        }
    }

    /// <summary>
    /// ��l��m�]�m
    /// </summary>
    public void Init()
    {
        Player = GameManager.Player;
        CharacterController = GameManager.Player.GetComponent<CharacterController>();
        FPPlayerCam = GameManager.CameraManager.FPCamera;
        FPMouseRotateCamera = FPPlayerCam.GetComponent<FPMouseRotateCamera>();

        for (int i = 0; i < GameManager.RoomInfo_Assets.Count; i++)
        {
            TeleportTransforms[i].position = GameManager.RoomInfo_Assets[i].TelportPos;
            TeleportTransforms[i].rotation = GameManager.RoomInfo_Assets[i].TelportRot;
        }

        if (TeleportTransforms.Length > 0 && CharacterController != null)
        {
            FPMouseRotateCamera.OnInitialized();
            TeleportToLocation(currentLocationIndex);
        }
        Debug.Log("<color=blue>" + this.name + " ��l�Ƨ���</color>");
    }

    /// <summary>
    /// �ǰe��U�@�Ӧ�m
    /// </summary>
    public void TeleportToNextLocation()
    {
        currentLocationIndex = (currentLocationIndex + 1) % TeleportTransforms.Length;
        TeleportToLocation(currentLocationIndex);
    }

    private void TeleportToLocation(int index)
    {
        if (index >= 0 && index < TeleportTransforms.Length && CharacterController != null)
        {
            targetLocation = TeleportTransforms[index];
            newPosition = targetLocation.position;

            CharacterController.enabled = false;
            Player.transform.position = newPosition;
            CharacterController.enabled = true;
 
            //�]�m�۾�������
            FPPlayerCam.transform.rotation = TeleportTransforms[index].rotation;
            FPMouseRotateCamera.ResetRotationX(TeleportTransforms[index].eulerAngles.x);
            FPMouseRotateCamera.ResetRotationY(TeleportTransforms[index].eulerAngles.y);
        }
    }

    public void SwitchToRoom(TeleportRoomPlace teleportRoomPlace)
    {
        Debug.Log($"�ǰe�쪺�a�� {teleportRoomPlace}");

        TeleportToLocation((int)teleportRoomPlace);
    }
}
