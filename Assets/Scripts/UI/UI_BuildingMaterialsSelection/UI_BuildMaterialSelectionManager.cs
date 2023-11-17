using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BuildMaterialSelectionManager : MonoBehaviour
{
    //������Ϊ�
    //�ثe�O�Ѫ�O3 ���4 �a�O4

    [Header("�a�O��Mesh�M����")]
    [SerializeField] List<Material> FloorMaterials;
    [SerializeField] List<MeshRenderer> FloorMeshRenders;

    [Header("�����Mesh�M����")]
    [SerializeField] List<Material> WallMaterials;
    [SerializeField] List<MeshRenderer> WallMeshRenders;

    [Header("�Ѫ�O��Mesh�M����")]
    [SerializeField] List<Material> CeilingMaterials;
    [SerializeField] List<MeshRenderer> CeilingMeshRenders;

    #region �a�O
    public void ChangeFloorMat01(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in FloorMeshRenders)
        {
            meshRenderer.material = FloorMaterials[0];
        }

    }

    public void ChangeFloorMat02(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in FloorMeshRenders)
        {
            meshRenderer.material = FloorMaterials[1];
        }

    }

    public void ChangeFloorMat03(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in FloorMeshRenders)
        {
            meshRenderer.material = FloorMaterials[2];
        }

    }

    public void ChangeFloorMat04(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in FloorMeshRenders)
        {
            meshRenderer.material = FloorMaterials[3];
        }
    }
    #endregion

    #region ���
    public void ChangeWallMat01(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in WallMeshRenders)
        {
            meshRenderer.material = WallMaterials[0];
        }
    }

    public void ChangeWallMat02(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in WallMeshRenders)
        {
            meshRenderer.material = WallMaterials[1];
        }
    }

    public void ChangeWallMat03(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in WallMeshRenders)
        {
            meshRenderer.material = WallMaterials[2];
        }
    }

    public void ChangeWallMat04(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in WallMeshRenders)
        {
            meshRenderer.material = WallMaterials[3];
        }
    }
    #endregion

    #region �Ѫ�O
    public void ChangeCeilingMat01(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in CeilingMeshRenders)
        {
            meshRenderer.material = CeilingMaterials[0];
        }
    }

    public void ChangeCeilingMat02(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in CeilingMeshRenders)
        {
            meshRenderer.material = CeilingMaterials[1];
        }
    }

    public void ChangeCeilingMat03(bool isOn)
    {
        foreach (MeshRenderer meshRenderer in CeilingMeshRenders)
        {
            meshRenderer.material = CeilingMaterials[2];
        }
    }
    #endregion
}
