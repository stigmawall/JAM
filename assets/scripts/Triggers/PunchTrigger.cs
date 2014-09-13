using UnityEngine;
using System.Collections;


public class PunchTrigger : MonoBehaviour 
{
	public float respawnTime = 0.5f;

	public bool hitSomeone = false;

	public bool collided = false;

	Mordecai _player;

	CharacterController _controller;




	void Start() {
		_player = GetComponentInParent<Mordecai>();
		_controller = GetComponentInParent<CharacterController>();
	}
	
	
	
	void OnTriggerStay( Collider col ) 
	{
		if( collided || !active ) return;
	
		if( col.tag == "Enemy" ) 
		{
			collided = true;
			Enemy e = col.GetComponent<Enemy>();
			e.TakeDamage( _player.GetComponent<Status>().Power );


			// ultimo golpe = empurra o inimigo
			// ou faz ele cair
			// voadora - mesma coisa
			if( _player.animationCount >= _player.AttackAnimations.Length ||
			   ( _player.attacking && !_controller.isGrounded ) ||
				e.status.HP <= 0 )
			{

				Vector3 vec = col.transform.position;
				vec -= _player.transform.position;
				vec += Vector3.up;
				vec.Normalize();
				vec *= ( e.status.HP <= 0 ) ? 100 : 6;
				col.rigidbody.AddForce(vec,ForceMode.Impulse);
			}
			
			if( respawnTime > 0 ) Invoke( "RestartItem", respawnTime );
		}
	}
	
	
	void RestartItem() { collided = false; }

}
