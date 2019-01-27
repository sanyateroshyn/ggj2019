using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledBridge : MonoBehaviour {

	public GameObject bridge;

	public void Enabled() {
		bridge.SetActive(true);
	}
}
