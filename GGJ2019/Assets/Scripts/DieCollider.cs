using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCollider : MonoBehaviour {


	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") {
			//Debug.Log("Die");
			collision.transform.position = collision.transform.GetComponent<PlayerMoovement>().LastCheckPoint().position;
			collision.gameObject.GetComponent<HPController>().takeDamage(100); // раскоментить

		}
	}

}
