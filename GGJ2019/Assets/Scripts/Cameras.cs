using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour {


	public float speed = 2f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.D)) {
			transform.position += new Vector3(speed * Time.deltaTime, 0,0);

		}
		if (Input.GetKey(KeyCode.A)) {
			transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

		}

	}
}
