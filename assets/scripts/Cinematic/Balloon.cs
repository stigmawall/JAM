using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	public AnimateCutscene animateCuscene = null;

	public AnimateCutscene animateBeforeBalloon = null;

	public bool visible = false;

	private GameObject baloom = null;

	//private float posX = 0f;

	public GameObject sprite = null;

	public GameObject character = null;

	public Transform pointBalloon;

	//public Vector3 characterPosition;

	// Use this for initialization
	void Start () {
		baloom = gameObject;
		//posX = baloom.transform.position.x;
		//Debug.Log (posX);
		if (visible) {
			show ();
		} else {
			hide ();
		}
	}
	
	// Update is called once per frame
	public void hide () {
		if (pointBalloon) {
			if (baloom) {
				baloom.transform.position = new Vector3 (-99999f, baloom.transform.position.y, baloom.transform.position.z);
			}
		}
	}

	public void show () {
		if (animateBeforeBalloon) {
			animateBeforeBalloon.animate();
		}
		if(pointBalloon){
			if(sprite){
				MeshRenderer mr = character.GetComponent<MeshRenderer>();
				mr.renderer.enabled = true;

				MeshRenderer mr2 = sprite.GetComponent<MeshRenderer>();
				//Debug.Log(mr2);
				//.renderer.enabled = true;



				mr.material.mainTexture = mr2.material.mainTexture;
				//sprite.transform.position = character.transform.position;
				//mr = sprite.GetComponent<MeshRenderer>();
				//mr.renderer.enabled = false;
			}

			baloom.transform.position = new Vector3(
				pointBalloon.transform.position.x,
				pointBalloon.transform.position.y,
				pointBalloon.transform.position.z);
		}

		if (animateCuscene) {
			animateCuscene.animate();
		}
		  
	}


}
