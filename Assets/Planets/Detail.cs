using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Detail : MonoBehaviour {

	public Sprite Image;
	public GameObject Planet;
	private GameObject _player;

	void Start( ) {
		gameObject.GetComponentInChildren<Image>( ).sprite = Image;
		_player = GameObject.Find( "Player" ).gameObject;
	}

	void Update( ) {
		gameObject.transform.position = Planet.transform.position;
	}
}
