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
}
