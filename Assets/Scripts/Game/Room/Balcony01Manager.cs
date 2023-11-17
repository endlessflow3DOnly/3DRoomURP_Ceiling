using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcony01Manager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("���a�b�ж���")]
    [SerializeField] bool IsplayerInRoom;

    [Header("�i���ʪ���a��")]
    public List<GameObject> interactableObjects;

    [Header("��ĸ� �ا��i��")]
    [SerializeField] List<GameObject> BMD_EMarkObjects;

    [Header("��ĸ� �u�k�i��")]
    [SerializeField] List<GameObject> CMD_EMarkObjects;

    public void Init()
    {
        ShowInteractableObjects(false);

        Debug.Log("<color=green>" + this.name + " ��l�Ƨ���</color>");
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
