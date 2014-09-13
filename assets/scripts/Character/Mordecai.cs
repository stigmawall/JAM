using UnityEngine;
using System.Collections;

public class Mordecai : MonoBehaviour 
{

	/**********************************
	 * 
	 * Gerencia estados, animaçoes e funçoes referentes ao personagem principal
	 * 
	 *********************************/ 


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

	PunchTrigger _punch;

	Status _status;

	int _animationCount;

	int _animationPlayed;




	void Start() 
	{
		attacking = hitted = dying = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		_controller = (CharacterController)GetComponent(typeof(CharacterController));
		_status = GetComponent<Status>();
		_punch = GetComponentInChildren<PunchTrigger>();
	}
	



	void Update()
	{

		// atacando - no chao e no ar
		if( Input.GetKey( KeyCode.O ) && !attacking ) 
		{
			if ( _controller.isGrounded || _controller.velocity.y==0 ) {
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





	// STATUS CONTROLLERS
	public void TakeDamage( float damage ) 
	{

		// marca o dano
		_status.HP -= damage;

		if( _status.HP <= 0 ) 
		{
			StartCoroutine( Dying() );
		} else {

			StartCoroutine( GetHit() );
		}

		// imprime o valor atual
		Debug.Log ( "DAMAGE - " + _status.HP );
	}


	public void Heal( float value ) 
	{
		_status.HP += value;
		if( _status.HP >= _status.MAXHP ) _status.HP = _status.MAXHP;
		Debug.Log ( "HEAL - " + _status.HP );
	}




	// ANIMATION CONTROLLERS

	/// <summary>
	/// Punch animation controller.
	/// </summary>
	IEnumerator Punch() 
	{
		attacking = true;

		// primeiro soco
		_animations.CrossFade( AttackAnimations[ _animationCount ] );
		_animationCount++;


		// controla tempo de apariçao da bouding box
		yield return new WaitForSeconds( 0.1f );

		_punch.active = true;

		yield return new WaitForSeconds( 0.3f );

		_punch.active = false;
		_animationPlayed++;
		attacking = false;


		if( _animationPlayed >=  AttackAnimations.Length ) 
			_animationCount = _animationPlayed =  0;


		yield break;
	}



	/// <summary>
	/// Flying kick animation - when he's jumping.
	/// </summary>
	IEnumerator FlyingKick() 
	{
		attacking = true;
		_animations.CrossFade( FlyingKickAnimation );
		yield return new WaitForSeconds( 0.6f );
		attacking = false;
		yield break;
	}




	/// <summary>
	/// Animate him taking hit.
	/// </summary>
	IEnumerator GetHit()
	{
		hitted = true;
		_animations.CrossFade( HitAnimation );
		yield return new WaitForSeconds( 0.4f );
		hitted = false;
		yield break;
	}





	/// <summary>
	/// Animate the payer to die
	/// </summary>
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