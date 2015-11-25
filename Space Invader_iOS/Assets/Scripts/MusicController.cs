using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {
	
	static MusicController instance = null;

	public AudioClip startMusic;
	public AudioClip gameMusic;
	public AudioClip loseMusic;
	
	private AudioSource music;

	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
			print ("Duplicate Music Player Destroyed");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource>();
			music.clip = startMusic;
			music.loop = true;
			music.Play();
		}
	}
	void Start () {
		
	}
	void OnLevelWasLoaded(int level) {
		Debug.Log("Music Player: Level Loaded: " + level);
		music.Stop();
		if(level == 0) {
			music.clip = startMusic;
		}
		if(level == 1) {
			music.clip = gameMusic;
		}
		if(level == 2) {
			music.clip = loseMusic;
		}
		music.loop = true;
		music.Play();
	}
}
