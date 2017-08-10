using UnityEngine;
using Anima2D;
using System.Collections;

public class SpriteMeshesZOrder : MonoBehaviour
{
    public bool IsStatic;
    public float AnchorOffset;
 
    private SpriteMeshInstance[] spriteMeshInstances;
    private int[] ordering;
    private int flipFactor = -1;
 
    void Start()
    {
        spriteMeshInstances = GetComponentsInChildren<SpriteMeshInstance>();
        ordering = new int[spriteMeshInstances.Length];
        // read sort order.
        for (int i=0; i<spriteMeshInstances.Length; i++) {
            ordering[i] = spriteMeshInstances[i].sortingOrder;
        }
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
        int sortingOrder = flipFactor * Mathf.RoundToInt((transform.position.z + AnchorOffset) / 0.05f);
        for (int i=0; i<spriteMeshInstances.Length; i++) {
            spriteMeshInstances[i].sortingOrder = ordering[i] + sortingOrder;
        }
    }

    private void FlipCameraEventListener(Hashtable h) {
        bool flipped = FlippedCameraMessage.GetFlippedFromHashtable(h);
        flipFactor = flipped ? 1: -1;
        AssignSortOrder();
    }
}