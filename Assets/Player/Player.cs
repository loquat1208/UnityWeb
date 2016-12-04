using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	private float min_x;
	private float max_x;
	private float min_y;
	private float max_y;
	private float rotationY = 0F;
	private float rotationX = 0F;
	private bool _target_on;
	private GameObject _loading;
	private RaycastHit _hit_obj;

	void Start( ) {
		_target_on = false;
		_loading = null;
	}

	public void targeting( ) {
		Vector3 fwd = transform.forward;
		if ( Physics.Raycast( transform.position, fwd, out _hit_obj, Mathf.Infinity ) ) {
			//just hit planet, do loading.
			if ( _hit_obj.transform.tag != "Planet" ) {
				return;
			}
			//loading is just one.
			if ( _loading == null ) {
				Object loading = Resources.Load( "Prefabs/Loading" );
				Vector3 pos = _hit_obj.transform.position;
				Quaternion dir = transform.rotation;
				_loading = Instantiate( loading, pos, dir ) as GameObject;
			}
			_target_on = true;
		} else {
			_target_on = false;
			Destroy( _loading );
		}
	}

	public bool targetOn( ) {
		return _target_on;
	}

	public void move( float progress ) {
		if ( _hit_obj.transform.gameObject == null ) {
			return;
		}
		Vector3 start = Vector3.zero;
		Vector3 end = _hit_obj.transform.position - new Vector3( -1, 0, 2 );
		transform.position = Vector3.Lerp( start, end, progress );
	}

	public void view( ) {
		if ( axes == RotationAxes.MouseXAndY ) {
			//rotationX = transform.localEulerAngles.y + Input.GetAxis( "Mouse X" ) * sensitivityX;
			rotationX += Input.GetAxis( "Mouse X" ) * sensitivityY;
			rotationX = Mathf.Clamp( rotationX, min_x, max_x );
			rotationY += Input.GetAxis( "Mouse Y" ) * sensitivityY;
			rotationY = Mathf.Clamp( rotationY, min_y, max_y );

			transform.localEulerAngles = new Vector3( -rotationY, rotationX, 0 );
		} else if ( axes == RotationAxes.MouseX ) {
			transform.Rotate( 0, Input.GetAxis( "Mouse X" ) * sensitivityX, 0 );
		} else {
			rotationY += Input.GetAxis( "Mouse Y" ) * sensitivityY;
			rotationY = Mathf.Clamp( rotationY, min_y, max_y );

			transform.localEulerAngles = new Vector3( -rotationY, transform.localEulerAngles.y, 0 );
		}
	}

	public void freeView( ) {
		min_x = -360f;
		max_x = 360f;
		min_y = -90f;
		max_y = 90f;
	}

	public void fixView( ) {
		min_x = -90f;
		max_x = 90f;
		min_y = -45f;
		max_y = 45f;
	}
}
