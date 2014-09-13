using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {
	public enum State { Disabled, Enabled, Indicating  };

	public State state = State.Disabled;

	//public Transform from;

	public Transform target;

	public float speed = 0.1F;

	public float angleRadian = 0f;

	public Transform falloing = null;

	void Update() {
		//transform.position = new Vector3( falloing.position.x, falloing.position.y+3f, transform.position.z);




		//var newRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.forward);
		//newRotation.x = 0.0f;
		//newRotation.y = 0.0f;
		//Debug.Log ();
		//transform.LookAt(newRotation, target.transform.position);
		//transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);




	}




}
