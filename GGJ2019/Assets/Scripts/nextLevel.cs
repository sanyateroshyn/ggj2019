using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevel : MonoBehaviour {

[SerializeField] int nextLevelNumber;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag =="Player")
		{
		 SceneManager.LoadScene(nextLevelNumber);
		}
	}
}
 
