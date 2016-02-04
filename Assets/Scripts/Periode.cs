using UnityEngine;
using System.Collections;
[System.Serializable]

public class Periode  {

		
	public int a;
	public int b;
		
	public bool doesHave (int x){
		return(x > a && x < b);
	}

}
