using UnityEngine;
using System.Collections;

public class DragObject : SimpleButton {
	private static GameObject dragged = null;
	public static GameObject last_dragged = null;
	private Vector3 offset = new Vector3();

	public override void clickBtnPressOut(){
		if( DragObject.dragged ){
			DragObject.dragged.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f)) + new Vector3(offset.x, offset.y, 0.0f);
			DragObject.dragged.transform.position = new Vector3(DragObject.dragged.transform.position.x, dragged.transform.position.y, -3.0f);
			draggin();
		}
	}



	public override void clickBtnUp(){
		draggout();
		DragObject.dragged = null;
	}

	public static void setDragged(GameObject d){
		DragObject.dragged = d;
		DragObject.last_dragged = d;
	}

	public static GameObject getDragged(){
		return DragObject.dragged;
	}

	public override void clickBtnDown(){
		if(dragged == null){
			setDragged(gameObject);
			dragFinded();
			offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0.0f));
			offset.Set( offset.x, offset.y, -3);
		}
	}

	public virtual void draggin(){

	}

	public virtual void draggout(){
		
	}

	public virtual void dragFinded(){
		
	}

}