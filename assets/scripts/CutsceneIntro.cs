using UnityEngine;
using System.Collections;

public class CutsceneIntro : Cutscene {
	enum Action { Talk, BFGComming, Wait };

	public GameObject modercai = null;
	public GameObject margaret = null;
	public GameObject bfg = null;
	public Cutscene nextCutscene = null;

	CutsceneIntro.Action action = CutsceneIntro.Action.Talk;

	void modercaTurn(){
		var tweenParams = new Hashtable();
		tweenParams.Add("y", 98);
		tweenParams.Add("time", 1);
		tweenParams.Add("oncomplete", "commingBigBoss");
		tweenParams.Add("oncompletetarget", gameObject);

		iTween.RotateAdd(modercai, tweenParams);
	}

	void commingBigBoss(){
		var tweenParams = new Hashtable();
		tweenParams.Add("x", 1.814845f);
		tweenParams.Add("time", 1);
		tweenParams.Add("oncomplete", "endCutscene");
		tweenParams.Add("oncompletetarget", gameObject);

		iTween.MoveTo(bfg, tweenParams);
	}

	void endCutscene(){
		stop ();
		if (nextCutscene) {
			nextCutscene.play ();
		}
	}

	void Update () {
		if (! playing) {
			return;
		}

		if (action == CutsceneIntro.Action.Talk) {
			if (Input.GetMouseButtonUp (0)) {
				if(nextBalloon () == false){
					action = CutsceneIntro.Action.BFGComming;
				}
			}
		}
		if (action == CutsceneIntro.Action.BFGComming) {
			action = CutsceneIntro.Action.Wait;
			modercaTurn();
		}
	}
}
