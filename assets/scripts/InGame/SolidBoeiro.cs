using UnityEngine;
using System.Collections;

public class SolidBoeiro : MonoBehaviour {

	public GameObject toSolid = null;
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (col.gameObject.layer == 9) {
			BoxCollider bc = toSolid.gameObject.GetComponent<BoxCollider>();
			bc.enabled = true;
		}  
	}
}
