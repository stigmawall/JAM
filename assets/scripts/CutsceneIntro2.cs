using UnityEngine;
using System.Collections;

public class CutsceneIntro2 : Cutscene {
	enum Action { Talk, Animate, Wait };
	
	public GameObject car = null;
	public GameObject bfg = null;
	public Cutscene nextCutscene = null;
	
	CutsceneIntro2.Action action = CutsceneIntro2.Action.Talk;
	
	void bfgTeleport(){
		//Debug.Log ("Chegou!");
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
		stop ();
	}

	void playGame(){

	}
	
	void Update () {
		if (! playing) {
			return;
		}
		
		if (action == CutsceneIntro2.Action.Talk) {
			if (Input.GetMouseButtonUp (0)) {
				if(nextBalloon () == false){
					action = CutsceneIntro2.Action.Animate;
				}
			}
		}
		if (action == CutsceneIntro2.Action.Animate) {
			action = CutsceneIntro2.Action.Wait;
			bfgTeleport();
		}
	}
}
