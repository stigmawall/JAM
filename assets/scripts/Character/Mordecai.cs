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


	// managing assist effect

	public SpriteRenderer AssistSprite;

	public MeshRenderer BlackMood;



	public bool attacking;

	public bool hitted;

	public bool dying;

	public bool dead;

	public bool assisted;

	public bool controlling;

	public bool invulnerable;




	CharacterController _controller;

	Animation _animations;

	PunchTrigger _punch;

	Status _status;

	int _animationCount;

	int _animationPlayed;

	public bool isStand;


	void Start() 
	{
		controlling = true;
		attacking = hitted = dying = dead = false;
		_animationCount = _animationPlayed = 0;
		isStand = true;
		_animations = (Animation)GetComponent(typeof(Animation));
		_controller = (CharacterController)GetComponent(typeof(CharacterController));
		_status = GetComponent<Status>();
		_punch = GetComponentInChildren<PunchTrigger>();
		_punch.active = false;

		MeshRenderer mr = BlackMood.GetComponent<MeshRenderer>();
		mr.renderer.enabled = false;

		SpriteRenderer bm = AssistSprite.GetComponent<SpriteRenderer>();
		bm.renderer.enabled = false;
		//BlackMood;
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
		// Se estiver morrendo ou sem controle, nada faz
		if( dying || hitted || !controlling ) return;


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


		// chamando o assist 
		if( Input.GetKeyDown( KeyCode.P ) && HUDController.instance.canUseAssist && !assisted ) 
		{
			// estado assistencia
			assisted = true;

			// congela o tempo
			Time.timeScale = 0;



			MeshRenderer mr = BlackMood.GetComponent<MeshRenderer>();
			mr.renderer.enabled = true;
			
			SpriteRenderer bm = AssistSprite.GetComponent<SpriteRenderer>();
			bm.renderer.enabled = true;



			// fundo preto
			Vector3 bp = BlackMood.gameObject.transform.localPosition;
			bp.x = -5;
			BlackMood.gameObject.transform.localPosition = bp;
			iTween.FadeTo( BlackMood.gameObject, 0, 0.01f );
			iTween.FadeTo( BlackMood.gameObject, 0.4f, 1 );


			// anima o sprite
			iTween.MoveTo( AssistSprite.gameObject, 
			              	iTween.Hash( "x", 20, "islocal", true,
			            				"time", 1.4f, "easetype", iTween.EaseType.linear,
			            				"onComplete", "CalculateDamageAndReturnAssist",
			            				"ignoretimescale", true,
			            				"onCompleteTarget", gameObject ) );
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



	// retorna o assist a posiçao original e reinicia o carregamento
	public void CalculateDamageAndReturnAssist() 
	{
		// volta o tempo
		Time.timeScale = 1;

		// fundo preto
		Vector3 bp = BlackMood.gameObject.transform.localPosition;
		bp.x = -20;
		BlackMood.gameObject.transform.localPosition = bp;

		// retorna o sprite
		iTween.MoveTo( AssistSprite.gameObject, iTween.Hash( "x", -20, "time", 0.01f ) );
		assisted = false;

		//reativa a barra de assist
		HUDController.instance.StartAssist();

		// desejo a todas inimigas vida longa #sqn
		HitAllEnemies( 150, 80 );


		// remove visibilidade... tipo Barcos
		MeshRenderer mr = BlackMood.GetComponent<MeshRenderer>();
		mr.renderer.enabled = false;
		
		SpriteRenderer bm = AssistSprite.GetComponent<SpriteRenderer>();
		bm.renderer.enabled = false;
	}






	// STATUS CONTROLLERS
	public void TakeDamage( float damage ) 
	{
		if( invulnerable ) return;

		// marca o dano
		_status.Damage( damage );

		if( _status.HP <= 0 ) 
		{
			StartCoroutine( Dying() );
		} else {
			StartCoroutine( GetHit() );
			iTween.ShakePosition( transform.FindChild("rig_mullet").gameObject, new Vector3(0.2f,0,0), 0.3f );
		}

		// imprime o valor atual
		//Debug.Log ( "DAMAGE - " + _status.HP );

		// atualiza a hud
		HUDController.instance.UpdateLifebarInfo( _status.HP, _status.MAXHP );
	}





	public void Heal( float value ) 
	{

		_status.Heal( value );

		// atualiza a hud
		HUDController.instance.UpdateLifebarInfo( _status.HP, _status.MAXHP );
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
		dead = true;
		_animations.Play( DieAnimation );
		yield return new WaitForSeconds( 1 );

		// pisca o jogador caido
		//this.renderer.enabled = false;
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		yield return new WaitForSeconds( 0.2f );
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		yield return new WaitForSeconds( 0.2f );
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		yield return new WaitForSeconds( 0.2f );		
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		yield return new WaitForSeconds( 0.2f );
		GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;


		// caso tenha mais vidas, subtrai uma e volta a vida
		if( HUDController.instance.lives>0 ) 
		{

			GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;

			// remove uma bola da tela
			Destroy( GameObject.Find ("HUD_live" + HUDController.instance.lives).gameObject );
			HUDController.instance.lives--;

			// coloca o personagem pra cair na tela
			// para a camera por uns instantes
			Vector3 p = transform.position;
			p.y = 12;
			transform.position = p;
			dying = false;
			Heal( _status.MAXHP );


			// depois de um tempo, volta
			yield return new WaitForSeconds( 1 );

			// revive
			animateState( CharacterBeatenUp.CharacterState.Jumping );
			dead = false;

			// causa dano  e treme
			HitAllEnemies( 20, 20 );
			iTween.ShakePosition( this.gameObject, new Vector3(0.6f,0), 0.3f );

		} else {

			Debug.Log ("GAME OVER");
		}


		//lives

		
		// nao cancela na real, mas para testes, sim
		//dying = false;
		yield break;
	}




	// desejo a todas inimigas vida longa #sqn
	public void HitAllEnemies( float damage, float push ) {

		// teste - para ver o que fazer
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach( GameObject ene in enemies ) 
		{
			Enemy e = ene.GetComponent<Enemy>();
			e.TakeDamage( 150 );
			
			// morreu? voa - odeio repetir, mas nao vi outra forma :/
			if( e.status.HP <= 0 )
			{
				Vector3 vec = e.transform.position;
				vec -= transform.position;
				vec += Vector3.up;
				vec.Normalize();
				vec *= push;
				e.rigidbody.AddForce(vec,ForceMode.Impulse);
			}
		}
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




	public float animationCount {
		get { return _animationCount; }
	}


	public float animationPlayed {
		get { return _animationPlayed; }
	}

}