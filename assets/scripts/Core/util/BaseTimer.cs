using UnityEngine;
using System.Collections;

public class BaseTimer : MonoBehaviour {
	public float secondsWaitRun = 50f;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(wait());
	}
	
	private IEnumerator wait(){
		yield return new WaitForSeconds( secondsWaitRun );
		run();
	}

	public virtual void run(){
		
	}
}
