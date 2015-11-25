using UnityEngine;
using System.Collections;

public class CameraGizmo : MonoBehaviour {

	public float width = 5.64f;
	public float height = 10f;
	
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
}
