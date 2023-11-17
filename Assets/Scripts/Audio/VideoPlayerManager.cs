using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEditor;

public class VideoPlayerManager : MonoBehaviour
{
    /* 負責管理所有要播放影片的Manager */

    [SerializeField] UIManager UIManager;

    [Header("VideoPlayer")]
    [SerializeField] RenderTexture RenderTex_Video;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] AudioClip audioClip;
    [ReadOnlyInspector]
    [SerializeField] float videoLengthInSeconds;

    [Header("開頭動畫用")]
    [SerializeField] GameObject UI_Opening;
    [SerializeField] RawImage RawImg_Opening;
    [SerializeField] Image Img_BlackBackground;
    [SerializeField, Range(0.0f, 3f)] float FadeInDuration;
    [SerializeField, Range(0.0f, 3f)] float FadeOutDuration;
    [SerializeField] bool StartPlayLogo = false;
    [SerializeField] bool loopPlay = false;
    [SerializeField] bool CanSkipVideo = false;
    bool isPlaying = false;

    [Header("TV關機動畫")]
    [SerializeField] List<VideoClip> TV_ClosingClip;

    [Header("建材展示")]
    [SerializeField] List<VideoClip> Opening_videoClips;

    [Header("工法展示")]
    [SerializeField] List<VideoClip> CMD_videoClips;
    [SerializeField] RawImage ConstructMethodImg;

    //工法展示
    IEnumerator OpeningVideoIEnum;
    IEnumerator ConstructMethodVideoIEnum;
    int CMDVideoIndex;
    bool isLoop;

    public void Init()
    {
        ResetVideoPlayerSetting();
        OpeningVideoIEnum = PlayOpeningVideo();

#if UNITY_EDITOR
        StartPlayLogo = false;
#endif
        if (StartPlayLogo)
        {
            StartCoroutine(OpeningVideoIEnum);
        }
        else
        {
            UI_Opening.SetActive(false);
        }

        ConstructMethodVideoIEnum = PlayConstructMethodVideo();
    }

    private void Update()
    {
        //if (loopPlay && !isPlaying)
        //{
        //    StartCoroutine(PlayOpeningVideo());
        //}
    }

    #region 開頭動畫
    /// <summary>
    /// 播放Logo開頭動畫
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayOpeningVideo()
    {
        UI_Opening.SetActive(true);

        if (videoPlayer != null && videoPlayer.targetTexture == null)
        {
            videoPlayer.targetTexture = RenderTex_Video;
        }

        isPlaying = true;
        RawImg_Opening.gameObject.SetActive(true);

        videoPlayer.clip = Opening_videoClips[0];

        // 計算影片長度並添加音訊
        videoLengthInSeconds = (float)videoPlayer.length;
        AudioManager.instance.AddAudioObject(audioClip, videoLengthInSeconds, true, false);

        videoPlayer.Play();

        yield return new WaitForSeconds(videoLengthInSeconds);

        RawImg_Opening.DOFade(0, 1f);

        yield return FadeInOutManager.instance.DoFadeIn(FadeInDuration, false);

        Img_BlackBackground.gameObject.SetActive(false);

        // 淡出效果
        yield return FadeInOutManager.instance.DoFadeOut(FadeOutDuration, false);
        isPlaying = false;

        UI_Opening.SetActive(false);

        ResetVideoPlayerSetting();
    }
    #endregion

    #region TV
    public void PlayTVCloseAnim()
    {
        videoPlayer.clip = TV_ClosingClip[0];
        videoPlayer.Play();
    }
    #endregion

    #region 工法展示
    /// <summary>
    /// 開啟工法展示影片
    /// </summary>
    /// <param name="enable"></param>
    /// <param name="index"></param>
    /// <param name="isloop"></param>
    public void EnableConstructMethodVideo(bool enable, int index, bool isloop)
    {
        ResetVideoPlayerSetting();

        if (videoPlayer != null && videoPlayer.targetTexture == null)
        {
            videoPlayer.targetTexture = RenderTex_Video;
        }

        if (enable)
        {
            CMDVideoIndex = index;
            isLoop = isloop;

            StartCoroutine(PlayConstructMethodVideo());
        }
        else
        {
            StopCoroutine(PlayConstructMethodVideo());
        }
    }

    /// <summary>
    /// 建材工法影片暫停按鈕
    /// </summary>
    /// <param name="isOn"></param>
    public void ToggleVideoPlay(bool isOn)
    {
        //On是暫停, 沒on 是播放
        if (isOn)
        {
            videoPlayer.Pause();
            UIManager.UI_ConstructMethodManager.Text_PauseOrPlay.text = "PLAY";
        }
        else
        {
            videoPlayer.Play();
            UIManager.UI_ConstructMethodManager.Text_PauseOrPlay.text = "STOP";
        }
    }

    /// <summary>
    /// 重置工法展示影片設定
    /// </summary>
    public void CMD_ResetTogglePauseOrPlayState()
    {
        UIManager.UI_ConstructMethodManager.Toggle_PauseOrPlay.isOn = false;
        ToggleVideoPlay(false);
    }

    IEnumerator PlayConstructMethodVideo()
    {
        if (ConstructMethodImg != null)
        {
            videoPlayer.clip = CMD_videoClips[CMDVideoIndex];

            videoPlayer.isLooping = isLoop;
            videoPlayer.Play();
        }

        yield return null;
    }
    #endregion

    void ResetVideoPlayerSetting()
    {
        CMD_ResetTogglePauseOrPlayState();

        if (OpeningVideoIEnum != null)
            StopCoroutine(OpeningVideoIEnum);

        if (PlayConstructMethodVideo() != null)
            StopCoroutine(PlayConstructMethodVideo());

        videoPlayer.targetTexture = RenderTex_Video;
        videoPlayer.targetTexture.Release();

        videoPlayer.Stop();
        videoPlayer.clip = null;
        videoPlayer.isLooping = false;
    }
}
