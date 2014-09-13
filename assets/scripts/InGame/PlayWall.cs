using UnityEngine;
using System.Collections;

public class PlayWall : MonoBehaviour {
	public ControlCamera controlCamera = null;

	public bool enabled = true; 

	void OnTriggerEnter (Collider other) {
		if (! enabled) {
			return;		
		}
		if (other.gameObject.layer == 9){
			controlCamera.isFollow = true;
		}
	}

}
