using UnityEngine;
using System.Collections;


public class EnemyPunchTrigger : MonoBehaviour 
{

	public float respawnTime = 0.5f;

	public bool active = false;

	public bool collided = false;

	Enemy _enemy;





	void Start() {

		_enemy = GetComponentInParent<Enemy>();
	}
	
	
	
	void OnTriggerStay( Collider col ) 
	{
		if( collided || !active ) return;
	

		if( col.tag == "Player" ) 
		{
			collided = true;


			if( col.GetComponent<Mordecai>() ) 
			{
				if( !col.GetComponent<Mordecai>().dead && !col.GetComponent<Mordecai>().dying )
					col.GetComponent<Mordecai>().TakeDamage( _enemy.GetComponent<Status>().Power );
			} else {
				col.GetComponent<Margareth>().TakeDamage( _enemy.GetComponent<Status>().Power );
			}


			_enemy.PlayPunchSound();


			// ultimo golpe = empurra o inimigo
			// ou faz ele cair
			// voadora - mesma coisa
			if( _enemy.animationCount >= _enemy.AttackAnimations.Length ) {
				col.GetComponent<Mordecai>().Fall();
			}


			
			if( respawnTime > 0 ) Invoke( "RestartItem", respawnTime );
		}
	}
	
	
	void RestartItem() { collided = false; }

}
