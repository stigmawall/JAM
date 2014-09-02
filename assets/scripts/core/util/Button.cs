using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]
public class Button : MonoBehaviour {
	public AudioSource audioClick = null;
	public AudioSource audioSelect = null;
	public Sprite clicked = null;
	public Sprite unClicked = null;
	public float timeSleep = 1;
	protected SpriteRenderer myRenderer;
	protected bool oncePlay = true;
	public bool is_enabled = true;

	// Use this for initialization
	void Awake () {	
		initializeButton();
	}
	
	public void enableButton(bool is_enabled){
		oncePlay = true;
		this.is_enabled = is_enabled;
	}
	
	protected void initializeButton(){
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	private void run(){
		StartCoroutine(wait());
	}
	
	private IEnumerator wait(){
		if(audioSelect){
			if( ! audioSelect.isPlaying ){
				audioSelect.Play();
			}
		}
		yield return new WaitForSeconds( timeSleep );
		clickBtn();
	}

	protected void UpdateButton(){
		if(! is_enabled){
			return;
		}
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
		if(hit.collider != null && hit.collider.transform == transform)
		{
			//clicado

			if(Input.GetButtonDown("click")) {
				clickBtnDown();
			}

			if(Input.GetButton("click")) {
				if(clicked){
					myRenderer.sprite = clicked;
				}
				
				if(audioClick && oncePlay){
					oncePlay = false;
					if( ! audioClick.isPlaying ){
						audioClick.Play();
					}
				}
			}
			
			//desclicado
			if(Input.GetButtonUp("click")) {
				if(unClicked){
					myRenderer.sprite = unClicked;
				}
				
				run();
			}				
		}else{
			//desclicado
			if(Input.GetButtonUp("click")) {
				clickBtnUp();
				if(unClicked){
					myRenderer.sprite = unClicked;
				}
			}		
			
			oncePlay = true;
		}
	}

	// Update is called once per frame
	void Update () {
		UpdateButton();
	}

	public virtual void clickBtn(){
		
	}

	public virtual void clickBtnUp(){

	}

	public virtual void clickBtnDown(){
		
	}
}
