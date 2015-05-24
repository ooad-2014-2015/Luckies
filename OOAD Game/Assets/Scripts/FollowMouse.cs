using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	public int offset = 270;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Handler.running) {
			Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			difference.Normalize ();
		
			float rotationAngle = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		
			transform.rotation = Quaternion.Euler (0f, 0f, rotationAngle + offset);
		}

	}
}
