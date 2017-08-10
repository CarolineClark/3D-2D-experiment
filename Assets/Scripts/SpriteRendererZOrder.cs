using UnityEngine;
using Anima2D;
using System.Collections;

public class SpriteRendererZOrder : MonoBehaviour
{
    public bool IsStatic;
    public float AnchorOffset;
    private int flipFactor;
    private SpriteRenderer spriteRenderer;
    private SpriteMeshInstance spriteMeshInstance;
 
    void Start()
    {
        flipFactor = -1;
        spriteRenderer = GetComponent<SpriteRenderer>();
        AssignSortOrder();
        EventManager.StartListening(Constants.EVENT_PLAYER_FLIPPED, FlipCameraEventListener);
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
        spriteRenderer.sortingOrder = flipFactor * Mathf.RoundToInt((transform.position.z + AnchorOffset) / 0.05f);
    }

    private void FlipCameraEventListener(Hashtable h) {
        bool flipped = FlippedCameraMessage.GetFlippedFromHashtable(h);
        flipFactor = flipped ? 1: -1;
        AssignSortOrder();
    }
}