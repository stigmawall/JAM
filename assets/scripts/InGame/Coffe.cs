using UnityEngine;
using System.Collections;

public class Coffe : MonoBehaviour {
	public float health = 100f;
	public GameObject coffe = null;
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.layer == 9) {
			Mordecai m = col.gameObject.GetComponent<Mordecai>();
			m.Heal(health);
			Destroy(coffe);
		}
	}



}
