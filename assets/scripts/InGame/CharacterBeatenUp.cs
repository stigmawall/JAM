using UnityEngine;
using System.Collections;

public class CharacterBeatenUp : MonoBehaviour {

	public Mordecai hero = null;


	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;

	public float x = 0.0F;
	public float z = 0.0F;

	public enum CharacterState {
		Idle,
		Walking,
		Trotting,
		Running,
		Jumping
	};

	CharacterState state = CharacterState.Idle;

	// Use this for initialization
	void Start () {
		hero = GetComponent<Mordecai> ();
	}
	
	// Update is called once per frame
	void Update () {
		x = 0.0F;
		z = 0.0F;

		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			Event e = Event.current;

			if (  Input.GetKey (KeyCode.W)) {
				z = speed;
				state = CharacterState.Walking;
			}

			if (Input.GetKey (KeyCode.S)) {
				z = speed*-1;
				state = CharacterState.Walking;
			}

			if (Input.GetKey (KeyCode.A)) {
				iTween.RotateTo(gameObject,iTween.Hash("y",-134.9937,"time",0.3f));
				x = speed*-1;
				state = CharacterState.Walking;
			}
		

			if (Input.GetKey (KeyCode.D)) {
				iTween.RotateTo(gameObject,iTween.Hash("y",58.022,"time",0.3f));
				x = speed;
				state = CharacterState.Walking;
			}

			moveDirection.x = x;
			moveDirection.z = z;


			if (Input.GetButton("Jump") ){
				moveDirection = Vector3.zero;
				moveDirection.y = jumpSpeed;
				state = CharacterState.Jumping;
			}

			hero.animateState(state);

		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		if( state == CharacterState.Walking ){
			state = CharacterState.Idle;
			moveDirection = Vector3.zero;
		}
	}

}
