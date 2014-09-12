using UnityEngine;
using System.Collections;

public class KeyPress : MonoBehaviour {

	public GameObject title = null;

	void Start () {
		fadeIn ();
	}

	public void fadeIn(){
		Hashtable ht = new Hashtable();
		ht.Add("from", 1.0f);
		ht.Add("to", 0.0f);
		ht.Add("time", 3f);
		ht.Add("easetype", "linear");
		ht.Add("onupdate", "setAlpha");
		ht.Add("oncomplete", "fadeOut");
		ht.Add("oncompletetarget", gameObject);
		//easetype
		iTween.ValueTo(title, ht);

	}

	public void fadeOut(){
		Hashtable ht = new Hashtable();
		ht.Add("from", 0.0f);
		ht.Add("to", 1.0f);
		ht.Add("time", 3f);
		ht.Add("easetype", "linear");
		ht.Add("onupdate", "setAlpha");
		ht.Add("oncomplete", "fadeIn");
		ht.Add("oncompletetarget", gameObject);
		//easetype
		iTween.ValueTo(title, ht);		
	}

		          
	public void setAlpha(float newAlpha) {
		foreach (Material mObj in renderer.materials) {
			mObj.color = new Color(
				mObj.color.r, 
				mObj.color.g, 
				mObj.color.b, 
				newAlpha
			);			
		}		
	}	
	
	void Update () {
		if (Input.anyKey) {
			Application.LoadLevel ("test_cutscene");
		}
	}
}
