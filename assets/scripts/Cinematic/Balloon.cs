using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	public string runMethod = "";

	public bool visible = false;

	public GameObject baloom = null;

	private float posX = 0f;

	public Sprite sprite = null;

	public Sprite characterImage = null;

	public Transform pointBalloon;

	//public Vector3 characterPosition;

	// Use this for initialization
	void Start () {
		posX = baloom.transform.position.x;
		//Debug.Log (posX);
		if (visible) {
			show ();
		} else {
			hide ();
			if(runMethod != ""){
				Invoke(runMethod, 2);
			}
		}
	}
	
	// Update is called once per frame
	public void hide () {
		baloom.transform.position = new Vector3 (-99999f,baloom.transform.position.y,baloom.transform.position.z);
	}

	public void show () {
		if(pointBalloon){
			baloom.transform.position = new Vector3(
				pointBalloon.transform.position.x,
				pointBalloon.transform.position.y,
				pointBalloon.transform.position.z);
		}else{
			Debug.Log(gameObject.name);
		}
		  
	}


}
