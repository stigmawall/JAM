using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{

	public Transform target;



	public string IdleAnimation;

	public string WalkAnimation;

	public string HitAnimation;

	public string DieAnimation;

	public string[] AttackAnimations;
	
	public int distance = 1;



	public bool walking;

	public bool attacking;

	public bool hitted;

	public bool dying;

	

	Animation _animations;

	Status _status;

	int _animationCount;

	int _animationPlayed;



	void Start() 
	{
		walking = attacking = hitted = dying = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		_status = GetComponent<Status>();
	}
	



	void Update()
	{
		// verifica a distancia em que estao, ignorando o Y
		Vector3 dir = target.position - transform.position;
		dir.y = 0; 
		transform.LookAt( target );


		if (dir.magnitude > distance && !hitted && !dying && !attacking )
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, 0.06f);
			walking = true;
			//transform.position = Vector3.forward * 3 * Time.deltaTime;
		} else {
			walking = false;
			if( !attacking )
				StartCoroutine( Punch() );
		}
		

		// control animations
		if( attacking ) {
			_animations.CrossFade( HitAnimation );
		}

		else if( hitted ) {
			_animations.CrossFade( HitAnimation );
		}

		else if( walking ) {
			_animations.CrossFade( WalkAnimation );
		}

		else if( dying ) {
			_animations.CrossFade( DieAnimation );
		}

		else {
			_animations.CrossFade( IdleAnimation );
		}

	}





	// BEHAVIOR CONTROLLER
	IEnumerator Punch() 
	{
		attacking = true;

		_animations.CrossFade( AttackAnimations[ _animationCount ] );
		_animationCount++;
		yield return new WaitForSeconds( 0.4f );
		
		_animationPlayed++;
		attacking = false;
		
		if( _animationPlayed >=  AttackAnimations.Length ) 
			_animationCount = _animationPlayed =  0;
		
		
		yield break;
	}





	// STATUS CONTROLLERS
	public void TakeDamage( float damage ) 
	{
		// marca o dano
		_status.HP -= damage;
		
		if( _status.HP <= 0 ) 
		{
			//StartCoroutine( Dying() );
		} else {
			
			//StartCoroutine( GetHit() );
		}
		
		// imprime o valor atual
		Debug.Log ( "DAMAGE MAGGIE - " + _status.HP );
	}



	
	public void Heal( float value ) 
	{
		_status.HP += value;
		if( _status.HP >= _status.MAXHP ) _status.HP = _status.MAXHP;
		Debug.Log ( "HEAL MAGGIE - " + _status.HP );
	}



}
