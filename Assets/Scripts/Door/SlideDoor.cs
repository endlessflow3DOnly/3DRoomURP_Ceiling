using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    /*滑門用*/

    [Header("單向門就用right")]
    [SerializeField] Transform DoorRight;
    [SerializeField] Vector3 offset_DoorRight;
    private Vector3 StartPos_RightDoor;
    private Vector3 targetPosRight;

    [SerializeField] Transform DoorLeft;
    [SerializeField] Vector3 offset_DoorLeft;
    private Vector3 StartPos_LeftDoor;
    private Vector3 targetPosLeft;

    [Header("門是否開著")]
    public bool isDoorOpen = false;

    [Header("是否是單向門")]
    public bool isSingleSlideDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isSingleSlideDoor == false)
        {
            StartPos_RightDoor = DoorRight.position;
            StartPos_LeftDoor = DoorLeft.position;
        }
        else
        {
            StartPos_RightDoor = DoorRight.position;
        }
    }

    public void DoOpenDoor()
    {
        if (isSingleSlideDoor == false)
        {
            targetPosRight = DoorRight.position + offset_DoorRight;
            targetPosLeft = DoorLeft.position + offset_DoorLeft;

            DoorRight.DOMove(targetPosRight, 2f);
            DoorLeft.DOMove(targetPosLeft, 2f);
        }
        else
        {
            targetPosRight = DoorRight.position + offset_DoorRight;
            DoorRight.DOMove(targetPosRight, 2f);
        }
    }


    public void DoCloseDoor()
    {
        if (isSingleSlideDoor == false)
        {
            DoorRight.DOMove(StartPos_RightDoor, 2f);
            DoorLeft.DOMove(StartPos_LeftDoor, 2f);
        }
        else
        {
            DoorRight.DOMove(StartPos_RightDoor, 2f);
        }
    }
}
