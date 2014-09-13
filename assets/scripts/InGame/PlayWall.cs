using UnityEngine;
using System.Collections;

public class PlayWall : MonoBehaviour {
	public ControlCamera controlCamera = null;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == 9){
			controlCamera.isFollow = true;
		}
	}

}
