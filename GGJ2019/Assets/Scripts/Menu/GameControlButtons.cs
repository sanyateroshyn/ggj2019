﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlButtons : MonoBehaviour {

[SerializeField] GameObject PauseScreen;
 public void startGame()
 {
	 	SceneManager.LoadScene("level1");

 }
 void Start()
 {
	  Time.timeScale = 1;
 }
 public void quitGame()
 {

	 Application.Quit();
 }
  public void MainMenu()
 {
		SceneManager.LoadScene(0);
	}
 public void pauseGame()
 {
	 	Time.timeScale = 0;
		PauseScreen.SetActive(true);
 }
 public void resumeGame()
 {
	 Time.timeScale = 1;
	PauseScreen.SetActive(false);
 }

}
