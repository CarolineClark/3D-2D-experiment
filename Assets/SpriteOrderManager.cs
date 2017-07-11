using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOrderManager : MonoBehaviour {
	private SpriteRenderer[] spriteLayers;
    private int[] spriteLayersSorting;

	void Start () {
		spriteLayers = FindObjectsOfType<SpriteRenderer>();
		spriteLayersSorting = new int[spriteLayers.Length];
		for (int i = 0; i < spriteLayers.Length; i++) {
			spriteLayersSorting[i] = spriteLayers[i].sortingOrder;
		}
	}
	void Update(){
		for (int i = 0; i < spriteLayers.Length; i++) {
			spriteLayers [i].sortingOrder = spriteLayersSorting [i] + ((int)-transform.position.z);
		}
	}
}
