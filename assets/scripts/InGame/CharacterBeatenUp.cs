using UnityEngine;
using System.Collections;

public class CharacterBeatenUp : MonoBehaviour {

	public Mordecai hero = null;


	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	public float x = 0.0F;
	public float z = 0.0F;


	private Vector3 moveDirection = Vector3.zero;


	bool antGround = false;


	public enum CharacterState {
		Idle,
		Walking,
		Jumping
	};

	CharacterState state = CharacterState.Idle;

	// Use this for initialization
	void Start () {
		hero = GetComponent<Mordecai> ();
	}

	public void superJump(float force){
		if( hero.dying || hero.hitted || !hero.controlling ) return;

		CharacterController controller = GetComponent<CharacterController>();

		x = 0.0F;
		z = 0.0F;
		state = CharacterState.Walking;
		
		moveDirection.y = force;
		//state = CharacterState.Jumping;

		hero.animateState(state);

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( hero.dying || hero.hitted || !hero.controlling ) return;

		x = 0.0F;
		z = 0.0F;

		CharacterController controller = GetComponent<CharacterController>();



		if (controller.isGrounded && !hero.attacking ) //esta no chao
		{
			Event e = Event.current;
			state = CharacterState.Idle;


			if (  Input.GetKey (KeyCode.W)) {
				z = speed;
				state = CharacterState.Walking;
			}

			if (Input.GetKey (KeyCode.S)) {
				z = speed*-1;
				state = CharacterState.Walking;
			}

			if (Input.GetKey (KeyCode.A)) {
				iTween.RotateTo(gameObject,iTween.Hash("y",-90,"time",0.1f));
				x = speed*-1;
				state = CharacterState.Walking;
			}
		

			if (Input.GetKey (KeyCode.D)) {
				iTween.RotateTo(gameObject,iTween.Hash("y",90,"time",0.1f));
				x = speed;
				state = CharacterState.Walking;
			}

			moveDirection.x = x;
			moveDirection.z = z;


			if ( Input.GetButton("Jump") )
			{
				moveDirection.y = jumpSpeed;
				state = CharacterState.Jumping;
			}

			hero.animateState(state);
		} 


		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);


		if( state == CharacterState.Walking ) 
		{
			hero.isStand = false;
			state = CharacterState.Idle;
			moveDirection = Vector3.zero;
		}
	}


}
