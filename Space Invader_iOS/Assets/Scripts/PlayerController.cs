using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public GameObject playerLaser;
	public GameObject playerDeathPS;
	public float laserSpeed = 2.0f;
	public float fireRate = 1f;
	public float playerHealth = 100f;
	public float shipSpeed = 8.0f;
	public float padding = 0.22f;
	public int shipHealth = 100;
	public AudioClip playerHit;
	public AudioClip playerExplode;
	
	private LevelManager levelManager;
	private HealthKeeper healthKeeper;
	private float xmin;
	private float xmax;
	
	void Start () {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		healthKeeper = GameObject.Find("Health").GetComponent<HealthKeeper>();
	}
	
	void Update () {
		PlayerMovementLimit ();
		PlayerMovementControl();	
		if(Input.touchCount > 0){
			InvokeRepeating("ShootLaser", 0.000001f, fireRate);
		} else if (Input.touchCount == 0) {
			CancelInvoke("ShootLaser");
		}
	}
	void OnTriggerEnter2D(Collider2D collider) {
		EnemyLaser laser = collider.gameObject.GetComponent<EnemyLaser> ();
		if (laser) {
			playerHealth = playerHealth - laser.laserDamage;
			Debug.Log (playerHealth);
			shipHealth =- 10;
			healthKeeper.Health(shipHealth);
			AudioSource.PlayClipAtPoint (playerHit, transform.position);
		}
		Destroy(collider.gameObject);
		if (playerHealth <= 0) {
			Die();
		}
	}
	void Die () {
		PlayerDeathExplosion ();
		AudioSource.PlayClipAtPoint (playerExplode, transform.position);
		LevelManager man = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		man.LoadLevel("Lose");
		Destroy (gameObject);
	}
	void PlayerMovementLimit () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x + padding;
		xmax = rightmost.x - padding;
	}
	void PlayerMovementControl () {
		transform.Translate(Input.acceleration.x, 0, 0);
		
		//restricts player ship to camera view
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	void ShootLaser() {
		GameObject beam = Instantiate (playerLaser, transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0, laserSpeed, 0);
	}
	void PlayerDeathExplosion() {
		Instantiate (playerDeathPS, transform.position, Quaternion.identity);
	}
}