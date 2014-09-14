using UnityEngine;
using System.Collections;

public class HUDController : MonoBehaviour {


	// simgleton para controle da HUD geral
	// possui uma chamada para os Status chamarem e atualizarem suas infos

	private static HUDController _instance;


	
	public SpriteRenderer lifebar;

	public SpriteRenderer assistbar;

	public float AssistMaxTime = 5;

	public bool canUseAssist = false;

	
	bool _assistActive;

	float _assistTimeCount;



	public static HUDController instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<HUDController>();
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	


	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance) Destroy(this.gameObject);
		}

		StartAssist();
	}




	void Update()
	{
		// controla a barra de assist se tiver ativada e nao cheia
		if( _assistActive && !canUseAssist ) 
		{
			_assistTimeCount += Time.deltaTime;

			// calcula valor em % para encher a barra
			Vector3 pos = assistbar.gameObject.transform.localScale;
			pos.x = _assistTimeCount / AssistMaxTime;
			if( pos.x > 1 ) pos.x = 1;

			assistbar.gameObject.transform.localScale = pos;

			if( _assistTimeCount >= AssistMaxTime ) {
				canUseAssist = true;
			}
		}
	}




	/****************** Funçoes *******************/



	// inicia contagem do tempo e aumento da barra
	public void StartAssist() {

		_assistActive = true;
		_assistTimeCount = 0;
	}



	public void UpdateLifebarInfo( float hp, float max ) 
	{
		// caso esqueça de setar o lifebar
		if(lifebar==null) {
			Debug.Log ("Da um setada no Lifebar");
			return;
		}

		Vector3 pos = lifebar.gameObject.transform.localScale;
		pos.x = hp / max;
		if( pos.x < 0 ) pos.x = 0;
		lifebar.gameObject.transform.localScale = pos;
	}

}
