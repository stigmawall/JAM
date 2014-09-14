using UnityEngine;
using System.Collections;

public class CutsceneIntro : Cutscene {
	enum Action { Talk, BFGComming, Wait };
	
	public GameObject modercai = null;
	public GameObject margaret = null;
	public GameObject bfg = null;
	public Cutscene nextCutscene = null;

	CutsceneIntro.Action action = CutsceneIntro.Action.Talk;


	void Start() {
		GameObject bg = GameObject.Find ("BGPlane");
		iTween.FadeTo( bg.gameObject, 0, 1.4f );
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
		/*
		if (action == CutsceneIntro.Action.BFGComming) {
			action = CutsceneIntro.Action.Wait;
			modercaTurn();
		}
		*/
	}
}
