using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class MusicPart{
	

	public List<Periode> mesures;
	public PatternGroup patterns;
	public bool additional = false;


	public MusicPart(){
		this.mesures = new List<Periode>();
		this.patterns = new PatternGroup();
	}

	public bool doesHaveMesure(int x){
		bool result = false;

		int i = 0;
		while (result == false && i < mesures.Count){
			result = mesures[i].doesHave(x);
			i++;
		}
		return result;
	}

	public Pattern getAPattern(){
		
		return patterns.getAPattern ();
		
	}

}
