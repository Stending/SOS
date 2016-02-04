using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class PatternGroup{

	public List<Pattern> patterns;


	
	public PatternGroup(){
		patterns = new List<Pattern> ();
	}

	public PatternGroup(PatternGroup p1, PatternGroup p2){
		patterns = new List<Pattern> ();
		int i;
		for (i = 0; i<p1.patterns.Count; i++) {
			patterns.Add (p1.patterns[i]);
		}
		for (i = 0; i<p2.patterns.Count; i++) {
			patterns.Add (p2.patterns[i]);
		}
	}

	public Pattern getPattern (int i){
		if (i < patterns.Count)
						return patterns [i];
				else
						return null;

	}
	public Pattern getAPattern(){
		//Debug.Log ("DEBUT");
		int max = 0;
		int i;
		for (i = 0; i<patterns.Count; i++) {
			max+=patterns[i].proba;
		}
		//Debug.Log ("MAX : " + max);

		int rand = Random.Range(0, max);
		int actualMax = 0;
		for (i = 0; i<patterns.Count; i++) {
			actualMax+=patterns[i].proba;
			if(rand < actualMax){
				return(patterns[i]);
			}
		}
		return patterns[0];

	}

}
