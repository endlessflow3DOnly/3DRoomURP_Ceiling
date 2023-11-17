using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TVCyclingManager : MonoBehaviour
{
    [Header("�����q�������Ϥ�")]
    [SerializeField] Image Img_WorldCanvasMain;
    [SerializeField] Image Img_SceneFadeInOut;
    [SerializeField] List<Sprite> Spr_MainPictures;
    [SerializeField, Range(0f, 5f)] float switchInterval;
    int currentWSTVIndex = 0; //�����q��������index
    private IEnumerator CoroutineTVSceneFadeInImg;
    private IEnumerator CoroutineTVSceneFadeOutImg;

    // Start is called before the first frame update
    public void Init()
    {
        //�����q����
        CoroutineTVSceneFadeInImg = FadeImg(Img_SceneFadeInOut, true, 2f);
        CoroutineTVSceneFadeOutImg = FadeImg(Img_SceneFadeInOut, false, 1f);

        //�������q���Ϥ�����
        StartCoroutine(RotateImages());
    }

    /// <summary>
    /// �����~ ���OTV UI��
    /// </summary>
    /// <returns></returns>
    IEnumerator RotateImages()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            if (CoroutineTVSceneFadeInImg != null)
            {
                StopCoroutine(CoroutineTVSceneFadeInImg);
                CoroutineTVSceneFadeInImg = FadeImg(Img_SceneFadeInOut, true, 2f);
            }

            yield return StartCoroutine(CoroutineTVSceneFadeInImg);

            // ��ܷ�e�Ϥ�
            Img_WorldCanvasMain.sprite = Spr_MainPictures[currentWSTVIndex];

            // �W�[���ޥH������U�@�i�Ϥ�
            currentWSTVIndex = (currentWSTVIndex + 1) % Spr_MainPictures.Count;

            if (CoroutineTVSceneFadeOutImg != null)
            {
                StopCoroutine(CoroutineTVSceneFadeOutImg);
                CoroutineTVSceneFadeOutImg = FadeImg(Img_SceneFadeInOut, false, 1f);
            }

            yield return StartCoroutine(CoroutineTVSceneFadeOutImg);
        }
    }

    /// <summary>
    /// �������q���βH�J�H�X
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

        // �T�O�̲׳]�m�ؼ� alpha ��
        Color finalColor = image.color;
        finalColor.a = targetAlpha;
        image.color = finalColor;
    }
}
