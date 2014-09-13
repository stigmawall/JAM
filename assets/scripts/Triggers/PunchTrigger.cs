using UnityEngine;
using System.Collections;


public class PunchTrigger : MonoBehaviour 
{
	public float respawnTime = 0.5f;

	public bool active = false;

	Mordecai _player;

	bool _collided = false;




	void Start() {

		_player = GetComponentInParent<Mordecai>();
	}
	
	
	
	void OnTriggerStay( Collider col ) 
	{
		if( _collided || !active ) return;
	
		if( col.tag == "Enemy" ) 
		{
			_collided = true;
			col.GetComponent<Enemy>().TakeDamage( _player.GetComponent<Status>().Power );
			
			if( respawnTime > 0 ) Invoke( "RestartItem", respawnTime );
		}
	}
	
	
	void RestartItem() { _collided = false; }

}
