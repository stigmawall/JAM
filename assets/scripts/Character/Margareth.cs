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

	public bool dead;

	public bool invulnerable;



	Animation _animations;

	Status _status;

	bool stand;



	void Start() 
	{
		walking = hitted = stand = dying = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		_status = GetComponent<Status>();
		//_controller = (CharacterController)GetComponent(typeof(CharacterController));
	}
	

	float limit = -2.984886f;

	void Update()
	{
		if( Time.timeScale == 0 || dead || dying ){
			return;
		}

		// verifica a distancia em que estao, ignorando o Y
		Vector3 dir = target.position - transform.position;
		dir.y = 0; 

		
		if( dir.x < 0 ) 
			iTween.RotateTo(gameObject,iTween.Hash("y",-90,"time",0.1f));
		else 
			iTween.RotateTo(gameObject,iTween.Hash("y",90,"time",0.1f));

		//transform.LookAt( targetWithoutY );
		//Debug.Log (target.position.y);



		if (dir.magnitude > 5 && (target.position.y >= limit))
		{
			/*
			Vector3 pos = new Vector3(
				target.position.x,
				target.position.y,
				limit
			);

			transform.position = Vector3.MoveTowards(transform.position, pos, 0.06f);
			*/

			transform.position = Vector3.MoveTowards(transform.position, target.position, 0.06f);
			walking = true;
			//transform.position = Vector3.forward * 3 * Time.deltaTime;
		} else {
			walking = false;
		}



		
		if( hitted ) {
			_animations.Play( HitAnimation );
			return;
		}

		else if( walking ) {
			_animations.Play( WalkAnimation );
		}

		else if( dying ) {
			_animations.Play( DieAnimation );
		}

		else {
			if( !stand ) {
				_animations.Play( "Fix" );
			}
			stand = true;
			_animations.Play( IdleAnimation );
		}


	}






	// STATUS CONTROLLERS
	public void TakeDamage( float damage ) 
	{
		if( invulnerable ) return;

		// marca o dano
		_status.Damage(damage);
		
		if( _status.HP <= 0 ) 
		{
			StartCoroutine( Dying() );
		} else {
			StartCoroutine( GetHit() );
		}
		
		// imprime o valor atual
		HUDController.instance.UpdateMaggieLifebarInfo( _status.HP, _status.MAXHP );
		//Debug.Log ( "DAMAGE MAGGIE - " + _status.HP );
	}
	



	public void Heal( float value ) 
	{
		_status.Heal(value);
		if( _status.HP >= _status.MAXHP ) _status.HP = _status.MAXHP;
	}





	// mostra o personagem caindo e ganhando invulnerabilidade por um tempo
	public void Fall() 
	{
		invulnerable = true;
		StartCoroutine( BlinkChar() );
	}
	
	
	
	
	// pisca o personagem e retorna de ser invulneravel ao termino
	IEnumerator BlinkChar() 
	{
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		yield return new WaitForSeconds( 0.2f );
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		yield return new WaitForSeconds( 0.2f );
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		yield return new WaitForSeconds( 0.2f );		
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		yield return new WaitForSeconds( 0.2f );
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		yield return new WaitForSeconds( 0.2f );
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		invulnerable = false;
	}



	/// <summary>
	/// Animate him taking hit.
	/// </summary>
	IEnumerator GetHit()
	{
		hitted = true;
		_animations.CrossFade( HitAnimation );
		yield return new WaitForSeconds( 0.5f );
		hitted = false;
		_animations.Play( "rig_margaret|Run_Margaret" );
		yield return new WaitForSeconds( 0.01f );
		yield break;
	}



	/// <summary>
	/// Animate the enemy to die
	/// na real ele vai voar longe
	/// </summary>
	IEnumerator Dying()
	{
		dying = true;
		_animations.CrossFade( DieAnimation );
		yield return new WaitForSeconds( 2 );
		Destroy( this.gameObject );

		// chama um gameover daqui
		yield break;
	}



}