using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutManager : MonoBehaviour
{
    public static FadeInOutManager instance;

    [SerializeField] Image Img_MainFadeInOut;      //FadeInOut��Image
    [SerializeField] Image Img_RoamingFadeInOut;   //���C��FadeInOut��Image

    private Color StartColor_Roaming; 
    private Color StartColor; 

    private Coroutine fadeInCoroutine;
    private Coroutine fadeOutCoroutine;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitImgColor();
    }

    /// <summary>
    /// �x�s�H�J�H�X�Ϊ��Ϥ���l�C��
    /// </summary>
    void InitImgColor()
    {
        StartColor_Roaming = Img_RoamingFadeInOut.color;
        StartColor = Img_MainFadeInOut.color;
    }

    private void Update()
    {

    }

    #region Test�Ϊ�
    public void TestFadeIn_Main()
    {
        StartCoroutine(DoFadeIn(2f, false));
    }

    public void TestFadeOut_Main()
    {
        StartCoroutine(DoFadeOut(2f, false));
    }

    public void TestFadeIn_Roaming()
    {
        StartCoroutine(DoFadeIn(2f, true));
    }

    public void TestFadeOut_Roaming()
    {
        StartCoroutine(DoFadeOut(2f, true));
    }
    #endregion

    IEnumerator FadeIn(float fadeInDuration, bool isRoaming)
    {
        (isRoaming ? Img_RoamingFadeInOut : Img_MainFadeInOut).gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeInDuration;
            float alpha = Mathf.Lerp(0f, 1f, t);
            Color newColor;
            if (isRoaming)
            {
                newColor = new Color(StartColor_Roaming.r, StartColor_Roaming.g, StartColor_Roaming.b, alpha);
                Img_RoamingFadeInOut.color = newColor;
            }
            else
            {
                newColor = new Color(StartColor.r, StartColor.g, StartColor.b, alpha);
                Img_MainFadeInOut.color = newColor;
            }
            yield return null;
        }
    }

    IEnumerator FadeOut(float fadeOutDuration, bool isRoaming)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeOutDuration;
            float alpha = Mathf.Lerp(1f, 0f, t);
            Color newColor;
            if (isRoaming)
            {
                newColor = new Color(StartColor_Roaming.r, StartColor_Roaming.g, StartColor_Roaming.b, alpha);
                Img_RoamingFadeInOut.color = newColor;
            }
            else
            {
                newColor = new Color(StartColor.r, StartColor.g, StartColor.b, alpha);
                Img_MainFadeInOut.color = newColor;
            }
            yield return null;
        }

        (isRoaming ? Img_RoamingFadeInOut : Img_MainFadeInOut).gameObject.SetActive(false);
    }

    public IEnumerator DoFadeIn(float fadeInDuration, bool isRoaming)
    {
        // �p�G�w�g�b�H�J���A�h������e���H�J�ĪG
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
        }

        fadeInCoroutine = StartCoroutine(FadeIn(fadeInDuration, isRoaming));

        yield return fadeInCoroutine;
    }

    public IEnumerator DoFadeOut(float fadeOutDuration, bool isRoaming)
    {
        // �p�G�w�g�b�H�X���A�h������e���H�X�ĪG
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
        }

        fadeOutCoroutine = StartCoroutine(FadeOut(fadeOutDuration, isRoaming));

        yield return fadeOutCoroutine;
    }

    /// <summary>
    /// ����H�J�M�H�X�ĪG
    /// </summary>
    public void StopDoingFade()
    {
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
            InitImgColor();
        }

        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            InitImgColor();
        }

        Img_RoamingFadeInOut.gameObject.SetActive(false);
    }
}
