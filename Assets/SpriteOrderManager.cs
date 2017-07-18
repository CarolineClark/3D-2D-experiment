using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class SpriteOrderManager : MonoBehaviour {
	private SpriteRenderer[] spriteLayers;
	private SpriteMeshInstance[] spriteMeshes;
    private int[] spriteSorting;

	void Start () {
		spriteLayers = FindObjectsOfType<SpriteRenderer>();
		spriteMeshes = FindObjectsOfType<SpriteMeshInstance>();
		int spriteLayersLength = spriteLayers.Length;
		int spriteMeshesLength = spriteMeshes.Length;
		spriteSorting = new int[spriteLayers.Length + spriteMeshes.Length];
		for (int i = 0; i < spriteLayersLength; i++) {
			spriteSorting[i] = spriteLayers[i].sortingOrder;
		}
		for (int j = spriteLayersLength; j < spriteLayersLength + spriteMeshesLength; j++) {
			spriteSorting[j] = spriteMeshes[j - spriteLayersLength].sortingOrder;
		}
	}
	void Update(){
		for (int i = 0; i < spriteLayers.Length; i++) {
			spriteLayers[i].sortingOrder = spriteSorting[i] + ((int)-transform.position.z);
		}
		for (int j = spriteLayers.Length; j < spriteLayers.Length + spriteMeshes.Length; j++) {
			spriteMeshes[j - spriteLayers.Length].sortingOrder = spriteSorting[j] + ((int)-transform.position.z);
		}
		Debug.Log(spriteMeshes);
	}
}
