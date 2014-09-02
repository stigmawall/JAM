using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (BoxCollider2D))]
public class ClickSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if( Input.GetButton("click") ){
			playSoundInObject();
		}
	}

	void playSoundInObject(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);

		if(hit.collider != null && hit.collider.transform == transform)
		{
			//se exisitr audio e nao estiver tocando ativa som!
			if(audio && !audio.isPlaying){
				audio.Play();
			}
		}
	}

}
