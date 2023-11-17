using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room Info", menuName = "RoomInfoAsset")]
public class RoomInfo_Asset : ScriptableObject
{
    public string RoomName;

    [Header("�D�e�����ϥ�")]
    public Sprite Spr_MainTex;

    [Header("�ǰe���ϰ�")]
    public TeleportRoomPlace teleportRoomPlace;

    [Header("�ǰe�Ϊ���m�����")]
    public Vector3 TelportPos;
    public Quaternion TelportRot;

    public void SetPositionAndRotation(Vector3 postion, Quaternion rotation)
    {
        TelportPos = postion;
        TelportRot = rotation;
    }
}
