using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float Speed;
	public float walkSpeed = 40;
	public float runSpeed = 60f;


	float horizontalMove = 0f;
	public bool jump = false;
	//bool crouch = false;

	// Use this for initialization
	void Start () {

		controller = GetComponent<CharacterController2D>();
		animator = GetComponent<Animator>();
		Speed = walkSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;
		animator.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

		if(Input.GetButton("Run")) {

			animator.SetBool("Run", true);
			Speed = runSpeed;
		} 
		else if (Input.GetButtonUp("Run")) {

			animator.SetBool("Run", false);
			Speed = walkSpeed;
		}

		//GetSword
		if (Input.GetButtonDown("GetSword")) {

			animator.SetBool("Swording", !animator.GetBool("Swording"));
		}


		if (Input.GetButtonDown("Jump")) {

			jump = true;
		}

	}

	public void OnDie() {
		animator.SetTrigger("Die");
	}


	public void Life() {
		animator.SetTrigger("IsALife");
	}

	private void FixedUpdate() {


		controller.Move(horizontalMove * Time.fixedDeltaTime , false, jump);

		jump = false;

	}
}
