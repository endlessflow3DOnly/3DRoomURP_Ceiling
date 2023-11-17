using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("玩家在房間內")]
    [SerializeField] bool IsplayerInRoom;

    [Header("可互動物件家具")]
    [SerializeField] List<GameObject> InteractableFurnitures;

    [Header("展示用家具")]
    [SerializeField] List<GameObject> InteractableFurnitures_Display;

    [Header("驚嘆號 建材展示")]
    [SerializeField] List<GameObject> BMD_EMarkObjects;

    [Header("驚嘆號 工法展示")]
    [SerializeField] List<GameObject> CMD_EMarkObjects;

    public void Init()
    {
        ShowInteractableObjects(false);

        Debug.Log("<color=green>" + this.name + " 初始化完成</color>");
    }

    /// <summary>
    /// 是否顯示可互動物件
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowInteractableObjects(bool isShow)
    {
        for (int i = 0; i < InteractableFurnitures.Count; ++i)
        {
            InteractableFurnitures[i].gameObject.SetActive(isShow);
        }

        bool showBMD_EMarkObjects = GameManager.currentGameMode == GameManager.GameMode.BuildingMaterialDisplay;
        bool showCMD_EMarkObjects = GameManager.currentGameMode == GameManager.GameMode.ConstructionMethodDisplay;

        foreach (var bmdObject in BMD_EMarkObjects)
        {
            bmdObject.gameObject.SetActive(showBMD_EMarkObjects);
        }

        foreach (var cmdObject in CMD_EMarkObjects)
        {
            cmdObject.gameObject.SetActive(showCMD_EMarkObjects);
        }
    }

    public void ShowDisplayForniture(bool isShow)
    {
        for (int i = 0; i < InteractableFurnitures_Display.Count; i++)
        {
            InteractableFurnitures_Display[i].SetActive(isShow);
        }
    }    

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            IsplayerInRoom = true;
            ShowInteractableObjects(true);
            ShowDisplayForniture(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            IsplayerInRoom = false;
            ShowInteractableObjects(false);
            ShowDisplayForniture(true);
        }
    }
}
