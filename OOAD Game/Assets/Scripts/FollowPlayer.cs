using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public int offset = 270;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		//Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		Vector3 difference = transform.position - new Vector3 (0, 0, 0);
		difference.Normalize ();
		
		float rotationAngle = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		
		transform.rotation = Quaternion.Euler (0f, 0f, rotationAngle + offset);
		
	}
}
