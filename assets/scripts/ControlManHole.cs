using UnityEngine;
using System.Collections;

public class ControlManHole : MonoBehaviour {
	public enum Action {stop,open,close,opened};
	
	public GameObject target = null;	
	
	public float posOpen = 0f;

	public Action actionOnCollide = ControlManHole.Action.stop;
	
	protected Action action = ControlManHole.Action.stop;
	
	public float secondsCloseDoor = 1f; 
	
	protected float secondsWaitClose = 0f; 
	
	void Start(){
		//Debug.Log("chegou!!!");
	}

	void Close(){
		//fechando a porta
		if (action == ControlManHole.Action.close) {
			
			//posDefault = Mathf.Lerp(posDefault,posMin,Time.deltaTime);
			
			if(target){
				Hashtable ht = new Hashtable();
				ht.Add("x", posOpen*-1);
				ht.Add("oncomplete", "OnClose");
				ht.Add("oncompletetarget", gameObject);
				iTween.MoveAdd(target, ht );
				
			} 
		}	
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.layer == 8 )//"Player"
		{
			action = actionOnCollide;

			//Debug.Log (action);
			//abre porta
			if(action == Action.open ){
				if(target){
					Hashtable ht = new Hashtable();
					ht.Add("x", posOpen);
					ht.Add("oncomplete", "OnOpened");
					ht.Add("oncompletetarget", gameObject);
					iTween.MoveAdd(target, ht );
					
				} 
			}	

			Close ();
		}
	}

	void OnOpened(){
		action = Action.opened;
		secondsWaitClose = secondsCloseDoor;
		//Debug.Log(action);
	}

	void OnClose(){
		action = Action.close;
		//Debug.Log(action);
	}

	void Update () {
		//tempo para fechar a porta
		if (action == ControlManHole.Action.opened) {
			secondsWaitClose = secondsWaitClose - Time.deltaTime;
			//Debug.Log(secondsWaitClose);

			if(secondsWaitClose <= 0){

				action = ControlManHole.Action.close;
				Close ();

			}
		}
	}
	
}
