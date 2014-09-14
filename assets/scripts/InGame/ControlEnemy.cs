using UnityEngine;
using System.Collections;

public class ControlEnemy : MonoBehaviour {
	enum Action {Actived,NoMore,WaintingHero};

	public ControlCamera controlCamera = null;

	public GameObject target = null;

	public GameObject blockedLeft = null;

	public SpriteRenderer hud = null;

	public int level = 1;

	public int count = 1;

	public float secondsDefault = 5;

	float seconds = 0;

	ControlEnemy.Action action = ControlEnemy.Action.WaintingHero;

	int index = 0;

	GameObject [] enemy;

	void setVisible(int index,bool visible){
		Transform t = enemy[index].transform.Find("Mordecai.001");
		t.gameObject.renderer.enabled = visible;
	}

	void setTarget(int index,Transform t){
		Enemy e = enemy[index].GetComponent<Enemy>();
		e.target = t;
	}

	void setControl(int index){
		Enemy e = enemy[index].GetComponent<Enemy>();
		e.controlEnemy = GetComponent<ControlEnemy>();
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
			enemy[i] = (GameObject)Instantiate(Resources.Load("inimigo", typeof(GameObject)));

			setControl(index);
			//setHud(index);
			setVisible(index,false);
			//index
			setTarget(index,target.transform);
		}
		nextEnemy();
		action = ControlEnemy.Action.Actived;
	}

	/*
	void setHud(int index){
		Enemy enemy = enemy[index].GetComponent<Enemy>();
		enemy.picture = hud.material.mainTexture;
	}
	*/


	void Update(){
		if (action == ControlEnemy.Action.NoMore) {
			return;
		}
		if (action == ControlEnemy.Action.WaintingHero) {
			return;
		}





		if (level == 1) {
			seconds = seconds - Time.deltaTime;
			//Debug.Log(seconds);
			if( seconds <= 0 ){
				if( nextEnemy() == false ){
					//todo: ver como desabilitar ao inves de destruir
					//Debug.Log("destroy");
					//Destroy(blockedLeft);
					StopWall sw = blockedLeft.transform.GetComponent("StopWall") as StopWall;
					sw.enabled = false;
					controlCamera.isFollow = true;
					action = ControlEnemy.Action.NoMore;
				}
				seconds = secondsDefault;
			}
		}

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

	public void dieEnemy(int index){

		enemy [index] = null;

		count--;

		if (count <= 0) {

			Destroy (blockedLeft);

		}
	}

}
