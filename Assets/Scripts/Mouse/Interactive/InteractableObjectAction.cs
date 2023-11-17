using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectAction : MonoBehaviour
{
    public enum InteractiveObject
    {
        TV,
        EMark_OverLook,
        EMark_FP_BMD,
        EMark_FP_CMD
    }
    public InteractiveObject interactiveObject;

    public enum BMD_InteractiveObjectTypes
    {
        Floor,
        Toilet,
        Shower,
        Sink,
        Window,
    }
    public BMD_InteractiveObjectTypes BMD_interactiveObjectTypes;

    public enum CMD_InteractiveObjectTypes
    {
        Bathroom,
        Balcony
    }
    public CMD_InteractiveObjectTypes CMD_interactiveObjectTypes;

    [SerializeField] CameraManager CameraManager;
    [SerializeField] UIManager UIManager;
    [SerializeField] SceneObjectsManager SceneObjectsManager;

    public TeleportRoomPlace teleportRoomPlace;

    /// <summary>
    /// 點開可互動物件的行為 (直接進入第一人稱)
    /// </summary>
    public void ClickToDoSomething(GameManager GameManager)
    {
        if (interactiveObject == InteractiveObject.TV)
        {
            GameManager.EnablePlayer(false);
            UIManager.EnableUIMain(false);
            GameManager.UI_MiniMapManager.ShowMiniMap(false);

            CameraManager.ToTV_CameraDoPath(UIManager.OpenTV);
        }

        if (interactiveObject == InteractiveObject.EMark_OverLook)
        {
            UIManager.ClickToRoom(teleportRoomPlace);
        }

        if (interactiveObject == InteractiveObject.EMark_FP_BMD)
        {
            UIManager.UI_BuildMaterialManager.OpenBMDUI((int)BMD_interactiveObjectTypes);
        }

        if (interactiveObject == InteractiveObject.EMark_FP_CMD)
        {
            UIManager.UI_ConstructMethodManager.OpenCMDUI((int)CMD_interactiveObjectTypes);
        }
    }
}
