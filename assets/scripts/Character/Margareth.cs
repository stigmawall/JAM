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





	void Start() 
	{
		walking = hitted = dying = false;
		_animations = (Animation)GetComponent(typeof(Animation));
		//_controller = (CharacterController)GetComponent(typeof(CharacterController));
	}
	



	void Update()
	{
		// verifica a distancia em que estao, ignorando o Y
		Vector3 dir = target.position - transform.position;
		dir.y = 0; 
		transform.LookAt( target );

		if (dir.magnitude > 5)
		{
			transform.position = Vector3.MoveTowards(transform.position, target.position, 0.06f);
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


}