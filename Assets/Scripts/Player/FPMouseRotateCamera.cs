using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

//玩家相機拖曳移動
public class FPMouseRotateCamera : MonoBehaviour
{
    [SerializeField] Transform PlayerObject;
    [SerializeField] PlayerController playerController;
    [SerializeField] NavMeshAgent NavMeshAgent;

    [SerializeField] float mouseXSensitivity = 100f;
    [SerializeField] float mouseYSensitivity = 100f;

    private bool isDragging = false;

    float mouseX;
    float mouseY;
    [SerializeField] float startXRotation;
    [SerializeField] float startYRotation;
    [SerializeField] Quaternion rotation;

    [Header("光標隱藏")]
    public bool CursorLock = false;

    [Header("拖曳方向反轉")]
    public bool DraggingDirectionSwitch = false;

    void Start()
    {
        if (CursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void OnInitialized()
    {
        startXRotation = gameObject.transform.eulerAngles.x;
        startYRotation = gameObject.transform.eulerAngles.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            NavMeshAgent.updateRotation = false;

            startYRotation = gameObject.transform.eulerAngles.y;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseXSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseYSensitivity * Time.deltaTime;

            if (DraggingDirectionSwitch)
            {
                startXRotation -= mouseY;
                startXRotation = Mathf.Clamp(startXRotation, -90f, 90f);
                transform.localRotation = Quaternion.Euler(startXRotation, 0f, 0f);
                startYRotation += mouseX;
                PlayerObject.localRotation = Quaternion.Euler(0f, startYRotation, 0f);
            }
            else
            {
                startXRotation += mouseY;
                startXRotation = Mathf.Clamp(startXRotation, -90f, 90f);
                transform.localRotation = Quaternion.Euler(startXRotation, 0f, 0f);
                startYRotation -= mouseX;
                PlayerObject.localRotation = Quaternion.Euler(0f, startYRotation, 0f);
            }

            rotation = PlayerObject.localRotation;
        }
    }

    public void ResetRotationY(float yValue)
    {
        startYRotation = yValue;
    }

    public void ResetRotationX(float xValue)
    {
        startXRotation = xValue;
    }
}