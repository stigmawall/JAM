using UnityEngine;
using System.Collections;

public class ShowBFG : AnimateCutscene {
	public GameObject bfg = null;

	// Use this for initialization
	public override void animate(){
		Show ();


	}
	
	// Update is called once per frame
	void Show () {
		Hashtable tweenParams2 = new Hashtable();
		tweenParams2.Add("x", -15.22629f);
		tweenParams2.Add("y",3.204366f);
		tweenParams2.Add("z", -2.082242f);
		tweenParams2.Add("time", 1);
		tweenParams2.Add("easetype", "easeInBounce");
		iTween.MoveTo( bfg, tweenParams2);	


	}
}
