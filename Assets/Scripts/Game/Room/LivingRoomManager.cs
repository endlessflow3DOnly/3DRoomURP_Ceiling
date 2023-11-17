using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("���a�b�ж���")]
    [SerializeField] bool IsplayerInRoom;

    [Header("�i���ʪ���a��")]
    [SerializeField] List<GameObject> InteractableFurnitures;

    [Header("�i�ܥήa��")]
    [SerializeField] List<GameObject> InteractableFurnitures_Display;

    [Header("��ĸ� �ا��i��")]
    [SerializeField] List<GameObject> BMD_EMarkObjects;

    [Header("��ĸ� �u�k�i��")]
    [SerializeField] List<GameObject> CMD_EMarkObjects;

    public void Init()
    {
        ShowInteractableObjects(false);

        Debug.Log("<color=green>" + this.name + " ��l�Ƨ���</color>");
    }

    /// <summary>
    /// �O�_��ܥi���ʪ���
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
