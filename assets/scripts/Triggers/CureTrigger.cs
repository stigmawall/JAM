using UnityEngine;
using System.Collections;

public class CureTrigger : MonoBehaviour {

	
	public float heal = 50;

	public float respawnTime = 3;

	


	bool _collided = false;


	
	void OnTriggerEnter( Collider col ) 
	{
		if( _collided ) return;
		
		_collided = true;
		if( col.tag == "Player" ) 
		{
			
			if( col.GetComponent<Mordecai>() ) {
				col.GetComponent<Mordecai>().Heal( heal );
			} else {
				col.GetComponent<Margareth>().Heal( heal );
			}


			if( respawnTime > 0 ) Invoke( "RestartItem", respawnTime );
		}
	}


	void RestartItem() {
		_collided = false;
	}
}
