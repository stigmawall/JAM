using UnityEngine;
using System.Collections;

public class ButtonInGame : MonoBehaviour {

	public bool collided = false;

	public ControlDoorTwo doorTwoButtons = null;

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.layer == 8 )//"Player"
		{
			collided = true;
			//doorTwoButtons.updateActionDoor();
		}
	}

	void OnCollisionExit(Collision collision){
		if(collision.gameObject.layer == 8 )//"Player"
		{
			collided = false;
		}
	}

}
