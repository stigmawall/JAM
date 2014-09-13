using UnityEngine;
using System.Collections;

public class ControlCamera : MonoBehaviour {
	public GameObject wallTop = null;
	public GameObject wallBotton = null;
	public GameObject wallLeft = null;
	public GameObject wallRigth = null;
	public bool isFollow = false; 
	public GameObject target = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isFollow) {
			wallTop.transform.position = new Vector3 (
				target.transform.position.x-5f,
				wallTop.transform.position.y,
				wallTop.transform.position.z
			);
			wallBotton.transform.position = new Vector3 (
				target.transform.position.x-5f,
				wallBotton.transform.position.y,
				wallBotton.transform.position.z
			);
			wallLeft.transform.position = new Vector3 (
				target.transform.position.x-5f,
				wallLeft.transform.position.y,
				wallLeft.transform.position.z
			);
			wallRigth.transform.position = new Vector3 (
				target.transform.position.x+5f,
				wallRigth.transform.position.y,
				wallRigth.transform.position.z
			);
		}
	}
}
