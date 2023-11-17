using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcony01Manager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("玩家在房間內")]
    [SerializeField] bool IsplayerInRoom;

    [Header("可互動物件家具")]
    public List<GameObject> interactableObjects;

    [Header("驚嘆號 建材展示")]
    [SerializeField] List<GameObject> BMD_EMarkObjects;

    [Header("驚嘆號 工法展示")]
    [SerializeField] List<GameObject> CMD_EMarkObjects;

    public void Init()
    {
        ShowInteractableObjects(false);

        Debug.Log("<color=green>" + this.name + " 初始化完成</color>");
    }


    void ShowInteractableObjects(bool isShow)
    {
        for (int i = 0; i < interactableObjects.Count; ++i)
        {
            interactableObjects[i].gameObject.SetActive(isShow);
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            IsplayerInRoom = true;
            ShowInteractableObjects(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            IsplayerInRoom = false;
            ShowInteractableObjects(false);
        }
    }
}
