using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	
	public float RotateSpeed;
	public Color RimColor;
    public string URL;

	private Player _player;
	private Material _material;
	private Color _default;

	void Awake( ) {
		_player = GameObject.Find( "Player" ).gameObject.GetComponent<Player>( );
		_material = this.gameObject.GetComponent<Renderer>( ).material;
	}

	void Start( ) {
		_default = new Color( 0.3f, 0.3f, 0.3f, 0 );
		_material.SetFloat( "_RimPower", 1.8f );
	}

	void Update( ) {
		animationUpdate( );
		if ( _player.targetOn( ) ) {
			_material.SetColor( "_RimColor", RimColor );
		} else {
			_material.SetColor( "_RimColor", _default );
		}
	}

	void animationUpdate( ) {
		transform.Rotate( Vector3.up * Time.deltaTime * RotateSpeed );
	}

    public string getURL( ) {
        return URL;
    }
}
