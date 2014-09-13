using UnityEngine;
using System.Collections;

public class ControlEnemy : MonoBehaviour {
	enum Action {Actived,NoMore,WaintingHero};

	public ControlCamera controlCamera = null;

	public GameObject target = null;

	public GameObject blockedLeft = null;
	
	public int level = 1;

	public int count = 1;

	public float secondsDefault = 5;

	float seconds = 0;

	ControlEnemy.Action action = ControlEnemy.Action.WaintingHero;

	int index = 0;

	GameObject [] enemy;

	void setVisible(int index,bool visible){
		Transform t = enemy[index].transform.Find("Mordecai.001");
		t.gameObject.renderer.enabled = false;
	}

	void setTarget(int index,Transform t){
		Enemy e = enemy[index].GetComponent<Enemy>();
		e.target = t;
	}

	void OnTriggerEnter (Collider other) {
		if (action == ControlEnemy.Action.NoMore) {
			return;
		}
		if (action != ControlEnemy.Action.WaintingHero) {
			return;
		}

		if (other.gameObject.layer != 9) {
			return;
		}

		index = 0;
		enemy = new GameObject [count];
		for(int i=0;i<count;i++){
			//todo: ver como desabilitar a IA
			enemy[i] = (GameObject)Instantiate(Resources.Load("inimigo", typeof(GameObject)));
			Debug.Log (enemy[i]);

			setVisible(index,false);

			setTarget(index,target.transform);
		}
		nextEnemy();
		action = ControlEnemy.Action.Actived;
	}

	void Update(){
		if (action == ControlEnemy.Action.WaintingHero) {
			return;
		}

		/*
		if (level == 1) {
			seconds = seconds - Time.deltaTime;
			if( seconds <= 0 ){
				if( nextEnemy() == false ){
					//todo: ver como desabilitar ao inves de destruir
					Destroy(blockedLeft);
					controlCamera.isFollow = false;
					action = ControlEnemy.Action.NoMore;
				}
				seconds = secondsDefault;
			}
		}
		*/
	}

	bool nextEnemy(){
		if (index >= count) {
			return false;
		}

		setVisible(index,true);

		enemy [index].transform.position = new Vector3 (
			target.transform.position.x+5f,
			target.transform.position.y,
			target.transform.position.z
		);

		index ++;

		return false;
	}

	void dieEnemy(int index){

		enemy [index] = null;

		count--;

		if (count <= 0) {

			Destroy (blockedLeft);

		}
	}

}
