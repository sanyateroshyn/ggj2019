using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUDIO : MonoBehaviour {

	public AudioClip AttakClip;
	public AudioClip BG;
	public AudioSource audioSource;

	public static AUDIO s_Instance;

	private void Awake() {
		if (s_Instance == null) { s_Instance = this; DontDestroyOnLoad(this); } else { return; }
	}

	public void PlayAttak() 
		{
		audioSource.PlayOneShot(AttakClip);
	}

}
