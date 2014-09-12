using UnityEngine;
using System.Collections;

public class Mordecai : MonoBehaviour 
{
	

	public string[] AttackAnimations;

	public string FlyingKickAnimation;

	public string HitAnimation;

	public string DieAnimation;

	public string PowerAnimation;

	public string AssistAnimation;



	public bool attacking;

	public bool hitted;

	public bool dying;



	CharacterController _controller;

	Animation _animations;

	int _animationCount;

	int _animationPlayed;




	void Start() 
	{
		attacking = hitted = dying = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		_controller = (CharacterController)GetComponent(typeof(CharacterController));
	}
	



	void Update()
	{

		// atacando - no chao e no ar
		if( Input.GetKey( KeyCode.O ) && !attacking ) 
		{
			if ( _controller.isGrounded ) {
				StartCoroutine( Punch() );
			} else {
				StartCoroutine( FlyingKick() );
			}
		}


		// para testes: animaçao caso seja atingido
		if( Input.GetKey( KeyCode.I ) )  {
			StartCoroutine( GetHit() );
		}

		// para testes: animaçao caso morra
		if( Input.GetKey( KeyCode.U ) )  {
			StartCoroutine( Dying() );
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




	IEnumerator GetHit()
	{
		hitted = true;
		_animations.CrossFade( HitAnimation );
		yield return new WaitForSeconds( 0.4f );
		hitted = false;
		yield break;
	}




	IEnumerator Dying()
	{
		dying = true;
		_animations.CrossFade( DieAnimation );
		yield return new WaitForSeconds( 2 );

		// nao cancela na real, mas para testes, sim
		dying = false;
		yield break;
	}

}