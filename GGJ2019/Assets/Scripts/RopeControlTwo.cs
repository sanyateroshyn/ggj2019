using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CnControls;

public class RopeControlTwo : MonoBehaviour {

	public float climbUpInterval = 0.05f;       //interval between climbing up
	public float climbDownInterval = 0.04f;     //interval between climbing down
	public float swingForce = 10.0f;            //force which will be added to connected chain when left/right buttons will be pressed
	public float delayBeforeSecondHang = 0.4f;  //delay after jump before player will be able to hang on another rope

	private static Transform collidedChain;     //saves transform on which player is connected
	private static List<Transform> chains;      //saves connected rope's chain objects

	private Transform playerTransform;          //saves player's transform   private
	private int chainIndex = 0;                 //saves chains index on which player is connected
	private Collider2D[] colliders;             //saves all colliders of player
             //used for enabling/disabling PlayerControl script
	private Animator anim;                      //used for playing animations


	public float AddForceY = 400f;
	public float AddForceX = 200f;

	private bool onRope = false;
	private float timer = 0.0f;
	private float direction;

	private Rigidbody2D m_Rigidbody2D;

	private CharacterController2D characterController;
	private PlayerMoovement player;



	// Use this for initialization
	void Start() {
		//get player's components
		playerTransform = transform;

		colliders = GetComponents<Collider2D>();

		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		anim = GetComponent<Animator>();

		player = GetComponent<PlayerMoovement>();
		characterController =  GetComponent<CharacterController2D>();
	}


	// Update is called once per frame
	void Update() {
		if (onRope) {

			float H = CnInputManager.GetAxis("Horizontal");
			//make player's position and rotation same as connected chain's
			playerTransform.position = collidedChain.position;
			playerTransform.localRotation = Quaternion.AngleAxis(direction, Vector3.forward);

			//if up button is pressed and "chainIndex > 1" (there is another chain above player), climb up
			if (CnInputManager.GetAxisRaw("Vertical") > 0 && chainIndex > 1) {
				timer += Time.deltaTime;

				if (timer > climbUpInterval) {
					ClimbUp();
					timer = 0.0f;
				}
			}

			//if down button is pressed and "chainIndex < 1" (there is another chain below player), climb down
			if (CnInputManager.GetAxisRaw("Vertical") < 0) {
				if (chainIndex < chains.Count - 2) {   // -1 до последней -2 до предпоследней  --- заставить не спускаться на самую нижнюю ступень
					timer += Time.deltaTime;

					if (timer > climbUpInterval) {
						ClimbDown();
						timer = 0.0f;
					}
				} else {
					//ClimbUp(); // заставить не спускаться на самую нижнюю ступень
					//StartCoroutine(JumpOff()); //if there isn't chain below player, jump from rope  падение с веревки если дошел до ее конца  с низу
				}
			}

			//if jump button is pressed, jump from rope
			if (CnInputManager.GetButtonDown("Jump")) {

				StartCoroutine(JumpOff());

				if (!characterController.m_FacingRight) {
					m_Rigidbody2D.AddForce(new Vector2(-AddForceX, AddForceY));
				} else if(characterController.m_FacingRight) {
					m_Rigidbody2D.AddForce(new Vector2(AddForceX, AddForceY));
				}
				
				//characterController.Jump(H);
				//player.jump = true;

			}

			// Cache the horizontal CnInputManager.
			

			if (H > 0 && !characterController.m_FacingRight)
				// ... flip the player.
				characterController.Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (H < 0 && characterController.m_FacingRight)
				// ... flip the player.
				characterController.Flip();

			//add swing force to connected chain
			collidedChain.GetComponent<Rigidbody2D>().AddForce(Vector2.right * H * swingForce);
		}
	}


	void ClimbDown() {
		//get all HingeJoint2D components from chain below the player
		var joint = chains[chainIndex + 1].GetComponent<HingeJoint2D>();

		//if chain has HingeJoint2D but isn't enabled jump from rope
		if (joint && !joint.enabled) {
			StartCoroutine(JumpOff());
			return;
		}

		//connect player to below chain
		collidedChain = chains[chainIndex + 1];
		playerTransform.parent = collidedChain;
		chainIndex++;
	}


	void ClimbUp() {
		//get all HingeJoint2D components from chain above the player
		var joint = chains[chainIndex - 1].GetComponent<HingeJoint2D>();

		//if chain has HingeJoint2D but isn't enabled don't do anything
		if (joint && !joint.enabled)
			return;

		//connect player to above chain
		collidedChain = chains[chainIndex - 1];
		playerTransform.parent = collidedChain;
		chainIndex--;
	}


	IEnumerator JumpOff() {
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;    //reset velocity
		playerTransform.parent = null;          //detach player from chain
		onRope = false;

		transform.rotation = Quaternion.identity;
		//player.enabled = true;            //activate PlayerControl script
		//characterController.enabled = true;

		yield return new WaitForSeconds(delayBeforeSecondHang); //wait 0.5 second

		player.enabled = true;           
		characterController.enabled = true;


		//activate all colliders on player
		foreach (var col in colliders) {
			col.isTrigger = false;
			//col.enabled = true;
		}

	}


	//called when player collides with another object
	IEnumerator OnCollisionEnter2D(Collision2D coll) {
		//if collided object's tag is "rope2D" and it has HingeJoint2D component, connect player to that object
		if (!characterController.m_Grounded && coll.gameObject.tag == "rope2D") {
			var joint = coll.gameObject.GetComponent<HingeJoint2D>(); //get HingeJoint2D component from collided object

			if (joint && joint.enabled) {

				player.enabled = false;   //disable PlayerControl script
				characterController.enabled = false;

				anim.SetTrigger("RESET"); // reset animator
				anim.SetFloat("Speed", 0);
				anim.SetBool("Swording", false);

				//disable all player colliders
				foreach (var col in colliders) {

					col.isTrigger = true;
					//col.enabled = false;
				}


				var chainsParent = coll.transform.parent;   //get collided object's parent
				chains = new List<Transform>();

				//fill chains list 
				foreach (Transform child in chainsParent)
					chains.Add(child);

				//connect player to collided object
				collidedChain = coll.transform;
				chainIndex = chains.IndexOf(collidedChain);
				playerTransform.parent = collidedChain;
				onRope = true;

				direction = Mathf.Sign(Vector3.Dot(-collidedChain.right, Vector3.up));
			}
		}
		return null;
	}
}
