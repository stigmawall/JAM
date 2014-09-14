using UnityEngine;
using System.Collections;

public class SuperJump : MonoBehaviour {
	public float jumpSpeed = 100;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 9) {
			CharacterBeatenUp cb = col.gameObject.GetComponent<CharacterBeatenUp>();
			cb.superJump( jumpSpeed );
		}
	}
}
