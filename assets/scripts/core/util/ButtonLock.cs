using UnityEngine;
using System.Collections;

public class ButtonLock : Button {
	public bool is_locked = false;
	private Sprite clickedUnlocked = null;
	private Sprite unClickedUnlocked = null;
	public Sprite clickedLocked = null;
	public Sprite unClickedLocked = null;

	// Use this for initialization
	void Awake () {
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
		clickedUnlocked= clicked;
		unClickedUnlocked= unClicked;
		updateSprites();
		OnAwake();
	}

	public virtual void OnAwake(){

	}

	public void setLock(bool is_locked){
		this.is_locked = is_locked;
		updateSprites();
	}

	void updateSprites(){
		if( ! is_locked){
			clicked = clickedUnlocked;
			unClicked = unClickedUnlocked;
			myRenderer.sprite = unClicked;
		}else{
			clicked = clickedLocked;
			unClicked = unClickedLocked;
			myRenderer.sprite = unClicked;
		}
	}
	
}
