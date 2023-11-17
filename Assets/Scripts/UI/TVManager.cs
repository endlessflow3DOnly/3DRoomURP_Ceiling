using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TVManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;
    [SerializeField] VideoPlayerManager VideoPlayerManager;
    [SerializeField] UIManager UIManager;
    CameraManager CameraManager;

    [Header("TV UI")]
    public GameObject UI_TV;
    [SerializeField] RawImage RawImg_videoImg;
    [SerializeField] float switchInterval;

    #region 主要大圖UI
    [Header("主要大圖")]
    [SerializeField] Image Img_MainPictureOjbect;
    [SerializeField] List<Sprite> Spr_MainPictures;
    int currentUITVIndex = 0; //TVUI內的大圖index

    [Header("左上角小圖")]
    [SerializeField] Image Img_LittlePictureOjbect;
    [SerializeField] List<Sprite> Spr_LittlePictures;
    Vector2 LittlePicStartPos = Vector2.zero;
    Vector2 targetPosImgLittlePic = Vector2.zero;
    #endregion

    [Header("右上角功能")]
    [SerializeField] Button Btn_Back;
    Vector2 BtnBackStartPos = Vector2.zero;
    Vector2 targetPosBtnBack = Vector2.zero;

    [Header("右下功能用按鈕")]
    [SerializeField] Text Text_Option;
    [SerializeField] Text Text_AutoPlay;
    [SerializeField] Button Btn_OptionClose;
    [SerializeField] Toggle Toggle_AutoPlay;
    [SerializeField] Button Btn_TVClose;

    [Header("左下方物件")]
    [SerializeField] Button Btn_DownLeft_PreviousPicture;
    [SerializeField] Button Btn_DownLeft_NextPicture;

    [Header("正下方物件")]
    [SerializeField] GameObject DownObject;
    [SerializeField] Button Btn_Down_PreviousPicture;
    [SerializeField] Button Btn_Down_NextPicture;
    [SerializeField] Color Color_UnClicked;
    [SerializeField] Color Color_CurrentSelect;
    [SerializeField] List<Button> Btns_DownMid;
    [SerializeField] Image Img_Background;
    Vector2 DownObjectStartPos = Vector2.zero;
    Vector2 targetPosDownObject = Vector2.zero;

    [Header("當前選項開關")]
    public bool isOptionOpened = false;

    [Header("TV內UI淡出淡出用")]
    [SerializeField] Image Img_FadeInOut;
    private Coroutine CoroutineFadeInImg;
    private Coroutine CoroutineFadeOutImg;
    private Color startColor;
    bool isCycling = false;

    public void Init()
    {
        CameraManager = GameManager.CameraManager;

        InitDownMidButtonsState();

        //記錄左上小圖初始位置和終點位置
        LittlePicStartPos = Img_LittlePictureOjbect.rectTransform.anchoredPosition;
        targetPosImgLittlePic = new Vector2(-927, 800);

        //紀錄下方物件初始位置和終點位置
        DownObjectStartPos = DownObject.GetComponent<RectTransform>().anchoredPosition;
        targetPosDownObject = new Vector2(0, -137);

        //紀錄返回鍵的初始位置和終點位置
        BtnBackStartPos = Btn_Back.GetComponent<RectTransform>().anchoredPosition;
        targetPosBtnBack = new Vector2(865, 479);

        Img_FadeInOut.color = startColor;

        InitListener();
    }

    void InitListener()
    {
        Btn_OptionClose.onClick.AddListener(() => ShowDownContent(false));

        Toggle_AutoPlay.onValueChanged.AddListener(AutoPlay);
        Toggle_AutoPlay.isOn = false;

        Btn_Back.onClick.AddListener(() => ShowDownContent(true));
        Btn_TVClose.onClick.AddListener(() => ShowTV(false));

        Btn_DownLeft_PreviousPicture.onClick.AddListener(ShowPreviousImage);
        Btn_DownLeft_NextPicture.onClick.AddListener(ShowNextImage);

        Btn_Down_PreviousPicture.onClick.AddListener(ShowPreviousImage);
        Btn_Down_NextPicture.onClick.AddListener(ShowNextImage);
    }



    #region 電視按鈕左下方 [大圖操控 TV左下、下方，左右按鈕控制]
    void InitDownMidButtonsState()
    {
        currentUITVIndex = 0;

        //Reset
        foreach (var btn in Btns_DownMid)
        {
            btn.GetComponent<Image>().color = Color_UnClicked;
            btn.onClick.AddListener(() => LittleBtnsChangeMainPicture(btn));
        }

        ShowImage(currentUITVIndex);
        UpdateLittleButtonsHighlight();
    }

    public void ShowNextImage()
    {
        currentUITVIndex = currentUITVIndex + 1;

        if (currentUITVIndex > Spr_MainPictures.Count - 1)
        {
            currentUITVIndex = 0;
        }

        ShowImage(currentUITVIndex);
        UpdateLittleButtonsHighlight();
    }

    public void ShowPreviousImage()
    {
        currentUITVIndex = currentUITVIndex - 1;

        if (currentUITVIndex < 0)
        {
            currentUITVIndex = Spr_MainPictures.Count - 1;
        }

        ShowImage(currentUITVIndex);
        UpdateLittleButtonsHighlight();

    }

    public void LittleBtnsChangeMainPicture(Button selectedButton)
    {
        int selectedIndex = Btns_DownMid.IndexOf(selectedButton);

        if (selectedIndex != -1)
        {
            for (int i = 0; i < Btns_DownMid.Count; i++)
            {
                bool isSelected = (i == selectedIndex);
                Color newColor = isSelected ? Color_CurrentSelect : Color_UnClicked;
                Image imageComponent = Btns_DownMid[i].GetComponent<Image>();
                imageComponent.color = newColor;
            }

            currentUITVIndex = selectedIndex;
            ShowImage(selectedIndex);
        }
    }

    private void UpdateLittleButtonsHighlight()
    {
        foreach (var btn in Btns_DownMid)
        {
            btn.GetComponent<Image>().color = Color_UnClicked;
        }

        Image imageComponent = Btns_DownMid[currentUITVIndex].GetComponent<Image>();
        imageComponent.color = Color_CurrentSelect;
    }

    private void ShowImage(int index)
    {
        if (index >= 0 && index < Spr_MainPictures.Count)
        {
            Img_MainPictureOjbect.sprite = Spr_MainPictures[index];
            Img_LittlePictureOjbect.sprite = Spr_LittlePictures[index];
        }
    }

    #endregion

    /// <summary>
    /// 開關電視
    /// </summary>
    /// <param name="isShow"></param>
    public void ShowTV(bool isShow)
    {
        if (isShow)
        {
            UI_TV.SetActive(isShow);
        }
        else
        {
            InitTVSetting();

            StartCoroutine(PlayerCloseTVMovie(isShow));
        }
    }

    void InitTVSetting()
    {
        StopCycling();

        Toggle_AutoPlay.isOn = false;
        Text_AutoPlay.text = Toggle_AutoPlay.isOn ? "關閉播放" : "自動播放";

        ShowDownContent(true);
    }

    IEnumerator PlayerCloseTVMovie(bool isShow)
    {
        RawImg_videoImg.gameObject.SetActive(!isShow);
        VideoPlayerManager.PlayTVCloseAnim();

        yield return FadeInOutManager.instance.DoFadeIn(3f, false);

        UI_TV.SetActive(isShow);
        RawImg_videoImg.gameObject.SetActive(isShow);

        CameraManager.SwitchToFirstPersonCamera();
        GameManager.EnablePlayer(true);
        UIManager.EnableUIMain(!isShow);
        yield return FadeInOutManager.instance.DoFadeOut(1f, false);
    }

    #region 電視按鈕右下功能

    /// <summary>
    /// 開關選項功能
    /// </summary>
    public void ShowDownContent(bool isShow)
    {
        StartCoroutine(DoAnimMove(isShow));
    }

    IEnumerator DoAnimMove(bool isShow)
    {
        if (isShow)
        {
            StartCoroutine(FadeImg(Img_Background, isShow, 1f));

            DownObject.GetComponent<RectTransform>().DOAnchorPos(DownObjectStartPos, 1f);

            Img_LittlePictureOjbect.GetComponent<RectTransform>().DOAnchorPos(LittlePicStartPos, 1f);

            Btn_Back.GetComponent<RectTransform>().DOAnchorPos(BtnBackStartPos, 1f);
        }
        else
        {
            DownObject.GetComponent<RectTransform>().DOAnchorPos(targetPosDownObject, 1f);

            Img_LittlePictureOjbect.GetComponent<RectTransform>().DOAnchorPos(targetPosImgLittlePic, 1f);

            Btn_Back.GetComponent<RectTransform>().DOAnchorPos(targetPosBtnBack, 1f);

            StartCoroutine(FadeImg(Img_Background, isShow, 1f));
        }
        yield return null;
    }

    /// <summary>
    /// 輪替播放圖片選項
    /// </summary>
    public void AutoPlay(bool isOn)
    {
        bool isAutoPlayOn = Toggle_AutoPlay.isOn;
        Text_AutoPlay.text = isAutoPlayOn ? "關閉播放" : "自動播放";

        if (isAutoPlayOn)
        {
            StartCycling();
        }
        else
        {
            StopCycling();
            StopAndResetFadeImage();
        }
    }

    /// <summary>
    /// 開始輪播TV圖片
    /// </summary>
    public void StartCycling()
    {
        isCycling = true;
        StartCoroutine(CyclingTVImages());
    }

    /// <summary>
    /// 關閉輪播TV圖片
    /// </summary>
    public void StopCycling()
    {
        isCycling = false;
        StopCoroutine(CyclingTVImages());
    }

    IEnumerator CyclingTVImages()
    {
        while (Toggle_AutoPlay.isOn)
        { 
            Img_FadeInOut.gameObject.SetActive(true);

            yield return new WaitForSeconds(switchInterval);

            if (isCycling == false)
            {
                yield break;
            }

            if (CoroutineFadeInImg != null)
            {
                StopCoroutine(CoroutineFadeInImg);
            }
            CoroutineFadeInImg = StartCoroutine(FadeImg(Img_FadeInOut, true, 1f));
           
            yield return CoroutineFadeInImg;

            currentUITVIndex = (currentUITVIndex + 1) % Spr_MainPictures.Count;

            Img_MainPictureOjbect.sprite = Spr_MainPictures[currentUITVIndex];
            Img_LittlePictureOjbect.sprite = Spr_LittlePictures[currentUITVIndex];
            UpdateLittleButtonsHighlight();

            if (CoroutineFadeOutImg != null)
            {
                StopCoroutine(CoroutineFadeOutImg);
            }
            CoroutineFadeOutImg = StartCoroutine(FadeImg(Img_FadeInOut, false, 1f));

            yield return CoroutineFadeOutImg;
        }
    }

    #endregion

    #region 淡入淡出
    /// <summary>
    /// 給場景電視用淡入淡出
    /// </summary>
    /// <param name="image"></param>
    /// <param name="isShow"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator FadeImg(Image image, bool isShow, float duration)
    {
        float startAlpha = image.color.a;
        float targetAlpha = isShow ? 1.0f : 0.0f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            Color newColor = image.color;
            newColor.a = newAlpha;
            image.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 確保最終設置目標 alpha 值
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
    }

    /// <summary>
    /// 停止淡入淡出的功能
    /// </summary>
    void StopAndResetFadeImage()
    {
        Img_FadeInOut.color = startColor;
        Img_FadeInOut.gameObject.SetActive(false);

        if (CoroutineFadeInImg != null)
        {
            StopCoroutine(CoroutineFadeInImg);
        }

        if (CoroutineFadeOutImg != null)
        {
            StopCoroutine(CoroutineFadeOutImg);
        }
    }
    #endregion
}
