using UnityEngine;
using System.Collections;

public class ControlMusic : MonoBehaviour {

	public AudioSource music = null;

	// Use this for initialization
	void Start () {
		music.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
