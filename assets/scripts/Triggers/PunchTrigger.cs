using UnityEngine;
using System.Collections;


public class PunchTrigger : MonoBehaviour 
{
	public float respawnTime = 0.5f;

	public bool active = false;

	Mordecai _player;

	//CharacterController _controller;

	bool _collided = false;




	void Start() {
		_player = GetComponentInParent<Mordecai>();
		//_controller = GetComponent<CharacterController>();
	}
	
	
	
	void OnTriggerStay( Collider col ) 
	{
		if( _collided || !active ) return;
	
		if( col.tag == "Enemy" ) 
		{
			_collided = true;
			col.GetComponent<Enemy>().TakeDamage( _player.GetComponent<Status>().Power );


			// ultimo golpe = empurra o inimigo
			// ou faz ele cair
			//Debug.Log(_player);
			//Debug.Log(_player.animationCount);
			//Debug.Log(_player.AttackAnimations.Length);
			//Debug.Log(_controller);

			// voadora - mesma coisa
			if( _player.animationCount >= _player.AttackAnimations.Length ||
			   _player.attacking )// && ( !_controller.isGrounded && _controller.velocity.y!=0 ) 
			{

				Vector3 vec = col.transform.position;
				vec -= _player.transform.position;
				vec.Normalize();
				vec *= 20;
				col.rigidbody.AddForce(vec,ForceMode.Impulse);
			}
			
			if( respawnTime > 0 ) Invoke( "RestartItem", respawnTime );
		}
	}
	
	
	void RestartItem() { _collided = false; }

}
