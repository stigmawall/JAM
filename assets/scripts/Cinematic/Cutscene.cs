using UnityEngine;
using System.Collections;

public class Cutscene : MonoBehaviour {
	public Balloon[] balloon;
	public int index = 0;
	public bool playing = false;

	public bool nextBalloon(){
		balloon [index].hide();

		index++;

		if( index >= balloon.Length ){
			index = balloon.Length-1;
			balloon [index].hide();
			return false;
		}

		balloon [index].show();
		return true;
	}

	public void play(){
		playing = true; 
		index = 0;
		if (balloon.Length > 0) {
			balloon[index].show();
		}
	}

	public void stop(){
		playing = false; 
	}

}
