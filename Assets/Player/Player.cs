using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float ZoomDistance = 2f;

    private float rotationY = 0F;
    private float rotationX = 0F;
    private GameObject _loading;
    private RaycastHit _hit_obj;

    void Start( ) {
        _loading = null;
    }

    public bool targeting( ) {
        Vector3 fwd = transform.forward;
        if ( Physics.Raycast( transform.position, fwd, out _hit_obj, Mathf.Infinity ) ) {
            //just hit planet, do loading.
            if ( _hit_obj.transform.tag != "Planet" ) {
                return false;
            }
            return true;
        } else {
            return false;
        }
    }

    public GameObject getTarget( ) {
        return _hit_obj.transform.gameObject;
    }

    public void createLoading( ) {
        if ( targeting( ) ) {
            //loading is just one.
            if ( _loading == null ) {
                Object loading = Resources.Load( "Prefabs/Loading" );
                Vector3 pos = _hit_obj.transform.position;
                Quaternion dir = transform.rotation;
                _loading = Instantiate( loading, pos, dir ) as GameObject;
            }
        } else {
            Destroy( _loading );
        }
    }

	public bool targetOn( ) {
		return targeting( );
	}

	public void move( float progress ) {
		if ( _hit_obj.transform.gameObject == null ) {
			return;
		}
		Vector3 start = Vector3.zero;
        Vector3 end = _hit_obj.transform.position - ZoomDistance * transform.forward;
		transform.position = Vector3.Lerp( start, end, progress );
	}

	public void view( float min_x, float min_y, float max_x, float max_y ) {
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
}
