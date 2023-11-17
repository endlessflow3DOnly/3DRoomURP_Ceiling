using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("俯瞰模式驚嘆號群組")]
    public GameObject OL_BMDMode_EMarkGroup;
    public GameObject OL_CMDMode_EMarkGroup;
    public GameObject OL_BMSMode_EMarkGroup;

    [Header("第一人稱模式驚嘆號群組")]
    public GameObject FP_BMDMode_EMarkGroup;
    public GameObject FP_CMDMode_EMarkGroup;

    [Header("天花板")]
    public GameObject CeilingObject;
    public GameObject NotShootLightObject;

    [Header("房間管理員")]
    public GameObject RoomManagerGroup;

    [Header("房間內互動家具 (可Outline)")]
    public List<GameObject> RoomInteractableObjects;

    /// <summary>
    /// 顯示指定物件與否，會先關掉全部驚嘆號
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="isShow"></param>
    public void EnableGameObject(GameObject gameObject, bool isShow)
    {
        CloseAllEMark();

        gameObject.SetActive(isShow);
    }

    /// <summary>
    /// 顯示家具互動物件與否
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowRoomInteractableFurnitures(bool isShow)
    {
        for (int i = 0; i < RoomInteractableObjects.Count; i++)
        {
            RoomInteractableObjects[i].SetActive(isShow);
        }
    }

    /// <summary>
    /// 顯示天花板與否 (還有擋光物件)
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowCeilingObject(bool isShow)
    {
        CeilingObject.SetActive(isShow);
        NotShootLightObject.SetActive(isShow);
    }

    /// <summary>
    /// 重製並關掉全部驚嘆號 (小地圖+第一人稱)
    /// </summary>
    public void CloseAllEMark()
    {
        OL_BMDMode_EMarkGroup.gameObject.SetActive(false);
        OL_CMDMode_EMarkGroup.gameObject.SetActive(false);
        FP_BMDMode_EMarkGroup.gameObject.SetActive(false);
        FP_CMDMode_EMarkGroup.gameObject.SetActive(false);
    }

    /// <summary>
    /// 是否開啟房間管理器們
    /// </summary>
    /// <param name="isShow"></param>
    public void EnableRoomManagerGroup(bool isShow)
    {
        RoomManagerGroup.SetActive(isShow);
    }
}
