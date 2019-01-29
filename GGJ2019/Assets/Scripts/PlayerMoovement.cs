using CnControls;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoovement : MonoBehaviour {

	public int LevelIndex;


	public CharacterController2D controller;
	public Animator animator;
	public GameObject AttakBTN;

	public float Speed;
	public float walkSpeed = 40;
	public float runSpeed = 60f;

	bool stateSpeed = false;
	float horizontalMove = 0f;
	public bool jump = false;
	//bool crouch = false;

	bool ifCanMoove = true;
	bool forAttack = true;


	public GameObject sword;


	public int CountBird = 0;

	public int CountPhotos = 0;


	public CheckPointController[] Points;

	// Use this for initialization
	void Start () {
		AttakBTN.SetActive(false);


		Points = GameObject.Find("CheckPointS").GetComponentsInChildren<CheckPointController>();


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

#if UNITY_ANDROID

		if (CnInputManager.GetButtonDown("Run") && controller.m_Grounded) {

			stateSpeed = !stateSpeed;
			animator.SetBool("Run", stateSpeed);
			Speed = stateSpeed? runSpeed:walkSpeed;
		}
		//} else if (CnInputManager.GetButtonUp("Run")) {

		//	animator.SetBool("Run", false);
		//	Speed = walkSpeed;
		//}
#else 


		if (CnInputManager.GetButton("Run") && controller.m_Grounded) {

			animator.SetBool("Run", true);
			Speed = runSpeed;
		} 
		else if (CnInputManager.GetButtonUp("Run")) {

			animator.SetBool("Run", false);
			Speed = walkSpeed;
		}

#endif

		//GetSword
		if (CnInputManager.GetButtonDown("GetSword")) {

			animator.SetBool("Swording", !animator.GetBool("Swording"));

			AttakBTN.SetActive(animator.GetBool("Swording"));
		}

		if(animator.GetBool("Swording") && CnInputManager.GetButtonDown("Fire1") && forAttack) {
			animator.SetTrigger("Attack");
			sword.SetActive(true);
			
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
		sword.SetActive(false);
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


	private void OnCollisionEnter2D(Collision2D collision) {

		if (collision.gameObject.tag == "panel") {
			transform.SetParent(collision.gameObject.transform);
		} else {

			transform.SetParent(null);
		}


		//if (collision.gameObject.tag == "Enemy") {

		//	if (goDi) {
		//		gameObject.GetComponent<HPController>().takeDamage(10);
		//		goDi = false;
		//		StartCoroutine(Wait());
		//	}

		//}


	}



	private void OnCollisionStay2D(Collision2D collision) {

		if (collision.gameObject.tag == "Enemy") {

			if (goDi) {
				gameObject.GetComponent<HPController>().takeDamage(10);
				goDi = false;
				StartCoroutine(Wait());
			}

		}
	}



	void OnTriggerEnter2D(Collider2D other) {


		//if (other.gameObject.tag == "Die") {
		//	transform.position = LastCheckPoint().position;
		//	gameObject.GetComponent<HPController>().takeDamage(100); // раскоментить

		//}



		if (other.tag == "CheckPoint") {
			other.GetComponent<CheckPointController>().active = true;
			other.GetComponent<BoxCollider2D>().enabled = false;

		}


		if (other.tag == "Bird") {

			CountBird++; // 
			Destroy(other.gameObject);

		}

		if (other.tag == "Photo") {
			CountPhotos++;
			Destroy(other.gameObject);

		}

		if (other.tag == "EndOfLevel") {
			if (LevelIndex == 4) {
				if (CountBird >= 7) {
					SceneManager.LoadScene(LevelIndex);
				} else {
					POPUP.ShowMessage("You have not collected all the chicks !!!");
				}
			}
			if (LevelIndex == 6) {
				if (CountPhotos >= 10) {
					SceneManager.LoadScene(LevelIndex);
				} else {
					POPUP.ShowMessage("You have not collected all the pieces of the photo !!!");
				}
			}
		}



		if (other.tag == "Owl") {

			if (goDi) {
				gameObject.GetComponent<HPController>().takeDamage(10);
				goDi = false;
				StartCoroutine(Wait());
			}

		}





	}

	void OnTriggerExit2D(Collider2D other) 
		{
		POPUP.HideMessage();
	}



	bool goDi = true;

	IEnumerator Wait() {

		yield return new WaitForSeconds(1f);
		goDi = true;
	}

	
	public Transform LastCheckPoint() {

		return (Array.FindLast(Points, x => x.Active)).gameObject.transform;
	}



}
