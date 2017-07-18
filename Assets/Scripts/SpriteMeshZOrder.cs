using UnityEngine;
using Anima2D;

public class SpriteMeshZOrder : MonoBehaviour
{
    public bool IsStatic;
    public float AnchorOffset;
 
    private SpriteMeshInstance spriteMeshInstance;
 
    void Start()
    {
        spriteMeshInstance = GetComponent<SpriteMeshInstance>();
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
        spriteMeshInstance.sortingOrder = -Mathf.RoundToInt((transform.position.z + AnchorOffset) / 0.05f);
    }
}