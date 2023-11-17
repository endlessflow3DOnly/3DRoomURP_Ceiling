using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildMaterialManager : MonoBehaviour
{
    [Header("建材展示 (BuildingMaterailMode [BMD])")]
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
    /// 工法和建材展示的返回鍵
    /// </summary>
    public void CloseDisplayModeUI()
    {
        EnableBMDUI(false);
    }

    /// <summary>
    /// 場景上驚嘆號開啟對應UI (建材展示)
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
        //建材UI
        UI_BMD.SetActive(isShow);
    }
}
