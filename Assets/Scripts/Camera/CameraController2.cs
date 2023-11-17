using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController2 : MonoBehaviour
{
    public float moveSpeed = 5.0f; // ���ʳt��
    public float zoomSpeed = 1.0f; // �Ԫ�/�Ի��t��
    public float minZoomDistance = 1.0f; // �̤p�Ԫ�Z��
    public float maxZoomDistance = 10.0f; // �̤j�Ի��Z��
    public Vector2 moveBoundaryMin = new Vector2(-10f, -10f); // ���ʽd�򪺳̤p���
    public Vector2 moveBoundaryMax = new Vector2(10f, 10f); // ���ʽd�򪺳̤j���
    public KeyCode resetKey = KeyCode.R; // ���m�۾���m������

    public Camera thisCamera;

    private Vector3 lastMousePosition;
    private Vector3 offset;
    private float offsetY;

    void Start()
    {
        // �p��۾�����l����
        offset = transform.position;
        offsetY = offset.y;
    }

    void Update()
    {
        // �ˬd�۾��O�_�s�b�äw�ҥ�
        if (thisCamera != null && thisCamera.enabled)
        {
            // �ˬd�ƹ��u���A�ýվ�۾��Z��
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            float zoomAmount = scrollWheel * zoomSpeed;
            float newDistance = Mathf.Clamp(offset.y - zoomAmount, minZoomDistance, maxZoomDistance);
            offset.y = newDistance;

            transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);


            // �ˬd�ƹ�����O�_����A�å��Ʋ��ʬ۾���m
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
                float mouseX = deltaMouse.x * moveSpeed;
                float mouseY = deltaMouse.y * moveSpeed;

                // �p�⥭�Ʋ��ʦV�q�A�îھڷƹ����ʤ�V�i�����������
                Vector3 moveVector = new Vector3(-mouseX, 0, -mouseY);

                // ��s�۾���m
                transform.position += moveVector * Time.deltaTime;

                // ����۾�����V��
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, moveBoundaryMin.x, moveBoundaryMax.x),
                    transform.position.y,
                    Mathf.Clamp(transform.position.z, moveBoundaryMin.y, moveBoundaryMax.y)
                );

                // �O��s���ƹ���m
                lastMousePosition = Input.mousePosition;
            }

            // ���m�۾���m������
            if (Input.GetKeyDown(resetKey))
            {
                // �^���l��m
                ResetCamPosition();
            }
        }
    }

    public void ResetCamPosition()
    {
        // �^���l��m
        transform.position = offset;
        offset.y = offsetY;
    }
}
