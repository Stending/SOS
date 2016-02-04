using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InterfaceScript : MonoBehaviour {

	public Animator rotationWarning;
	public Image rotationWarningImage;
	public RectTransform rotationWarningImageTransform;
	public float warningTime;

	public Text highscore;

	public Text score1;
	public Animator score1Anim;
	public Text score2;
	public Animator score2Anim;

	public Animator playButton;

	public Object textPrefab;

	void Update(){
		if (warningTime > 0) {
			warningTime -= Time.deltaTime;
			if(warningTime <= 0){
				rotationWarning.SetBool ("Active", false);
				warningTime = 0;
			}
		}
	}
	public void appearWarning(float angle, float time){
		rotationWarningImageTransform.eulerAngles = new Vector3 (0, 0, angle);
		rotationWarning.SetBool ("Active", true);
		warningTime = time;
	}


	public void changeGlobalColor(Color color){
		highscore.color = color;
		score1.color = color;
		score2.color = color;
		rotationWarningImage.color = color;

	}

	public void scoresAppear(){
		score1Anim.SetBool ("InGame", true);
		score2Anim.SetBool ("InGame", true);
	}

	public void scoresDisappear(){
		score1Anim.SetBool ("InGame", false);
		score2Anim.SetBool ("InGame", false);
	}

	public void buttonsDisappear(){
		playButton.SetBool ("InGame", true);
	}

	public void buttonsAppear(){
		playButton.SetBool ("InGame", false);
	}

	public void updateScores(int value){
		score1.text = score2.text = value.ToString ("00"); 
	}
	public void updateHighscore(int value){
		highscore.text = value.ToString ("00");
	}


	public void createText(string text){
				GameObject textGO = Instantiate (textPrefab) as GameObject;
				textGO.transform.SetParent (this.transform);		
				RectTransform textRect = textGO.GetComponent<RectTransform> ();
				textRect.localPosition = new Vector3 (400, -120, 0);
				Text textText = textGO.GetComponent<Text> ();
				textText.text = text;
				//Animator textAnim = textGO.GetComponent<Animator> ();
		}
}