using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMeshColliderManager : MonoBehaviour
{
    private void Start()
    {
       StartCoroutine(AddMeshCollidersRecursively(transform));
    }

    private IEnumerator AddMeshCollidersRecursively(Transform parent)
    {
        // 获取父物体及其所有子物体的MeshRenderer组件
        MeshRenderer[] meshRenderers = parent.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (renderer.GetComponent<MeshCollider>() == null)
            {
                // 为每个子物体添加MeshCollider
                MeshCollider meshCollider = renderer.gameObject.AddComponent<MeshCollider>();
                meshCollider.convex = true;
                meshCollider.isTrigger = true;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}
