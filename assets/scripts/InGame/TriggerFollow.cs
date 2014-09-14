using UnityEngine;
using System.Collections;

public class TriggerFollow : MonoBehaviour {
	public GameObject margaret = null;
	public bool activeMargaret = false;
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 9) {
			Margareth m = margaret.GetComponent<Margareth>();
			m.active = activeMargaret;
		}
	}
}
