using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Aim : MonoBehaviour {

	private Player _player;

	void Awake( ) {
		_player = GameObject.Find( "Player" ).gameObject.GetComponent<Player>( );
	}

	void Start( ) {
		gameObject.GetComponent<Image>( ).color = Color.green;
	}

	void Update( ) {
		
	}

	public void freeAim( ) {
		if ( _player.targetOn( ) ) {
			gameObject.GetComponent<Image>( ).color = Color.red;
		} else {
			gameObject.GetComponent<Image>( ).color = Color.green;
		}
	}

	public void fixAim( ) {
		gameObject.GetComponent<Image>( ).color = Color.green;
		Vector3 pos = _player.transform.position + _player.transform.forward;
		pos.z = _player.transform.position.z + 1.0f;
		gameObject.transform.position = pos;
	}
}
