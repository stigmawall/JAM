using UnityEngine;
using System.Collections;

public class GameOverBehaviour : MonoBehaviour {



	void Start () {

		iTween.FadeFrom( this.gameObject, 0, 1.4f );
		Invoke ( "Return", 12 );
	}
	

	void Update () {

		if( Input.anyKey ) Return();
	}


	void Return() {
		CancelInvoke("Return");
		iTween.FadeTo( this.gameObject, 0, 1.4f );
		Invoke ( "GotoMenu", 1.4f );
	}


	void GotoMenu() {
		Application.LoadLevel( "menu" );
	}
}
