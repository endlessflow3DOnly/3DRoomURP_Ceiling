using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

//玩家移動
public class PlayerController : MonoBehaviour
{
    public bool playerControlEnabled = true;

    [SerializeField] GameManager GameManager;
    TeleportManager TeleportManager;

    [Header("參考物件")]
    CharacterController CharacterController;
    NavMeshAgent NavMeshAgent;
    private bool useCharacterController = true; // 預設使用 Character Controller

    [Header("玩家參數")]
    [SerializeField] float speed = 5f;
    [SerializeField] float gravity = -15f;
    Vector3 velocity;

    [SerializeField] List<Transform> targetPositions; // 在 Inspector 中指定四個目標位置的 Transform，分別代表按下1、2、3、4鍵時的目標位置
 
    [Header("抓取最近導航點")]
    public Camera FPCamera;
    [SerializeField] FPMouseRotateCamera FPMouseRotateCamera;
    public GameObject effectPrefab1; // 在 Inspector 中指定效果1的預置物
    public GameObject effectPrefab2; // 在 Inspector 中指定效果2的預置物
    public LayerMask raycastLayers; // 指定要對哪些層級進行 Raycast

    private GameObject effectInstance1; // 效果1的實例
    private GameObject effectInstance2; // 效果2的實例

    [Header("按下右鍵定位座標")]
    [SerializeField] float requiredHoldTime = 2.0f;
    [SerializeField] Vector3 navPoint;
    [SerializeField] GameObject effectPrefab3;//標註的特效物件
    private GameObject effectInstance3; // 效果1的實例
    private bool isMouseHeld = false;
    private float holdStartTime;

    [Header("導航到定位座標")]
    [SerializeField] Transform targetTransform;
    public bool isAgentMoving = false;

    public void InitPlayer( )
    {
        TeleportManager = GameManager.TeleportManager;

        CharacterController = GetComponent<CharacterController>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        targetPositions = new List<Transform>();

        if(TeleportManager.TeleportTransforms != null)
        {
            for (int i = 0; i < TeleportManager.TeleportTransforms.Length; i++)
            {
                targetPositions.Add(TeleportManager.TeleportTransforms[i]);
            }
        }

    }

    void Update()
    {
        if (!playerControlEnabled)
            return;

        if (GameManager.IsAllFinishInit == false)
            return;

        if (NavMeshAgent != null && NavMeshAgent.enabled == true)
        {
            if (NavMeshAgent.pathPending)
            {
            }
            else if (NavMeshAgent.remainingDistance > 0.1f)
            {
                isAgentMoving = true;
            }
            else
            {
                isAgentMoving = false;
            }
        }

        // Check for WASD input to switch to Character Controller
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            SwitchToCharacterController();
        }

        if (useCharacterController)
        {
            // Character Controller's movement logic
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;
            CharacterController.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            CharacterController.Move(velocity * Time.deltaTime);
        }

        if (GameManager.CameraManager.FPCamera.enabled == true)
        {
            HandleMouseInput();
        }
    }

    void HandleMouseInput()
    {
        Ray ray = FPCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, raycastLayers))
        { 
            HandleEffect1(hit.point);
            HandleEffect2(hit.point);
        }
        else
        {
            DestroyEffectInstances();
        }

        if (Input.GetMouseButtonDown(0) && !UIDetector.instance.IsPointerOverUIElement())
        {
            if (GameManager.SettingManager.isHoverHighlightObj == false)
            {
                HandleLeftMouseClick();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isMouseHeld = false;
            holdStartTime = 0f;
        }
    }

    void HandleEffect1(Vector3 hitPoint)
    {
        if (effectInstance1 == null)
        {
            effectInstance1 = Instantiate(effectPrefab1, hitPoint, Quaternion.identity);
        }
        else
        {
            effectInstance1.transform.position = hitPoint;
        }

        HandleEffect2(hitPoint);
    }

    void HandleEffect2(Vector3 hitPoint)
    {
        if (NavMesh.SamplePosition(hitPoint, out NavMeshHit navMeshHit, 100f, NavMesh.AllAreas))
        {
            Vector3 closestNavMeshPoint = navMeshHit.position;
            navPoint = closestNavMeshPoint;

            if (effectInstance2 == null)
            {
                effectInstance2 = Instantiate(effectPrefab2, closestNavMeshPoint, Quaternion.identity);
            }
            else
            {
                effectInstance2.transform.position = closestNavMeshPoint;
            }
        }
        else
        {
            DestroyEffect2();
        }
    }

    void DestroyEffectInstances()
    {
        if (effectInstance1 != null)
        {
            Destroy(effectInstance1);
        }
        DestroyEffect2();
    }

    void DestroyEffect2()
    {
        if (effectInstance2 != null)
        {
            Destroy(effectInstance2);
        }
    }

    void HandleLeftMouseClick()
    {
        DestroyEffectInstances();

        effectInstance3 = Instantiate(effectPrefab3, navPoint, Quaternion.identity);
        Destroy(effectInstance3, 1f);

        isMouseHeld = true;
        holdStartTime = Time.time;

        SwitchToNavMeshAgent();
        targetTransform.position = navPoint;
        NavMeshAgent.SetDestination(targetTransform.position);
    }

    /// <summary>
    /// 開關玩家控制
    /// </summary>
    public void EnablePlayerControl(bool enable)
    {
        if (enable)
        {
            playerControlEnabled = true;
        }
        else
        {
            playerControlEnabled = false;
            Destroy(effectInstance1);
            Destroy(effectInstance2);
            Destroy(effectInstance3);
        }
    }

    /// <summary>
    /// 切換為 Character Controller
    /// </summary>
    public void SwitchToCharacterController()
    {
        useCharacterController = true;
        CharacterController.enabled = true;
        NavMeshAgent.enabled = false;
        isAgentMoving = false;
    }

    /// <summary>
    /// 切換為 NavMesh Agent
    /// </summary>
    public void SwitchToNavMeshAgent()
    {
        useCharacterController = false;
        CharacterController.enabled = false;
        NavMeshAgent.enabled = true;
    }

    #region 小地圖按鈕 NavMesh 移動
    public void ClickToMove(int index)
    {
        NavMeshAgent.updateRotation = true;

        SwitchToNavMeshAgent();
        NavMeshAgent.SetDestination(targetPositions[index].position);
    }

    /// <summary>
    /// 點小地圖驚嘆號，場景搜尋ExclamationMarkPath看Path位置決定Path[index]
    /// </summary>
    /// <param name="index"></param>
    public void MoveBtnExclamationMark(TeleportRoomPlace teleportRoomPlace, GameManager GameManager)
    {
        SwitchToCharacterController();

        TeleportManager = GameManager.TeleportManager;

        TeleportManager.SwitchToRoom(teleportRoomPlace);
    }
    #endregion
}
