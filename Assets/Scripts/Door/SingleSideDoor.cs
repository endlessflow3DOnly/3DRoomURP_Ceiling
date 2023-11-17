using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleSideDoor : MonoBehaviour
{
    [Header("開關門角度")]
    [SerializeField] float OpenYRot = 0f;
    [SerializeField] float CloseYRot = 0f;

    public bool isDoorOpen = false;

    public void DoOpenDoor()
    {
        transform.DORotate(new Vector3(0, OpenYRot, 0), 1f, RotateMode.Fast);
    }

    public void DoCloseDoor()
    {
        transform.DORotate(new Vector3(0, CloseYRot, 0), 1f, RotateMode.Fast);
    }
}
