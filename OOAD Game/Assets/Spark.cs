using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class Spark: MonoBehaviour {
	
	public static List<Data> ldata = new List<Data> ();
	public static List<int> onlySec = new List<int> ();

	private string[] names = new string[10] {"K I N G", "D A M I A N", "D O N N Y", "L A Y L A", "G O B A T H",
		"A M A N D A", "R Y A N", "M E A N X", "R O G U E", "N O O B"};

	private string[] times = new string[10] {"5 : 0 0", "4 : 3 0", "4 : 0 0", "3 : 3 0", "3 : 0 0",
		"2 : 3 0", "2 : 0 0", "1 : 3 0", "1 : 0 0", "0 : 3 0"};
	
	private int[] hits = new int[10] {200, 180, 160, 140, 120, 100, 80, 60, 40, 20};


	private static Spark spark;

	void Awake() {

		if (spark == null) {

			DontDestroyOnLoad (gameObject);
			spark = this;
		} else if (spark != null) {

			Destroy(gameObject);
		}

		if (ldata.Count == 0) {

			if (!File.Exists (Application.persistentDataPath + "/zhdata.dat"))
				FillInitialData ();
			else
				ldata = LoadData ();

			if(onlySec.Count==0) FillSeconds ();
		}

	}

	private void FillInitialData() {

		for(int i=0;i<10;i++)
		{
			ldata.Add(new Data(names[i], times[i], hits[i]));
			
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create(Application.persistentDataPath + "/zhdata.dat");

			bf.Serialize (file, ldata);
			file.Close ();
		}
	}

	 private List<Data> LoadData(){

		List<Data> data = null;

		if (File.Exists (Application.persistentDataPath + "/zhdata.dat")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/zhdata.dat", FileMode.Open);
			data = bf.Deserialize(file) as List<Data>;
			file.Close();
		}
		return data;
	}

	private void FillSeconds(){

		string temp;

		for (int i=0; i<10; i++) {

			temp = ldata[i].time.Replace(" ","");
			List<string> lemp = temp.Split(':').ToList();

			onlySec.Add (Convert.ToInt32(lemp[0])*60 + Convert.ToInt32(lemp[1]));
		}
	}

	static public void UpdateData(string name, int min, int sec, int hits, int index)
	{
		for (int i=8; i>=index; i--) {
			ldata [i + 1].name = ldata [i].name;
			ldata [i + 1].time = ldata [i].time;
			ldata [i + 1].hits = ldata [i].hits;
			onlySec [i + 1] = onlySec [i];
		}

		ldata [index].name = PrepareName(name);
		ldata [index].time = PrepareTime (min, sec);
		ldata [index].hits = hits;
		onlySec [index] = min * 60 + sec;

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/zhdata.dat");
		
		bf.Serialize (file, ldata);
		file.Close ();
	}

	private static string PrepareName(string name) {

		string temp = Convert.ToString (name [0]);
		for (int i=1; i<name.Length; i++)
			temp += " " + name [i];

		return temp;
	}

	private static string PrepareTime(int min, int sec) {

		string temp;
		if (sec < 10)
			temp = min + ":0" + sec;
		else
			temp = min + ":" + sec;
		return PrepareName(temp);
	}
}
