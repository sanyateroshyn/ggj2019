using CnControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public GameObject AttakBTN;

	public float Speed;
	public float walkSpeed = 40;
	public float runSpeed = 60f;


	float horizontalMove = 0f;
	public bool jump = false;
	//bool crouch = false;

	bool ifCanMoove = true;
	bool forAttack = true;


	// Use this for initialization
	void Start () {
		AttakBTN.SetActive(false);

		controller = GetComponent<CharacterController2D>();
		animator = GetComponent<Animator>();
		Speed = walkSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (ifCanMoove) {
			horizontalMove = CnInputManager.GetAxisRaw("Horizontal") * Speed;
			animator.SetFloat("Speed", Mathf.Abs(CnInputManager.GetAxisRaw("Horizontal")));
		}

		if(CnInputManager.GetButton("Run") && controller.m_Grounded) {

			animator.SetBool("Run", true);
			Speed = runSpeed;
		} 
		else if (CnInputManager.GetButtonUp("Run")) {

			animator.SetBool("Run", false);
			Speed = walkSpeed;
		}

		//GetSword
		if (CnInputManager.GetButtonDown("GetSword")) {

			animator.SetBool("Swording", !animator.GetBool("Swording"));

			AttakBTN.SetActive(animator.GetBool("Swording"));
		}

		if(animator.GetBool("Swording") && CnInputManager.GetButtonDown("Fire1") && forAttack) {
			animator.SetTrigger("Attack");
			StartCoroutine(WaitSecfloat(1f));

		}


		if (CnInputManager.GetButtonDown("Jump")) {

			jump = true;
		}




	}

	public void OnDie() {
		animator.SetTrigger("Die");
	}


	public void Life() {
		animator.SetTrigger("IsALife");
	}


	IEnumerator WaitSecfloat(float time) {
		forAttack = false;
		ifCanMoove = false;
		horizontalMove = 0;
		animator.SetFloat("Speed", 0);
		controller.Move(horizontalMove, false, false); /////// остановить движение в атаке
		controller.enabled = false;
		
		yield return new WaitForSeconds(time);
		controller.enabled = true;
		forAttack = true;
		ifCanMoove = true;
	}

	private void FixedUpdate() {

		if (ifCanMoove) {
			controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		}
		jump = false;

	}
}
