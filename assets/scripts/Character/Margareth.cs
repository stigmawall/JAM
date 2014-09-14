using UnityEngine;
using System.Collections;

public class Margareth : MonoBehaviour 
{

	public Transform target;

	public string IdleAnimation;

	public string WalkAnimation;

	public string HitAnimation;

	public string DieAnimation;
	
	public bool walking;

	public bool hitted;

	public bool dying;

	Animation _animations;

	Status _status;

	void Start() 
	{
		walking = hitted = dying = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		_status = GetComponent<Status>();
		//_controller = (CharacterController)GetComponent(typeof(CharacterController));
	}
	

	float limit = -2.984886f;

	void Update()
	{
		if( Time.timeScale == 0 ){
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

		//transform.LookAt( targetWithoutY );
		//Debug.Log (target.position.y);



		if (dir.magnitude > 5 && (target.position.y >= limit))
		{
			Vector3 pos = new Vector3(
				target.position.x,
				target.position.y,
				limit
				);

			//target.position

				transform.position = Vector3.MoveTowards(transform.position, pos, 0.06f);
				walking = true;
			//transform.position = Vector3.forward * 3 * Time.deltaTime;
		} else {
			walking = false;
		}
		
		
		
		if( hitted ) {
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