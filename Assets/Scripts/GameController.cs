﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject hazzard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	private int score;
	private bool gameOver;
	private bool restart;

	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
	}
	void Update(){
		if(restart){
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds(startWait);
		while (true) {
			for (int i=0; i<hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazzard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if(gameOver){
				restartText.text="Press 'R' to restart";
				restart = true;
				break;
			}
		}
	
	}
	public void AddScore(int newScore){
		score += newScore;
		UpdateScore ();
	}
	public void GameOver(){
		gameOver = true;
		gameOverText.text = "Game Over";
	}
	void UpdateScore(){
		scoreText.text = "Score: " + score;
	}

}