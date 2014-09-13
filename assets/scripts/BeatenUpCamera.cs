using UnityEngine;
using System.Collections;

public class BeatenUpCamera : MonoBehaviour {

	public Camera mainCamera=null;

	public Camera mainSlave=null;

	public GameObject characterHero=null;

	public GameObject characterSlave=null;

	public float distance = 0f;

	public GameObject line = null;

	// Use this for initialization
	void Start () {
		line.renderer.enabled = false;
		mainCamera.rect = new Rect (0, 0, 1, 1);
		mainSlave.rect = new Rect (0, 0, 0, 0);

		mainSlave.transform.position = new Vector3 (
			mainCamera.transform.position.x,
			mainCamera.transform.position.y,
			mainCamera.transform.position.z);
		mainSlave.transform.rotation.Set(
			mainCamera.transform.rotation.x,
			mainCamera.transform.rotation.y,
			mainCamera.transform.rotation.z,
			mainCamera.transform.rotation.w
		);

	}

	// Update is called once per frame
	void Update () {
		if (characterHero && characterSlave && mainCamera && mainSlave ) {
			float dist = Vector3.Distance(characterHero.transform.position, characterSlave.transform.position);
			if(dist > distance){
				line.renderer.enabled = true;
				mainCamera.rect = new Rect (0, 0, 1, 0.5f);
				mainSlave.rect = new Rect (0, 0.5f, 1, 0.5f);
			}else{
				line.renderer.enabled = false;
				mainCamera.rect = new Rect (0, 0, 1, 1);
				mainSlave.rect = new Rect (0, 0, 0, 0);
			}
		}
	}
}
