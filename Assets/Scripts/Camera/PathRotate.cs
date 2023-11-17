using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//讓相機跟著路徑點移動時正常旋轉，之後漫遊模式有旋轉路徑用的到
public class PathRotate : MonoBehaviour
{
    public Vector3 Start_point;  //起始點
    public bool finish = false;  //抵達終點
    readonly int FPS = 100;  //幀率

    void Start()
    {
        StartCoroutine(RotateObject());
    }

    private void OnEnable()
    {
        StartCoroutine(RotateObject());
    }

    IEnumerator RotateObject()
    {
        Start_point = transform.position;

        while (!finish)
        {
            yield return new WaitForSeconds((float)1 / FPS);  //停止 1 / FPS 秒

            Vector3 direc = transform.position - Start_point;  //算出其位移方向

            //改變物體角度
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(direc.x, direc.z) * Mathf.Rad2Deg, 0);

            Start_point = transform.position;
        }
    }
}
