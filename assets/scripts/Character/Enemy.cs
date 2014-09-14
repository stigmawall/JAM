using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{

	public Transform target;



	public string name = "Unicorn";

	private SpriteRenderer picture;

	public string pictureResource = "";

	public string IdleAnimation;

	public string WalkAnimation;

	public string HitAnimation;

	public string DieAnimation;

	public string[] AttackAnimations;
	
	public float distance = 1;

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
	 	//GameObject go = (GameObject)Instantiate(Resources.Load(pictureResource));
		//picture = go.GetComponent<SpriteRenderer>();
		//SpriteRenderer picture

		walking = attacking = hitted = dying = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		status = GetComponent<Status>();
		_punch = GetComponentInChildren<EnemyPunchTrigger>();
		_punch.active = false;
	}




	void Update()
	{
		if( disable == true || Time.timeScale == 0 ){
			return;
		}


		// control animations
		if( !attacking ) 
		{
			if( dying ) 
			{
				if( controlEnemy ) 
					controlEnemy.dieEnemy(index);

				//_animations.CrossFade( DieAnimation );
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
			else {
				_animations.CrossFade( IdleAnimation );
			}
		}

		if (! target) {
			iTween.RotateTo(gameObject,iTween.Hash("y",-90,"time",0.1f));
			return;
		}
		// verifica a distancia em que estao, ignorando o Y
		Vector3 dir = target.position - transform.position;
		dir.y = 0; 
		//transform.LookAt( target );

		if( dir.x < 0 ) 
			iTween.RotateTo(gameObject,iTween.Hash("y",-90,"time",0.1f));
		else 
			iTween.RotateTo(gameObject,iTween.Hash("y",90,"time",0.1f));



		if ( dir.magnitude > distance )
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, 0.06f);
			walking = true;
			//transform.position = Vector3.forward * 3 * Time.deltaTime;
		} else 
		{
			walking = false;
			if( !attacking && !hitted && !dying ) 
			{
				bool inv;

				if( target.GetComponent<Mordecai>() ) {
					inv = 	target.GetComponent<Mordecai>().invulnerable || 
							target.GetComponent<Mordecai>().dying || 
							target.GetComponent<Mordecai>().dead;
				}
				else { 
					inv = 	target.GetComponent<Margareth>().invulnerable || 
							target.GetComponent<Mordecai>().dying || 
							target.GetComponent<Mordecai>().dead;
				}


				if(!inv)
					StartCoroutine( Punch() );
			}
		}

	}





	// BEHAVIOR CONTROLLER
	IEnumerator Punch() 
	{
		attacking = true;

		WaitForSeconds[] weakPunches = { new WaitForSeconds(0.1f), new WaitForSeconds(0.2f) };
		WaitForSeconds[] hardPunches = { new WaitForSeconds(0.5f), new WaitForSeconds(0.6f) };

		//_animations[ AttackAnimations[ _animationCount ] ].speed = 1.5f;
		_animations.CrossFade( AttackAnimations[ _animationCount ] );
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





	// STATUS CONTROLLERS
	public void TakeDamage( float damage ) 
	{
		// marca o dano
		status.Damage(damage);
		
		if( status.HP <= 0 ) 
		{
			StartCoroutine( Dying() );
		} else {
			StartCoroutine( GetHit() );
		}

		HUDController.instance.UpdateEnemyLifebarInfo( status.HP, status.MAXHP, picture, name );
		// imprime o valor atual
		//Debug.Log ( "DAMAGE ENEMY - " + status.HP );
	}



	
	public void Heal( float value ) 
	{
		status.Heal(value);
		if( status.HP >= status.MAXHP ) status.HP = status.MAXHP;

		HUDController.instance.UpdateEnemyLifebarInfo( status.HP, status.MAXHP, picture, name );
		//Debug.Log ( "HEAL ENEMY - " + status.HP );
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
	/// Animate the enemy to die
	/// na real ele vai voar longe
	/// </summary>
	IEnumerator Dying()
	{
		dying = true;
		//_animations.CrossFade( DieAnimation );
		yield return new WaitForSeconds( 0.5f );
		Destroy( this.gameObject );
		yield break;
	}




	public int animationCount {
		get { return _animationCount; }
	}

}
