using UnityEngine;
using System.Collections;

public class KeyPress : MonoBehaviour {

	public GameObject title = null;

	public GameObject plano;

	void Start () 
	{
		FadeInAll();
		fadeIn ();
	}



	public void FadeInAll() {
		Hashtable ht = new Hashtable();
		ht.Add("from", 1.0f);
		ht.Add("to", 0.0f);
		ht.Add("time", 1.4f);
		ht.Add("easetype", "linear");
		ht.Add("onupdate", "setAlphaBG");
		ht.Add("onupdatetarget", this.gameObject);
		
		//easetype
		iTween.ValueTo(plano.gameObject, ht);
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



	public void setAlphaBG(float newAlpha) 
	{
		foreach (Material mObj in plano.renderer.materials) 
		{
			mObj.color = new Color(
				mObj.color.r, 
				mObj.color.g, 
				mObj.color.b, 
				newAlpha
			);			
		}		
	}	

	
	void Update () 
	{
		if (Input.anyKey) 
		{
			FadeOutAll();
		}
	}	
		

	public void FadeOutAll () 
	{
		Hashtable ht = new Hashtable();
		ht.Add("from", 0.0f);
		ht.Add("to", 1.0f);
		ht.Add("time", 1.4f);
		ht.Add("easetype", "linear");
		ht.Add("onupdate", "setAlphaBG");
		ht.Add("onupdatetarget", this.gameObject);
		//easetype
		iTween.ValueTo(plano.gameObject, ht);
		
		Invoke ( "GotoMenu", 1.4f );
	}


	public void GotoMenu() {
		Application.LoadLevel( "intro" );
	}
	

}
