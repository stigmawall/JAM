using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {
	public enum State { Disabled, Enabled, Indicating  };
	//public bool actived = false;

	public Transform from;

	public Transform to;

	public float speed = 0.1F;

	void Update() {
		transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, Time.time * speed);
	}


}
