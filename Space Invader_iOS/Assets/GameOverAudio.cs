using UnityEngine;
using System.Collections;

public class GameOverAudio : MonoBehaviour {

	public AudioClip gameOver;

	void Start () {
		GameOver();
	}
	void GameOver() {
		AudioSource.PlayClipAtPoint (gameOver, transform.position);
	}
}
