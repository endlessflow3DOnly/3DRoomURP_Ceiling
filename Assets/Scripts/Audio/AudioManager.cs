using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public GameObject AudioParentObj;

    private void Awake()
    {
        instance = this;
    }

    public void AddAudioObject(AudioClip audioClip, float aliveTime ,bool IsPlayAwake, bool isLoop)
    {
        GameObject audioObj = new GameObject();
        audioObj.name = audioClip.name;
        audioObj.transform.SetParent(AudioParentObj.transform);

        AudioSource objAduioSource = audioObj.AddComponent<AudioSource>();
        audioObj.GetComponent<AudioSource>().clip = audioClip;

        if (IsPlayAwake) 
        {
            objAduioSource.Play();
        }

        objAduioSource.loop = isLoop;

        StartCoroutine(DestroyAudioObj(aliveTime, audioObj));
    }

    IEnumerator DestroyAudioObj(float time, GameObject audioObj)
    {
        yield return new WaitForSeconds(time);

        Destroy(audioObj);
    }
}
