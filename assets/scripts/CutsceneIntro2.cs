using UnityEngine;
using System.Collections;

public class CutsceneIntro2 : Cutscene {
	enum Action { Talk, Animate, Wait };
	
	public GameObject car = null;
	public GameObject bfg = null;
	public Cutscene nextCutscene = null;
	
	CutsceneIntro2.Action action = CutsceneIntro2.Action.Talk;
	
	void bfgTeleport(){
		var tweenParams = new Hashtable();
		tweenParams.Add("alpha", 0);
		tweenParams.Add("time", 1);
		iTween.FadeTo(car, tweenParams);

		var tweenParams2 = new Hashtable();
		tweenParams2.Add("alpha", 0);
		tweenParams2.Add("time", 1);
		tweenParams2.Add("oncomplete", "endCutscene");
		tweenParams2.Add("oncompletetarget", gameObject);
		iTween.FadeTo(bfg, tweenParams2);
	}
	
	void endCutscene(){
		stop ();
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
