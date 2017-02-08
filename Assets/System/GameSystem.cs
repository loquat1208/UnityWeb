using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum STATE {
	NONE,
	WARP,
	ZOOM,
}

public class GameSystem : MonoBehaviour {
	public float ZoomSpeed;
    public float ZoomTime = 1;

	private Player _player;
	private Camera _camera;
	private Aim _aim;
	private STATE _state;
	private float _timer;

    void Awake( ) {
		_player = GameObject.Find( "Player" ).gameObject.GetComponent<Player>( );
		_camera = GameObject.Find( "Main Camera" ).gameObject.GetComponent<Camera>( );
		_aim = GameObject.Find( "Aim" ).gameObject.GetComponent<Aim>( );
		Time.captureFramerate = 60;
		_state = STATE.NONE;
	}

	void Update( ) {
	    _camera.FreeCamera( );
		_aim.freeAim( );
		switch ( _state ) {
		    case STATE.NONE:
		    	_player.targeting( );
                _player.createLoading( );
		    	_player.view( -360f, -90f, 360f, 90f );
		    	break;
		    case STATE.WARP:
		    	if ( _timer > ZoomTime ) {
		    		_state = STATE.ZOOM;
		    	}
		    	_timer += Time.deltaTime * ZoomSpeed;
		    	_player.move( _timer );
		    	break;
		    case STATE.ZOOM:
                string url = _player.getTarget( ).GetComponent<Planet>( ).getURL( );
                Application.OpenURL( url );
                _timer = 0;
                _state = STATE.NONE;
                break;
		}
	}

	public STATE getState( ) {
		return _state;
	}

	public void setState( STATE state ) {
		_state = state;
	}
}
