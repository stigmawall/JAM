using UnityEngine;
using System.Collections;

public class StopWall : MonoBehaviour {
	public ControlCamera controlCamera = null;
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.layer == 9){
			controlCamera.isFollow = false;
		}
	}

}
