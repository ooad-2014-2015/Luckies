using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuButtons : MonoBehaviour {

	public Canvas characters;
	public Canvas scores;

	// Use this for initialization
	void Start () {
	
		characters.enabled = false;
		scores.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Escape)) {

			characters.enabled = false;
			scores.enabled = false;
		}
	}

	public void StartGame() {

		characters.enabled = true;
		scores.enabled = false;
		
	}

	public void StartMaleGame() {

		Handler.male = true;
		Application.LoadLevel ("Game");
	}

	public void StartFemaleGame() {

		Handler.male = false;
		Application.LoadLevel ("Game");
	}

	public void ShowScores() {

		scores.enabled = true;
		characters.enabled = false;
	}

	public void ExitGame() {
	
		Application.Quit ();
	}
}
