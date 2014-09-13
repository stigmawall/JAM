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

	public int runMaxAnimationSpeed = 1;

	public string WalkAnimation;

	public string JumpAnimation;

	public string IdleAnimation;

	public bool attacking;

	public bool hitted;

	public bool dying;



	CharacterController _controller;

	Animation _animations;

	PunchTrigger _punch;

	Status _status;

	int _animationCount;

	int _animationPlayed;

	public bool isStand;


	void Start() 
	{
		attacking = hitted = dying = false;
		_animationCount = _animationPlayed = 0;
		isStand = true;
		_animations = (Animation)GetComponent(typeof(Animation));
		_controller = (CharacterController)GetComponent(typeof(CharacterController));
		_status = GetComponent<Status>();
		_punch = GetComponentInChildren<PunchTrigger>();
		_punch.active = false;
	}
	



	public void animateState(CharacterBeatenUp.CharacterState state )
	{

		_animations = (Animation)GetComponent(typeof(Animation));
		CharacterController controller = GetComponent<CharacterController>();


		// se estiver atacando, controla no update
		if( attacking || hitted || dying ) return;


		// controla animaçoes de andar, pular e stand
		if(state == CharacterBeatenUp.CharacterState.Jumping) 
		{
			isStand = false;
			//_animations[JumpAnimation].speed = Mathf.Clamp(controller.velocity.magnitude, 0, runMaxAnimationSpeed);
			_animations.Play( "rig_mullet|Attack 1_Recover" ); // <<<< gambi da porra
			_animations.CrossFade(JumpAnimation);

		} 
		else if (state == CharacterBeatenUp.CharacterState.Walking) {
			 
			isStand = false;
			//_animations[WalkAnimation].speed = Mathf.Clamp(controller.velocity.magnitude, 0, runMaxAnimationSpeed);
			_animations.CrossFade(WalkAnimation);	

		}
		else if( !isStand ) 
		{
			isStand = true;
			//_animations[WalkAnimation].speed = Mathf.Clamp(controller.velocity.magnitude, 0, runMaxAnimationSpeed);
			_animations.Play( "rig_mullet|Attack 1_Recover" ); // <<<< gambi da porra
			_animations[IdleAnimation].wrapMode = WrapMode.Loop;
			_animations.CrossFade( IdleAnimation );
		}
	}



	void Update()
	{
		// atacando - no chao e no ar
		if( Input.GetKey( KeyCode.O ) && !attacking ) 
		{
			isStand = false;

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
			iTween.ShakePosition( this.gameObject, new Vector3(0.4f,0,0), 0.6f );
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

		WaitForSeconds[] weakPunches = { new WaitForSeconds(0.1f), new WaitForSeconds(0.2f) };
		WaitForSeconds[] hardPunches = { new WaitForSeconds(0.5f), new WaitForSeconds(0.6f) };


		_animations.Play( AttackAnimations[ _animationCount ] );
		_animationCount++;

		// controla tempo de apariçao da bouding box
		yield return ( _animationCount >= AttackAnimations.Length ) ? hardPunches[0] : weakPunches[0];

		_punch.active = true;

		yield return ( _animationCount >= AttackAnimations.Length ) ? hardPunches[1] : weakPunches[1];

		_punch.active = false;
		_animationPlayed++;
		attacking = false;


		if( _animationPlayed >=  AttackAnimations.Length || !_punch.collided ) 
			_animationCount = _animationPlayed =  0;


		yield break;
	}



	/// <summary>
	/// Flying kick animation - when he's jumping.
	/// </summary>
	IEnumerator FlyingKick() 
	{
		attacking = true;
		_animations.Play( FlyingKickAnimation );

		yield return new WaitForSeconds( 0.1f );
		_punch.active = true;
		yield return new WaitForSeconds( 0.4f );

		_punch.active = false;
		attacking = false;
		yield break;
	}




	/// <summary>
	/// Animate him taking hit.
	/// </summary>
	IEnumerator GetHit()
	{
		hitted = true;
		_animations.Play( HitAnimation );
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
		_animations.Play( DieAnimation );
		yield return new WaitForSeconds( 2 );
		
		// nao cancela na real, mas para testes, sim
		dying = false;
		yield break;
	}







	public float animationCount {
		get { return _animationCount; }
	}


	public float animationPlayed {
		get { return _animationPlayed; }
	}

}