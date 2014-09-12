using UnityEngine;
using System.Collections;

public class ControlDoor : MonoBehaviour {
	public enum Action {stop,open,close,opened};

	public GameObject target = null;	
	
	public float posMax = 0f;

	protected float posDefault = 0f;

	public float posMin = 0f;

	public Action actionOnCollide = Action.stop;

	protected Action action = Action.stop;

	public float secondsCloseDoor = 1f; 

	protected float secondsWaitClose = 0f; 

	void Start(){
		posDefault = posMin; 
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.layer == 8 )//"Player"
		{
			action = actionOnCollide;
		}
	}

	void Update () {
		//Debug.Log (action);
		//abre porta
		if(action == Action.open ){
			posDefault = Mathf.Lerp(posDefault,posMax,Time.deltaTime);
			if(target){
				target.transform.position = new Vector3(
					target.transform.position.x,
					posDefault,
					target.transform.position.z
				);
				if(target.transform.position.y >= posMax-0.01f){
					action = Action.opened;
					secondsWaitClose = secondsCloseDoor;
				}
			} 
		}	

		//tempo para fechar a porta
		if (action == Action.opened) {
			secondsWaitClose = secondsWaitClose - Time.deltaTime;

			if(secondsWaitClose <= 0){
				action = Action.close;
			}
		}

		//fechando a porta
		if (action == Action.close) {
			posDefault = Mathf.Lerp(posDefault,posMin,Time.deltaTime);
			if(target){
				target.transform.position = new Vector3(
					target.transform.position.x,
					posDefault,
					target.transform.position.z
					);
				if(target.transform.position.y <= posMin+0.01f){
					action = Action.stop;
				}
			} 
		}	

	}
	
}
