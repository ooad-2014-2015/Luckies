using System;
using System.Runtime.Serialization;


[Serializable]
public class Data {
	
	public string name;
	public string time;
	public int hits;
	
	public Data(string name, string time, int hits) {
		
		this.name = name;
		this.time = time;
		this.hits = hits;
	}
}
