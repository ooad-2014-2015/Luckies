using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour {


	public int bulletSpeed = 40;
	// Update is called once per frame
	void Update () {
	
		transform.Translate (Vector3.up * Time.deltaTime * bulletSpeed);
		Destroy (this.gameObject, 2);
	}
}
