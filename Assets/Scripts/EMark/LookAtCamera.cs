using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LookAtCamera : MonoBehaviour
{
    [Header("是否要往上看")]
    [SerializeField] bool forOverLook;

    [SerializeField] GameManager GameManager;
    [SerializeField] Camera overlookCam;
    [SerializeField] GameObject EarkGameObject;

    [SerializeField] float rotateX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(forOverLook)
        {
            EarkGameObject.transform.rotation = Quaternion.Euler(new Vector3(rotateX, 0, 0));
            //EarkGameObject.transform.LookAt(overlookCam.transform);
        }
        else 
        {
            EarkGameObject.transform.rotation = Quaternion.identity;
        }
    }
}
