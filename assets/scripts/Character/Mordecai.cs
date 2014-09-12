using UnityEngine;
using System.Collections;

public class Mordecai : MonoBehaviour 
{
	

	public string[] AttackAnimations;

	public string FlyingKickAnimation;

	public bool attacking;



	CharacterController _controller;

	Animation _animations;

	int _animationCount;

	int _animationPlayed;




	void Start() 
	{
		attacking = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		_controller = (CharacterController)GetComponent(typeof(CharacterController));
	}
	



	void Update()
	{
		if( Input.GetKey( KeyCode.O ) && !attacking ) 
		{
			if ( _controller.isGrounded ) {
				StartCoroutine( Punch() );
			} else {
				StartCoroutine( FlyingKick() );
			}
		}
	}


	IEnumerator Punch() 
	{
		attacking = true;

		// primeiro soco
		_animations.CrossFade( AttackAnimations[ _animationCount ] );
		_animationCount++;
		yield return new WaitForSeconds( 0.4f );


		_animationPlayed++;
		attacking = false;

		if( _animationPlayed >=  AttackAnimations.Length ) 
			_animationCount = _animationPlayed =  0;


		yield break;
	}



	IEnumerator FlyingKick() 
	{
		attacking = true;
		_animations.CrossFade( FlyingKickAnimation );
		yield return new WaitForSeconds( 0.6f );
		attacking = false;
		yield break;
	}


}