using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {
	public enum State { Disabled, Enabled, Indicating  };

	public State state = State.Disabled;

	//public Transform from;

	public Transform target;

	public float speed = 0.1F;

	public float angleRadian = 0f;

	void Update() {
		/*
		Vector3 point = target.position;
		point = new Vector3( point.x, 0.0f, 0.0f);
		transform.LookAt(point);
*/

		var newRotation = Quaternion.LookRotation(transform.position - target.position, Vector3.forward);
		//newRotation.x = 0.0f;
		newRotation.y = 0.0f;
		transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8);


	}




}
