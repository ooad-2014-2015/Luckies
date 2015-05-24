using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Handler : MonoBehaviour {

	static private Handler handler;

	public Canvas endGame;
	public Canvas highscoreEnd;
	public Canvas gamePaused;
	public Slider music;
	public Slider sfx;
	public InputField inField;
	public Button submitScore;

	static public bool male = true;
	static public bool running = false;
	static int hitCount, seconds, minutes, index;
	static public float sfxVolume;

	float spawnTimer = 0.0f;
	float levelUpTimer = 0.0f;
	float countTime = 0.0f;
	int spawnLevel = 1;
	int rand, randX, randY, k;


	public Transform malePlayer, femalePlayer;
	public Transform maleZombie, femaleZombie;

	private UnityEngine.Object player;
	static private List<UnityEngine.Object> zombies = new List<UnityEngine.Object>();

	GUIStyle mystyle;


	void Awake() {
		
		handler = this;

		sfxVolume = sfx.value;
		hitCount = 0;
		seconds = 0;
		minutes = 0;
		index = -1;
		handler.endGame.enabled = false;
		handler.highscoreEnd.enabled = false;
		handler.gamePaused.enabled = false;

	}

	// Use this for initialization
	void Start () {



		maleZombie.gameObject.GetComponent<Zombie>().zombieSpeed = -1.0f;
		femaleZombie.gameObject.GetComponent<Zombie>().zombieSpeed = -1.0f;

		if(male) player = Instantiate (malePlayer, new Vector3 (0, 0, 0), Quaternion.identity);
		else player = Instantiate (femalePlayer, new Vector3 (0, 0, 0), Quaternion.identity);

		GenerateRandom ();

		if (male) zombies.Add (Instantiate (maleZombie, new Vector3 (randX, randY, 0), Quaternion.identity));
        else zombies.Add (Instantiate (femaleZombie, new Vector3 (randX, randY, 0), Quaternion.identity));
		running = true;
	}


	void OnGUI() {

		mystyle = new GUIStyle (GUI.skin.label);
		mystyle.fontSize = 32;

		if(seconds<10) GUI.Label (new Rect (Screen.width-120, 40, 80, 40), String.Format("{0}:0{1}",minutes, seconds), mystyle);
		else GUI.Label (new Rect (Screen.width-120, 40, 80, 40), String.Format("{0}:{1}",minutes, seconds), mystyle);

		GUI.Label(new Rect (40, 40, 60, 40), Convert.ToString(hitCount), mystyle);

	}
	
	// Update is called once per frame
	void Update () {

		if (running) {

			gamePaused.enabled = false;
			CountPassingTime();

			spawnTimer += Time.deltaTime;
			levelUpTimer += Time.deltaTime;

			if (spawnTimer > 5) {

				for (int i=0; i<spawnLevel; i++) {
					GenerateRandom ();
					if (male)
						zombies.Add (Instantiate (maleZombie, new Vector3 (randX, randY, 0), Quaternion.identity));
					else
						zombies.Add (Instantiate (femaleZombie, new Vector3 (randX, randY, 0), Quaternion.identity));
				}

				spawnTimer = 0f;
			}

			if (levelUpTimer > 15) {
				
				if(male) maleZombie.gameObject.GetComponent<Zombie>().zombieSpeed -= 0.1f;
				else femaleZombie.gameObject.GetComponent<Zombie>().zombieSpeed -= 0.1f;

				spawnLevel++;
				levelUpTimer = 0;
			}

			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.P))
			{
			running = false;
			gamePaused.enabled = true;
			}

				

		} else {

			if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.P))
			{
			gamePaused.enabled = false;
			running = true;
			}
		}
	}

	static public void ShowEndScreen()
	{
		int myScore = minutes * 60 + seconds;
		List<int> onlySec = Spark.onlySec;
		for (int i=0; i<10; i++) {
			if(myScore > onlySec[i])
			{
				index = i;
				handler.inField.enabled = true;
				handler.submitScore.enabled = true;
				handler.highscoreEnd.enabled = true;
				handler.submitScore.enabled = true;
				handler.inField.enabled = true;
				break;
			}
		}
		if(handler.highscoreEnd.enabled==false)
		handler.endGame.enabled = true;
	}

	static public void KillZombie(Zombie z){

		zombies.Remove (z.gameObject as UnityEngine.Object);
		Destroy (z.gameObject);
		hitCount++;
	}
	
	void GenerateRandom() {

		rand = UnityEngine.Random.Range (0, 2);
		k = UnityEngine.Random.Range (0, 2);

		if (rand == 0) {

			randX = UnityEngine.Random.Range (-16, 17);

			if (k == 0)
				randY = UnityEngine.Random.Range(8,14);
			else
				randY = UnityEngine.Random.Range(-14,-7);

		} else {

			randY = UnityEngine.Random.Range (-8, 9);

			if (k == 0)
				randX = UnityEngine.Random.Range(15,22);
			else
				randX = UnityEngine.Random.Range(-22,-14);
		}

	}


	void CountPassingTime() {

		countTime += Time.deltaTime;
		
		if (countTime > 1) {
			
			seconds++;
			if(seconds==60) {
				
				seconds = 0;
				minutes++;
			}
			countTime = 0;
		}
	}

	public void ResumeGame() {

		gamePaused.enabled = false;
		running = true;
	}

	public void RestartGame() {
		
		Destroy ((player as Transform).gameObject);
		Application.LoadLevel ("Game");
	}
	
	public void BackToMenu() {

		Destroy ((player as Transform).gameObject);
		Application.LoadLevel ("Menu");
	}

	public void SubmitScore() {

		Spark.UpdateData (inField.text, Handler.minutes, Handler.seconds, Handler.hitCount, Handler.index);

		handler.inField.text = "";
		handler.inField.enabled = false;
		handler.submitScore.enabled = false;
	}

	public void SetMusicVolume() {

		gameObject.GetComponent<AudioSource> ().volume = music.value;
	}

	public void SetSFXVolume() {

		sfxVolume = sfx.value;
	}

}
