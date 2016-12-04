using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject Player;

    void Update( ) {
        
    }

	public void FreeCamera( ) {
		Quaternion _forward = Player.transform.rotation;
		transform.localRotation = _forward;
		transform.position = Player.transform.position;
	}
}
