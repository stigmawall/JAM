using UnityEngine;
using System.Collections;

public class DamageTrigger : MonoBehaviour {


	public float damage = 20;

	public float respawnTime = 3;



	bool _collided = false;

	GameObject _ally;



	void Start() {
		//mordecai = GameObject.FindGameObjectWithTag("Player").GetComponent<Mordecai>();
	}


	void OnTriggerEnter( Collider col ) 
	{
		if( _collided ) return;

		_collided = true;
		if( col.tag == "Player" ) 
		{
			if( col.GetComponent<Mordecai>() ) {
				col.GetComponent<Mordecai>().TakeDamage( damage );
			} else {
				col.GetComponent<Margareth>().TakeDamage( damage );
			}

			if( respawnTime > 0 ) Invoke( "RestartItem", respawnTime );
		}
	}



	void RestartItem() {
		_collided = false;
	}

}
