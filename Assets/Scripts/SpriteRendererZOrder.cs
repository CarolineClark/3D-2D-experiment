using UnityEngine;
using Anima2D;

public class SpriteRendererZOrder : MonoBehaviour
{
    public bool IsStatic;
    public float AnchorOffset;
 
    private SpriteRenderer spriteRenderer;
    private SpriteMeshInstance spriteMeshInstance;
 
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        spriteRenderer.sortingOrder = -Mathf.RoundToInt((transform.position.z + AnchorOffset) / 0.05f);
    }
}