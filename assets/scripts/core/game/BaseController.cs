using UnityEngine;
using System.Collections;

/*
 * Classe base responsavel por gerenciar as regras de negocio
 * 
 */
public class BaseController : MonoBehaviour {

	public virtual void nextLevel(){
		
	}

	public IEnumerator sleepGame(){
		yield return new WaitForSeconds(3f);
		nextLevel();
	}

}
