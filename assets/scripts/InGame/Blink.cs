using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {


	public string axy = "y";  
	public float distance = 1.0f;  

	void Start () {
		show();

	}

	void hide(){
		Hashtable ht = new Hashtable();
		ht.Add(axy, distance);
		ht.Add("time", 1f);
		ht.Add("easetype", "linear");
		ht.Add("oncomplete", "show");
		ht.Add("oncompletetarget", gameObject);
		iTween.MoveAdd(gameObject, ht);
	}

	void show(){
		Hashtable ht = new Hashtable();
		ht.Add(axy, distance*-1);
		ht.Add("time", 1f);
		ht.Add("easetype", "linear");
		ht.Add("oncomplete", "hide");
		ht.Add("oncompletetarget", gameObject);
		iTween.MoveAdd(gameObject, ht);
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

	// Update is called once per frame
	void Update () {
		
	}
}
