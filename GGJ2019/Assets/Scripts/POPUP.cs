using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POPUP : MonoBehaviour {


	public static POPUP Instance;

	public string startText;

	public Text m_messageText;

	public GameObject Dialog;

	public void Start() {
		StartCoroutine(ShowInStart(startText));
	}

	public void Awake() {

		if (Instance == null) Instance = this; else return;

		Dialog.SetActive(false);
	}

	public static void ShowMessage(string Message) 
		{
		Instance.m_messageText.text = Message;
		Instance.Dialog.SetActive(true);
	}

	public static void HideMessage() {
		Instance.m_messageText.text = "";
		Instance.Dialog.SetActive(false);
	}

	public IEnumerator ShowInStart(string Message)
		{
		m_messageText.text = Message;
		Dialog.SetActive(true);
		yield return new WaitForSeconds(5.0f);
		Dialog.SetActive(false);
		m_messageText.text = "";
		yield break;
	}
}