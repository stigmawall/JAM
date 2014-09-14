using UnityEngine;
using System.Collections;

public class BeContinue : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 9) {
			Application.LoadLevel ("BeContinue");
		}
	}
}
