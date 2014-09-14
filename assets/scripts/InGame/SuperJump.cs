using UnityEngine;
using System.Collections;

public class SuperJump : MonoBehaviour {
	public float jumpSpeed = 100;
	public int type = 1;
	private GameObject target = null;
	public Transform p1 = null;
	public Transform p2 = null;

	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 9) {
			target = col.gameObject;
			if(type==1){
				jumpLeft();
			}
		}
	}

	void jumpLeft(){
		//CharacterBeatenUp cb = col.gameObject.GetComponent<CharacterBeatenUp>();
		//cb.superJump( jumpSpeed );
		Hashtable ht = new Hashtable();
		ht.Add("x", p1.position.x);
		ht.Add("y", p1.position.y);
		ht.Add("z", p1.position.z);
		ht.Add("time", 0.5f);
		ht.Add("easetype", "linear");
		ht.Add("oncomplete", "left");
		ht.Add("oncompletetarget", gameObject);
		iTween.MoveTo(target, ht );
	}

	void left(){

		Hashtable ht = new Hashtable();
		ht.Add("x", p2.position.x);
		ht.Add("y", p2.position.y);
		ht.Add("z", p2.position.z);
		ht.Add("easetype", "linear");
		ht.Add("time", 0.5f);
		iTween.MoveTo(target, ht );
	}

}
