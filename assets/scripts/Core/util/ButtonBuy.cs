using UnityEngine;
using System.Collections;

public class ButtonBuy : Button {
	public bool is_purchased = false;
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

	public void buy(bool is_purchased){
		this.is_purchased = is_purchased;
		updateSprites();
	}

	void updateSprites(){
		if( ! is_purchased){
			clicked = clickedLocked;
			unClicked = unClickedLocked;
			myRenderer.sprite = unClicked;
		}else{
			clicked = clickedUnlocked;
			unClicked = unClickedUnlocked;
			myRenderer.sprite = unClicked;
		}
	}
	
}
