using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public Stats stats;

	public string actualSymbole = "triangle";

	public Animator triangleSymboleAnim;
	public Animator squareSymboleAnim;
	public Animator circleSymboleAnim;

	public Animator playAnimator;
	public Animator optionsAnimator;
	public Animator titleAnimator;
	public Animator symboleAnimator;

	public Animator level1Animator;
	public Animator level2Animator;
	public Animator level3Animator;
	public Animator backAnimator;

	public Text level1Score;
	public Text level2Score;
	public Text level3Score;

	/*public Animator PlayAnimator;
	public Animator PlayAnimator;
	public Animator PlayAnimator;*/



	public float symboleTimeChange = 3.0f;
	public float symboleTime = 3.0f;
	public void Start(){

		stats = GameObject.FindWithTag ("Statistics").GetComponent<Stats>();

		setSymboleTo ("triangle");


		playAnimator.SetBool ("Active", true);
		optionsAnimator.SetBool ("Active", true);
		titleAnimator.SetBool ("Active", true);

		symboleAnimator.SetBool ("Menu", true);
	
	
		level1Score.text = stats.levelsScores [0].ToString () + " / 60";
		level2Score.text = stats.levelsScores [1].ToString () + " / 120";
		level3Score.text = stats.levelsScores [2].ToString () + " / 180";
	
	
	
	}

	public void Update(){
		symboleTimeChange -= Time.deltaTime;
		if (symboleTimeChange < 0) {
			symboleChange();

		}

	}


	public void switchToLevelSelection(){
		menuOut ();
		levelSelectionIn ();
	}
	public void switchToMenu(){
		levelSelectionOut ();
		menuIn ();
	}


	public void menuIn(){
		playAnimator.SetBool ("Active", true);
		optionsAnimator.SetBool ("Active", true);
		titleAnimator.SetBool ("Active", true);
		
		symboleAnimator.SetBool ("Menu", true);
		
	}


	public void menuOut(){
		playAnimator.SetBool ("Active", false);
		optionsAnimator.SetBool ("Active", false);
		titleAnimator.SetBool ("Active", false);

		symboleAnimator.SetBool ("Menu", false);

	}

	public void levelSelectionIn(){

		level1Animator.SetBool ("Active", true);
		level2Animator.SetBool ("Active", true);
		level3Animator.SetBool ("Active", true);
		backAnimator.SetBool ("Active", true);
		symboleAnimator.SetBool ("LevelSelection", true);


	}

	public void levelSelectionOut(){
		
		level1Animator.SetBool ("Active", false);
		level2Animator.SetBool ("Active", false);
		level3Animator.SetBool ("Active", false);
		backAnimator.SetBool ("Active", false);
		symboleAnimator.SetBool ("LevelSelection", false);
		
		
	}

	public void symboleChange(){
		symboleTimeChange = symboleTime;
		if (actualSymbole == "triangle")
			setSymboleTo ("square");
		else if (actualSymbole == "square")
			setSymboleTo ("circle");
		else if (actualSymbole == "circle")
			setSymboleTo ("triangle");


	}

	public void setSymboleTo(string type){
		actualSymbole = type;
		triangleSymboleAnim.SetBool ("Active", type == "triangle");
		squareSymboleAnim.SetBool ("Active", type == "square");
		circleSymboleAnim.SetBool ("Active", type == "circle");
	}

	public void startLaunchingLevel(int i){

		levelSelectionOut ();
		symboleTimeChange = 10;
		symboleAnimator.SetBool ("LaunchGame", true);
		StartCoroutine(launchLevel (i));


	}

	public IEnumerator launchLevel(int i){

		yield return new WaitForSeconds(0.5f);

		Application.LoadLevel("Level" + i);
	
	}


}
