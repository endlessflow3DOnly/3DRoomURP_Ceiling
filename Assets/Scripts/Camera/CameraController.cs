using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // ���骺 transform
    public float rotationSpeed = 1.0f; // ����t��
    public float zoomSpeed = 1.0f; // �Ԫ�/�Ի��t��
    public float minZoomDistance = 1.0f; // �̤p�Ԫ�Z��
    public float maxZoomDistance = 10.0f; // �̤j�Ի��Z��

    public float minPositionY = 0.5f; // �۾��̧C�I����
    public float maxPositionY = 9.95f; // �۾��̰��I����

    public Camera thisCamera;

    private Vector3 offset;
    private Vector3 lastMousePosition;

    [Header("����̤j����t��")]
    public float MaxRotationSpeedX = 1.0f;
    public float MaxRotationSpeedY = 1.0f;

    void Start()
    {
        // �p��۾�����l����
        offset = transform.position - target.position;
    }

    void Update()
    {
        // �ˬd�۾��O�_�s�b�äw�ҥ�
        if (thisCamera != null && thisCamera.enabled)
        {
            // ����z���{���X
            // �ˬd�ƹ��u���A�ýվ�۾��Z��
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            float zoomAmount = scrollWheel * zoomSpeed;
            float newDistance = Mathf.Clamp(offset.magnitude - zoomAmount, minZoomDistance, maxZoomDistance);
            offset = offset.normalized * newDistance;

            // �ˬd�ƹ�����O�_����A�ñ���۾�����
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
                float mouseX = deltaMouse.x * rotationSpeed;
                float mouseY = deltaMouse.y * rotationSpeed;

                //����̤j����t��
                if(mouseX > MaxRotationSpeedX)
                {
                    mouseX = MaxRotationSpeedX;
                }
                else if (mouseX < -MaxRotationSpeedX)
                {
                    mouseX = -MaxRotationSpeedX;
                }
                
                if (mouseY > MaxRotationSpeedY)
                {
                    mouseY = MaxRotationSpeedY;
                }
                else if(mouseY < -MaxRotationSpeedY)
                {
                    mouseY = -MaxRotationSpeedY;
                }

                // ����۾�
                transform.RotateAround(target.position, Vector3.up, mouseX);

                //�T�O Y �b��m���p��s
                if(transform.position.y >= minPositionY)
                {
                    if(transform.position.y <= maxPositionY)
                    {
                        transform.RotateAround(target.position, transform.right, -mouseY); // �ϥέt������W�U�ư�
                    }
                    else
                    {
                        if (-mouseY < 0)
                        {
                            transform.RotateAround(target.position, transform.right, -mouseY);
                        }
                        else
                        {
                            transform.RotateAround(target.position, transform.right, 0);
                        }
                    }

                }
                else
                {
                    if(-mouseY > 0)
                    {
                        transform.RotateAround(target.position, transform.right, -mouseY);
                    }
                    else
                    {
                        transform.RotateAround(target.position, transform.right, 0);
                    }
                    
                }
                
                // �վ�۾���m
                offset = transform.position - target.position;
            }

            // ��s�۾���m
            transform.position = target.position + offset;

            lastMousePosition = Input.mousePosition;
        }
    }
}
