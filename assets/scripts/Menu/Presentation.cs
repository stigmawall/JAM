using UnityEngine;
using System.Collections;

public class Presentation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.FadeFrom( this.gameObject, 0, 1.4f );
		Invoke ( "HidePresentation", 3 );
	}
	
	void HidePresentation() 
	{
		iTween.FadeTo( this.gameObject, 0, 1.4f );
		Invoke ( "GotoMenu", 1.4f );
	}

	void GotoMenu() {
		Application.LoadLevel( "menu" );
	}
}
