using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public GameObject enemyDeathPS;
	public GameObject enemyLaser;
	public float enemyLaserSpeed = 2f;
	public float enemyHealth = 1000f;
	public int scoreValue = 1000;
	public AudioClip enemyHit;
	public AudioClip enemyExplode;
	
	private float enemyFireRate = 1.5f;
	private ScoreKeeper scoreKeeper;

	void Start () {
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update () {
		float probability = Time.deltaTime / enemyFireRate;
		float random = Random.value;
		if(random < probability) {
			EnemyAttack();
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		Laser laser = collider.gameObject.GetComponent<Laser> ();
		if (laser) {
			enemyHealth = enemyHealth - laser.laserDamage;
			AudioSource.PlayClipAtPoint (enemyHit, transform.position);
			Debug.Log (enemyHealth);
		}
		Destroy(collider.gameObject);
		if (enemyHealth <= 0) {
			EnemyDeathExplosion ();
			Destroy (gameObject);
			scoreKeeper.Score(scoreValue);
		}
	}
	void EnemyAttack() {
		Vector3 laserOffset = transform.position + new Vector3(0, -0.35f, 0);
		GameObject laserbeam = Instantiate (enemyLaser, laserOffset, Quaternion.identity) as GameObject;
		laserbeam.rigidbody2D.velocity = new Vector3(0, -enemyLaserSpeed, 0);
	}

	void EnemyDeathExplosion() {
		//spawns death explosion particle system on death of enemy
		Instantiate (enemyDeathPS, transform.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint (enemyExplode, transform.position);
	}
}
