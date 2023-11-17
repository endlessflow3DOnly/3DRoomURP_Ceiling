using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room Info", menuName = "RoomInfoAsset")]
public class RoomInfo_Asset : ScriptableObject
{
    public string RoomName;

    [Header("主畫面的圖示")]
    public Sprite Spr_MainTex;

    [Header("傳送的區域")]
    public TeleportRoomPlace teleportRoomPlace;

    [Header("傳送用的位置跟旋轉")]
    public Vector3 TelportPos;
    public Quaternion TelportRot;

    public void SetPositionAndRotation(Vector3 postion, Quaternion rotation)
    {
        TelportPos = postion;
        TelportRot = rotation;
    }
}
