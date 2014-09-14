using UnityEngine;
using System.Collections;

public class TeleportSomethings : AnimateCutscene {

	public GameObject car = null;
	public GameObject bfg = null;
	public GameObject balloon = null;

	public override void animate(){

		bfgTeleport ();
	}

	enum Action { Talk, Animate, Wait };

	void bfgTeleport(){
		var tweenParams = new Hashtable();
		tweenParams.Add("x", 0);
		tweenParams.Add("y", 0);
		tweenParams.Add("z", 0);
		tweenParams.Add("easetype", "easeInBounce");
		iTween.ScaleTo(car, tweenParams);
		
		var tweenParams2 = new Hashtable();
		tweenParams2.Add("x", 0);
		tweenParams2.Add("y", 0);
		tweenParams2.Add("z", 0);
		tweenParams2.Add("easetype", "easeInBounce");
		tweenParams2.Add("oncomplete", "endCutscene");
		tweenParams2.Add("oncompletetarget", gameObject);
		iTween.ScaleTo(bfg, tweenParams2);



	}
	
	void endCutscene(){
		var tweenParams = new Hashtable();
		tweenParams.Add("amount", 1);
		tweenParams.Add("time", 2);
		tweenParams.Add("oncompletetarget", gameObject);
		tweenParams.Add("oncomplete", "playGame");
		
		iTween.CameraFadeAdd();
		iTween.CameraFadeTo(tweenParams);
		MeshRenderer mr = balloon.GetComponent<MeshRenderer>();
		mr.renderer.enabled = false;	
	}
	
	void playGame(){
		
	}

}
