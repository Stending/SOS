using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {


	public static Stats Instance { get; private set; }

	public int[] levelsScores = new int[3];


	// Use this for initialization
	void Start () {
		if(Instance != null && Instance != this)
		{
			// If that is the case, we destroy other instances
			Destroy(gameObject);
		}
		
		// Here we save our singleton instance
		Instance = this;
		
		// Furthermore we make sure that we don't destroy between scenes (this is optional)
		DontDestroyOnLoad(gameObject);

		int i;
		for(i=0;i<levelsScores.Length;i++){
			levelsScores[i] = PlayerPrefs.GetInt("Level"+i, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
