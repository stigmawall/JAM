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

	public int index = 0;

	public bool walking;

	public bool attacking;

	public bool hitted;

	public bool dying;

	public Status status;
	



	Animation _animations;

	EnemyPunchTrigger _punch;

	int _animationCount;

	int _animationPlayed;

	bool disable = false;

	public ControlEnemy controlEnemy; 

	void Start() 
	{
		walking = attacking = hitted = dying = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		status = GetComponent<Status>();
		_punch = GetComponentInChildren<EnemyPunchTrigger>();



	}

	void Update()
	{
		if( disable == true ){
			return;
		}
		// control animations
		if( dying ) {
			controlEnemy.dieEnemy(index);
			_animations.CrossFade( DieAnimation );
			disable = true;
			return;
		} 
		
		else if( hitted ) {
			_animations.CrossFade( HitAnimation );
			return;
		}
		
		else if( walking ) {
			_animations.CrossFade( WalkAnimation );
		}
		
		else if( attacking ) {
			_animations.CrossFade( HitAnimation );
		}
		
		else {
			_animations.CrossFade( IdleAnimation );
		}


		// verifica a distancia em que estao, ignorando o Y
		Vector3 dir = target.position - transform.position;
		dir.y = 0; 
		//transform.LookAt( target );
		if( dir.x < 0 ) transform.LookAt( Vector3.forward );
		else transform.LookAt( Vector3.back );


		if (dir.magnitude > distance )
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, 0.06f);
			walking = true;
			//transform.position = Vector3.forward * 3 * Time.deltaTime;
		} else {
			walking = false;
			if( !attacking || !hitted || !dying )
				StartCoroutine( Punch() );
		}

	}





	// BEHAVIOR CONTROLLER
	IEnumerator Punch() 
	{
		attacking = true;

		_animations.CrossFade( AttackAnimations[ 0 ] );
		_animationCount++;

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





	// STATUS CONTROLLERS
	public void TakeDamage( float damage ) 
	{
		// marca o dano
		status.HP -= damage;
		
		if( status.HP <= 0 ) 
		{
			StartCoroutine( Dying() );
		} else {
			StartCoroutine( GetHit() );
		}
		
		// imprime o valor atual
		Debug.Log ( "DAMAGE ENEMY - " + status.HP );
	}



	
	public void Heal( float value ) 
	{
		status.HP += value;
		if( status.HP >= status.MAXHP ) status.HP = status.MAXHP;
		Debug.Log ( "HEAL ENEMY - " + status.HP );
	}




	/// <summary>
	/// Animate him taking hit.
	/// </summary>
	IEnumerator GetHit()
	{
		hitted = true;
		_animations.CrossFade( HitAnimation );
		yield return new WaitForSeconds( 1 );
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
		yield return new WaitForSeconds( 3 );

		Destroy( this.gameObject );
		//// nao cancela na real, mas para testes, sim
		//dying = false;
		yield break;
	}



}
