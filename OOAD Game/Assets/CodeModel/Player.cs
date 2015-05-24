using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int healthPoints;
	IDamageTakeLogic iDTL;

	private Transform firePoint, mediumPoint;

	public LayerMask whatToHit;
	public Transform bullet;

	public AudioClip clip;
	private AudioSource audio;



	void Awake () {

		audio = gameObject.GetComponent<AudioSource> ();
		healthPoints = 100;
		iDTL = new NormalDamageTake ();
		firePoint = transform.FindChild ("firePoint");
		mediumPoint = transform.FindChild ("mediumPoint");
		if (firePoint == null) {
			Debug.LogError ("No fire point");
		} else if (mediumPoint == null) {
			Debug.LogError ("No median point");
		}
	}

	void Start() {
		
	}


	// Update is called once per frame
	void Update () {

		if (healthPoints <= 0) {

			Handler.running = false;
			Handler.ShowEndScreen();
		}

		if (Input.GetButtonDown ("Fire1") && Handler.running) {

			audio.PlayOneShot(clip, Handler.sfxVolume);
			Shoot();
		}
	}
	
	public void Shoot() {

		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
		BulletEffect ();
		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.black);
		
		if (hit.collider != null) {
			
			Debug.DrawLine(firePointPosition, hit.point, Color.red);
		}
	}

	void BulletEffect() {
		
		Instantiate (bullet, mediumPoint.position, mediumPoint.rotation);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Zombie")
		{
			loseHealth(coll.gameObject.GetComponent<Zombie>().GetDamage());
		}
	}

	public void loseHealth(int damage) {
		
		healthPoints -= iDTL.takeDamage (damage);
	}
	
	public void setDamageTakeLogic(IDamageTakeLogic iDTL) {
		
		this.iDTL = iDTL;
	}
}
