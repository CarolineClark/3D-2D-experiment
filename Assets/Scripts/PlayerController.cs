using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	CharacterController controller;
	private float speed = 8f;
	private float jumpSpeed = 10f;
	private float gravity = 9.8f;
	public Animator animator;
	private bool flipped = false;
	private Vector3 moveDirection = Vector3.zero;

	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	void FixedUpdate () {
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
            
        }
		float horizontal = moveDirection.x;
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
		if (horizontal > 0 && !flipped) {
			FlipSprite();
		} else if (horizontal < 0 && flipped) {
			FlipSprite();
		}
	}

	void Update() {
		animator.SetFloat("speed", controller.velocity.magnitude);
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
