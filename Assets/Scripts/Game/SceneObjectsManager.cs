using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("�����Ҧ���ĸ��s��")]
    public GameObject OL_BMDMode_EMarkGroup;
    public GameObject OL_CMDMode_EMarkGroup;
    public GameObject OL_BMSMode_EMarkGroup;

    [Header("�Ĥ@�H�ټҦ���ĸ��s��")]
    public GameObject FP_BMDMode_EMarkGroup;
    public GameObject FP_CMDMode_EMarkGroup;

    [Header("�Ѫ�O")]
    public GameObject CeilingObject;
    public GameObject NotShootLightObject;

    [Header("�ж��޲z��")]
    public GameObject RoomManagerGroup;

    [Header("�ж������ʮa�� (�iOutline)")]
    public List<GameObject> RoomInteractableObjects;

    /// <summary>
    /// ��ܫ��w����P�_�A�|������������ĸ�
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="isShow"></param>
    public void EnableGameObject(GameObject gameObject, bool isShow)
    {
        CloseAllEMark();

        gameObject.SetActive(isShow);
    }

    /// <summary>
    /// ��ܮa�㤬�ʪ���P�_
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
    /// ��ܤѪ�O�P�_ (�٦��ץ�����)
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowCeilingObject(bool isShow)
    {
        CeilingObject.SetActive(isShow);
        NotShootLightObject.SetActive(isShow);
    }

    /// <summary>
    /// ���s������������ĸ� (�p�a��+�Ĥ@�H��)
    /// </summary>
    public void CloseAllEMark()
    {
        OL_BMDMode_EMarkGroup.gameObject.SetActive(false);
        OL_CMDMode_EMarkGroup.gameObject.SetActive(false);
        FP_BMDMode_EMarkGroup.gameObject.SetActive(false);
        FP_CMDMode_EMarkGroup.gameObject.SetActive(false);
    }

    /// <summary>
    /// �O�_�}�ҩж��޲z����
    /// </summary>
    /// <param name="isShow"></param>
    public void EnableRoomManagerGroup(bool isShow)
    {
        RoomManagerGroup.SetActive(isShow);
    }
}
