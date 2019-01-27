using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour {

	public int thisHp;
	public int hp = 100;

	private void Start() {
		thisHp = hp;
	}


	public void TakeDamage(int takeDamage) {

		StartCoroutine(timeDeley());

		thisHp = thisHp - takeDamage;
		if(thisHp <= 0) {
			Destroy(gameObject);
		}
	}


	IEnumerator timeDeley() {
		yield return new WaitForSeconds(1f);

	}

}
