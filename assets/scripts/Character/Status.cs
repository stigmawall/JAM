using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {


	[SerializeField] private float _HP = 10;
	[SerializeField] private float _MAXHP = 100;
	[SerializeField] private float _Power = 1;
	[SerializeField] private float _Defense = 1;
	[SerializeField] private float _Speed = 1;


	// pode separar os poder por assist e nao em um geral
	public float AssistPower = 1;
	public float AssistIncreaseBarSpeed = 1;


	public void Heal( float value ) {
		_HP += ( value > _MAXHP ) ? _MAXHP : value; 
	}


	public void Damage( float value ) 
	{
		if( value - _Defense <= 0 )
			_HP -= 1;
		else
			_HP -= ( value - _Defense );

		if( _HP < 0 ) _HP = 0;
	}

	
	public float HP {
		get { return _HP; }
		set { _HP = value; }
	}

	public float MAXHP {
		get { return _MAXHP; }
	}

	public float Power {
		get { return _Power; }
		set { _Power = value; }
	}

	public float Defense {
		get { return _Defense; }
		set { _Defense = value; }
	}

	public float Speed {
		get { return _Speed; }
		set { _Speed = value; }
	}
}
