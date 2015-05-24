using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Highscores : MonoBehaviour {

	List<Data> data = null;

	public Text[] names = new Text[10];
	public Text[] times = new Text[10];
	public Text[] hits = new Text[10];


	// Use this for initialization
	void Start () {
	
		data = Spark.ldata;

		if (data != null) {
			for(int i=0;i<10;i++)
			{
				names[i].text = data[i].name;
				times[i].text = data[i].time;
				hits[i].text = PrepareHits(data[i].hits);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private string PrepareHits(int n) {

		string raw = Convert.ToString (n);
		string temp = Convert.ToString (raw [0]);
		for (int i=1; i<raw.Length; i++)
			temp += " " + raw [i];
		
		return temp;
	}
}
