using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	Rigidbody rb;
	private float runningForce = 0.1f;
	public Animator animator;
	private bool flipped = false;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		float horizontal = Input.GetAxisRaw("Horizontal") * runningForce;
		float vertical = Input.GetAxisRaw("Vertical") * runningForce;
		rb.AddForce(new Vector3(horizontal, 0, vertical), ForceMode.Impulse);
		if (horizontal > 0 && !flipped) {
			FlipSprite();
		} else if (horizontal < 0 && flipped) {
			FlipSprite();
		}
	}

	void Update() {
		animator.SetFloat("speed", rb.velocity.magnitude);
	}

	void FlipSprite() {
		if (flipped) {
			animator.transform.localRotation = Quaternion.Euler(0, 0, 0);
		} else {
			animator.transform.localRotation = Quaternion.Euler(0, 180, 0);
		}
		
		flipped = !flipped;
	}
}
