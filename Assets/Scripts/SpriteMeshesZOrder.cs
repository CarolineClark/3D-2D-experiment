using UnityEngine;
using Anima2D;

public class SpriteMeshesZOrder : MonoBehaviour
{
    public bool IsStatic;
    public float AnchorOffset;
 
    private SpriteMeshInstance[] spriteMeshInstances;
    private int[] ordering;
 
    void Start()
    {
        
        spriteMeshInstances = GetComponentsInChildren<SpriteMeshInstance>();
        ordering = new int[spriteMeshInstances.Length];
        // read sort order.
        for (int i=0; i<spriteMeshInstances.Length; i++) {
            ordering[i] = spriteMeshInstances[i].sortingOrder;
        }
        AssignSortOrder();
    }
 
    void Update()
    {
        if (!IsStatic)
        {
            AssignSortOrder();
        }
    }
 
    private void AssignSortOrder() 
    {
        int sortingOrder = -Mathf.RoundToInt((transform.position.z + AnchorOffset) / 0.05f);
        for (int i=0; i<spriteMeshInstances.Length; i++) {
            spriteMeshInstances[i].sortingOrder = ordering[i] + sortingOrder;
        }
    }
}