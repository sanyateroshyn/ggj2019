using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision) {

		if (collision.gameObject.tag == "Enemy") {
			collision.gameObject.GetComponent<EnemyHp>().TakeDamage(10);
			}

		}


}

