using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingBar : MonoBehaviour {
	public float Speed;

	private GameSystem _system;
	private float _timer;
	private Material _material;

	void Awake( ) {
		_material = this.gameObject.GetComponent<Image>( ).material;
		_system = GameObject.Find( "System" ).gameObject.GetComponent<GameSystem>( );
		_timer = 0;
	}

	void Update( ) {
		if ( _system.getState( ) != STATE.NONE ) {
			return;
		}
		if ( _timer > 1f ) {
			_system.setState( STATE.WARP );
			Destroy( gameObject );
		}
		_timer += Time.deltaTime * Speed;
		_material.SetFloat( "_Angle", Mathf.Lerp( -3.15f, 3.15f, _timer ) );
		_material.SetColor( "_Color", Color.Lerp( Color.red, Color.green, _timer ) );
	}
}
