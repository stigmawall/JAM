using UnityEngine;
using System.Collections;


public class EnemyPunchTrigger : MonoBehaviour 
{

	public float respawnTime = 0.5f;

	public bool active = false;

	Enemy _enemy;

	bool _collided = false;




	void Start() {

		_enemy = GetComponentInParent<Enemy>();
	}
	
	
	
	void OnTriggerStay( Collider col ) 
	{
		if( _collided || !active ) return;
	
		if( col.tag == "Player" ) 
		{
			_collided = true;

			if( col.GetComponent<Mordecai>() ) 
			{
				col.GetComponent<Mordecai>().TakeDamage( _enemy.GetComponent<Status>().Power );
			} else {
				col.GetComponent<Margareth>().TakeDamage( _enemy.GetComponent<Status>().Power );
			}

			
			if( respawnTime > 0 ) Invoke( "RestartItem", respawnTime );
		}
	}
	
	
	void RestartItem() { _collided = false; }

}
