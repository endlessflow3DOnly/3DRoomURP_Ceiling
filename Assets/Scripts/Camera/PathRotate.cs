using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���۾���۸��|�I���ʮɥ��`����A���ẩ�C�Ҧ���������|�Ϊ���
public class PathRotate : MonoBehaviour
{
    public Vector3 Start_point;  //�_�l�I
    public bool finish = false;  //��F���I
    readonly int FPS = 100;  //�V�v

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
            yield return new WaitForSeconds((float)1 / FPS);  //���� 1 / FPS ��

            Vector3 direc = transform.position - Start_point;  //��X��첾��V

            //���ܪ��騤��
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(direc.x, direc.z) * Mathf.Rad2Deg, 0);

            Start_point = transform.position;
        }
    }
}
