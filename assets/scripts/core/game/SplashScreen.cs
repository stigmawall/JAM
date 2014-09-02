using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class SplashScreen : MonoBehaviour  {
	public float secondsToDestroy = 5;

	private IEnumerator wait(){
		yield return new WaitForSeconds( secondsToDestroy );
		hiding();
	}

	void Awake(){
		showing();
	}

	void Start(){
		if( secondsToDestroy != -1){
			StartCoroutine(wait());
		}
	}

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
		
		if(hit.collider != null && hit.collider.transform == transform)
		{
			if(Input.GetButton("click")) {
				hiding();
			}
		}		
	}

	public virtual void showing(){
		
	}

	public virtual void hiding(){
		
	}
}