using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConstructMethodManager : MonoBehaviour
{
    [Header("�u�k�i�� (ConstructionMethodMode [CMD])")]
    [SerializeField] GameObject UI_CMD;
    public Toggle Toggle_PauseOrPlay;
    public TMP_Text Text_PauseOrPlay;
    [SerializeField] Button Btn_CMD_Back;

    [Header("�ѦҪ��޲z��")]
    [SerializeField] VideoPlayerManager VideoPlayerManager;

    // Start is called before the first frame update
    public void Init()
    {
        Btn_CMD_Back.onClick.AddListener(() => EnableCMDUI(false));

        Toggle_PauseOrPlay.isOn = false;
        Toggle_PauseOrPlay.onValueChanged.AddListener(VideoPlayerManager.ToggleVideoPlay);
    }

    /// <summary>
    /// �����W��ĸ��}�ҹ���UI (�u�k�i��)
    /// </summary>
    /// <param name="index"></param>
    public void OpenCMDUI(int index)
    {
        EnableCMDUI(true);
        VideoPlayerManager.EnableConstructMethodVideo(true, index, true);
    }

    public void EnableCMDUI(bool isShow)
    {
        UI_CMD.SetActive(isShow);
    }
}
