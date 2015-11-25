using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthKeeper : MonoBehaviour {
	
	public static int health = 100;
	
	private Text myText;
	
	void Start () {
		HealthReset();
		myText = GetComponent<Text>();
	}
	
	public void Health(int damage) {
		health += damage;
		myText.text = health.ToString() + " :HEALTH";
	}
	
	public static void HealthReset() {
		health = 100;
	}
}
