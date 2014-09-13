using UnityEngine;
using System.Collections;

public class SuperJump : MonoBehaviour {
	public float jumpSpeed = 100;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 9) {
			col.gameObject.transform.Translate(Vector3.forward * jumpSpeed * Time.deltaTime);
		}
	}
}
