using UnityEngine;
using System.Collections;

public class ControlDoor : MonoBehaviour {
	//public enum Action {stop,open,close,opened};
	
	public GameObject target = null;	
	
	
	
	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.layer == 9 )//"Player"
		{
			Hashtable ht = new Hashtable();
			ht.Add("y", 10);
			ht.Add("time", 0.5f);
			ht.Add("oncompletetarget", gameObject);
			iTween.MoveTo(target, ht );
		}
	}
	
}
