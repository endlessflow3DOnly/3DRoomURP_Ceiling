using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOutlineSelectionManager : MonoBehaviour
{
    [SerializeField] GameManager GameManager;

    [Header("OutLine判定用的相機")]
    [ReadOnlyInspector] public Camera OutlineClickCamera;

    [SerializeField] LayerMask raycastLayers;

    [SerializeField] Color highlightColor;
    [SerializeField] Color selectColor;
    [SerializeField, Range(0f, 10f)] float outlinewidth;

    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [Header("所有可互動物件 (驚嘆號和家具)")]
    [SerializeField] List<Outline> OulineObjects;

    [Header("所有可互動物件 (家具)")]
    [SerializeField] List<Outline> OulineFurnitureObjects;

    bool isLoadFinished = false;
    Ray ray;

    public void Init()
    {
        OutlineClickCamera = GameManager.CameraManager.FPCamera;

        StartCoroutine(InitOutLineObjects());
        Debug.Log("<color=blue>" + this.name + " 初始化完成</color>");
    }

    IEnumerator InitOutLineObjects()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0;i < OulineObjects.Count;i++) 
        {
            OulineObjects[i].enabled = true;
            OulineObjects[i].OutlineWidth = 0;
            OulineObjects[i].tag = "SelectableObject";
        }

        GameManager.SceneObjectsManager.CloseAllEMark();

        isLoadFinished = true;
    }

    public void OpenOutLineObjects(bool isShow)
    {
        for (int i = 0; i < OulineObjects.Count; i++)
        {
            OulineObjects[i].enabled = isShow;
        }
    }

    void Update()
    {
        if (!isLoadFinished)
        {
            return;
        }

        ClearHighlight();

        if (OutlineClickCamera != null && OutlineClickCamera.enabled == true)
        {
            ray = OutlineClickCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit, float.MaxValue, raycastLayers))
            {
                highlight = raycastHit.transform;

                if (ShouldHighlightObject())
                {
                    GameManager.SettingManager.isHoverHighlightObj = true;
                    HandleHighlightObject();
                }
                else
                {
                    GameManager.SettingManager.isHoverHighlightObj = false;
                }
            }
        }
        else
        {
            GameManager.SettingManager.isHoverHighlightObj = false;
        }
    }

    bool ShouldHighlightObject()
    {
        return highlight.CompareTag("SelectableObject") && highlight != selection;
    }

    void HandleHighlightObject()
    {
        Outline outline = highlight.gameObject.GetComponent<Outline>();

        if (outline != null)
        {
            outline.OutlineWidth = outlinewidth;
            outline.enabled = true;
        }
        else
        {
            outline = highlight.gameObject.AddComponent<Outline>();
            outline.enabled = true;
            outline.OutlineColor = highlightColor;
            outline.OutlineWidth = outlinewidth;
        }

        if (Input.GetMouseButtonDown(0) && GameManager.SettingManager.isHoverHighlightObj)
        {
            HandleSelection();
        }
    }

    void ClearHighlight()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            GameManager.SettingManager.isHoverHighlightObj = false;
            highlight = null;
        }
    }

    void HandleSelection()
    {
        if (highlight)
        {
            ClearSelectionOutline();

            selection = raycastHit.transform;
            Outline selectionOutline = selection.gameObject.GetComponent<Outline>();
            selectionOutline.enabled = true;
            selectionOutline.OutlineColor = selectColor;
            selectionOutline.OutlineWidth = outlinewidth;

            EnterObjectAction();
        }
        else
        {
            if (selection)
            {
                ClearSelectionOutline();
                selection = null;
            }
        }
    }

    void ClearSelectionOutline()
    {
        if (selection != null)
        {
            selection.gameObject.GetComponent<Outline>().enabled = false;
        }
    }


    void EnterObjectAction()
    {
        selection.gameObject.GetComponent<InteractableObjectAction>().ClickToDoSomething(GameManager);
        DoMouseReset();
    }

    void DoMouseReset()
    {
        if(selection != null)
        {
            selection.gameObject.GetComponent<Outline>().enabled = false;
        }

        if(highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
        }

        selection = null;
        highlight = null;
    }
}
