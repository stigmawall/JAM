using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	public bool visible = false;

	public GameObject baloom = null;

	private float posX = 0f;

	// Use this for initialization
	void Start () {
		posX = baloom.transform.position.x;
		//Debug.Log (posX);
		if (visible) {
			show ();
		} else {
			hide ();
		}
	}
	
	// Update is called once per frame
	public void hide () {
		baloom.transform.position = new Vector3 (-99999f,baloom.transform.position.y,baloom.transform.position.z);
	}

	public void show () {
		baloom.transform.position = new Vector3 (posX,baloom.transform.position.y,baloom.transform.position.z);
	}


}
