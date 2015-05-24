using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	int healthPoints;
	IDamageTakeLogic iDTL;
	IDamageInflictLogic iDIL;

	public float zombieSpeed = -1.0f;

	void Awake() {
		
		healthPoints = 100;
		iDTL = new NormalDamageTake ();
		iDIL = new NormalDamageInflict ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Handler.running)
			transform.Translate (Vector3.up * Time.deltaTime * zombieSpeed);

		if (healthPoints <= 0)
			deleteCharacter (this);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Bullet")
		{
			loseHealth(iDTL.takeDamage(100));
			Destroy(coll.gameObject);
		}
	}

	public void loseHealth(int damage) {
		
		healthPoints -= iDTL.takeDamage (damage);
	}

	public int GetDamage() {
		return iDIL.inflictDamage ();
	}
	
	public void deleteCharacter(Zombie z){
		Handler.KillZombie (z);
	}
	
	public void setDamageTakeLogic(IDamageTakeLogic iDTL) {
		
		this.iDTL = iDTL;
	}

	public void setDamageInflictLogic(IDamageInflictLogic iDIL) {
		
		this.iDIL = iDIL;
	}
	
}
