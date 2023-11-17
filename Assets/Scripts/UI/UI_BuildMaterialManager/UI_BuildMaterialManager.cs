using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildMaterialManager : MonoBehaviour
{
    [Header("�ا��i�� (BuildingMaterailMode [BMD])")]
    [SerializeField] GameObject UI_BMD;
    [SerializeField] Image Img_BMDPictureObj;
    [SerializeField] List<Sprite> Spr_BMDMainPicture;
    [SerializeField] Button Btn_BMD_Back;

    // Start is called before the first frame update
    public void Init()
    {
        Btn_BMD_Back.onClick.AddListener(() => CloseDisplayModeUI());
    }

    /// <summary>
    /// �u�k�M�ا��i�ܪ���^��
    /// </summary>
    public void CloseDisplayModeUI()
    {
        EnableBMDUI(false);
    }

    /// <summary>
    /// �����W��ĸ��}�ҹ���UI (�ا��i��)
    /// </summary>
    /// <param name="index"></param>
    public void OpenBMDUI(int index)
    {
        //GameManager.SetBMDOpenSetting(true);
        Debug.Log(index);
        EnableBMDUI(true);
        Img_BMDPictureObj.sprite = Spr_BMDMainPicture[index];
    }

    public void EnableBMDUI(bool isShow)
    {
        //�ا�UI
        UI_BMD.SetActive(isShow);
    }
}
