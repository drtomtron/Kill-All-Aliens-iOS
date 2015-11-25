using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemyPrefab;
	public float width = 6.5f;
	public float height = 4.3f;
	public float enemySpeed = 1f;
	
	private bool movingLeft = true;
	private float xmin;
	private float xmax;
	private float spawnDelay = 1f;
	
	void Start () {
		if(AllEnemiesDead()) {
			EnemySpawn();
		}
	}
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	void Update () {
		EnemyFormationMovementLimit();
		EnemyFormationMovementControl();
		if (AllEnemiesDead()) {
			Debug.Log("All Enemies Dead");
			EnemySpawn ();
		}
	}
	void EnemySpawn() {
		Transform freePosition = NextFreePosition();
		if(freePosition) {
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if(NextFreePosition()) {
			Invoke ("EnemySpawn", spawnDelay);
		}
	}
	void EnemyFormationMovementLimit() {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x;
		xmax = rightmost.x;
	}
	void EnemyFormationMovementControl() {
		if(movingLeft) {
			transform.position += Vector3.left * enemySpeed * Time.deltaTime;
		} else {
			transform.position += Vector3.right * enemySpeed * Time.deltaTime;
		}
		
		//tells the formation when to move left or right
		float rightEdgeOfFormation = transform.position.x;
		float leftEdgeOfFormation = transform.position.x;
		if(rightEdgeOfFormation > xmax) {
			movingLeft = true;
		} else if (leftEdgeOfFormation < xmin) {
			movingLeft = false;
		}
		
		//limits the movement of the enemy formation within camera viewport
		float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	Transform NextFreePosition() {
		foreach(Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}
	bool AllEnemiesDead() {
		foreach(Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
	}
}
