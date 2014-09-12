using UnityEngine;
using System.Collections;

public class SimpleButton : MonoBehaviour {
	public bool is_enabled = true;
	
	void Update () {
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
			
			//clicando
			if(Input.GetButton("click")) {
				clickBtnPress();
			}

			//desclicado
			if(Input.GetButtonUp("click")) {
				clickBtnUp();
			}								
		}else{
			//desclicado
			if(Input.GetButtonUp("click")) {
				clickBtnUp();
			}		
		}	

		//clicando fora
		if(Input.GetButton("click")) {
			clickBtnPressOut();
		}

		//atualiza frame
		OnUpdate();
	}

	public virtual void OnUpdate(){
		
	}

	public virtual void clickBtnPressOut(){
		
	}

	public virtual void clickBtnPress(){
		
	}

	public virtual void clickBtnUp(){
		
	}
	
	public virtual void clickBtnDown(){
		
	}
}
