using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOTONextScene : MonoBehaviour {

	public int SceneIndex;

	public void OnEndAnim() 
		{
		SceneManager.LoadScene(SceneIndex);
	}

	public void OnClickStartGameButton(int SceneIndex) 
		{
		SceneManager.LoadScene(SceneIndex);
	}
}
