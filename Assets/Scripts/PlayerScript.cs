using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public LevelScript level;

	public Animation playerAnimation = null;
	public Animator triangleAnimation;
	public Animator squareAnimation;
	public Animator circleAnimation;

	public AudioSource audioSource;
	public AudioClip blueSound;
	public AudioClip greenSound;

	public string trianglePush = "Right";
	public string squarePush = "Left";
	public string circlePush = "Space";
	public string state = "triangle";

	public SpriteRenderer triangleSprite;
	public SpriteRenderer squareSprite;
	public SpriteRenderer circleSprite;

	public BoxCollider2D hitbox;

	public float pushing = 0.0f;
	public float invicible = 0.0f;

	public float pushTime = 0.3f;
	//public Animator playerAnim = null;
	public bool collisionBlock = false;

	void Start () {
		setTo ("triangle");
	}

	void Update () {

		if (invicible > 0) {
			invicible -= Time.deltaTime;
			int i = (int)(invicible*5);
			if (i%2 == 0)
				displayShapes(true);
			else
				displayShapes(false);

			if(invicible <0){
				invicible = 0;
				hitbox.enabled = true;
			}
			
		}


		if (pushing > 0) {
			pushing -= Time.deltaTime;
			if(pushing <0){
				pushing = 0;
				collisionBlock = false;
			}

			}
		if (Input.GetButtonDown (trianglePush)) {
			collisionBlock = false;
			pushing = pushTime;
			if(state!="triangle"){
				triangleAnimation.SetBool("Active", true);
				squareAnimation.SetBool("Active", false);
				circleAnimation.SetBool("Active", false);
				state = "triangle";
			}
			playerAnimation.Stop("push");
			playerAnimation.Play("push");

		}else if(Input.GetButtonDown (squarePush)){
			collisionBlock = false;
			pushing = pushTime;
			if(state!="square"){
				squareAnimation.SetBool("Active", true);
				triangleAnimation.SetBool("Active", false);
				circleAnimation.SetBool("Active", false);
				//triangleAnimation.Play ("disappear");
				//playerAnimation.Play("squareSwitch");
				state = "square";
			}
			playerAnimation.Stop("push");
			playerAnimation.Play("push");
		}else if(Input.GetButtonDown (circlePush)){
			collisionBlock = false;
			pushing = pushTime;
			if(state!="circle"){
				circleAnimation.SetBool("Active", true);
				triangleAnimation.SetBool("Active", false);
				squareAnimation.SetBool("Active", false);
				//triangleAnimation.Play ("disappear");
				//playerAnimation.Play("squareSwitch");
				state = "circle";
			}
			playerAnimation.Stop("push");
			playerAnimation.Play("push");
		}
	}



	void OnTriggerEnter2D(Collider2D coll) {

		if (coll.tag == "Obstacle") {

			ObstacleScript obs = coll.GetComponent<ObstacleScript>();
			if(obs.type != ObstacleScript.ObstacleType.Nothing){
				if(obs.active != false){
					if(obs.type == ObstacleScript.ObstacleType.Blue && state != "square"){
						hit ();
					}else if(obs.type == ObstacleScript.ObstacleType.Green && state != "triangle"){
						hit ();
					}else if (pushing > 0) {
						if(obs.type == ObstacleScript.ObstacleType.Blue)
							audioSource.PlayOneShot(blueSound);
						else if(obs.type == ObstacleScript.ObstacleType.Green)
							audioSource.PlayOneShot(greenSound);
						obs.die();
						level.incrementScore(1);
						collisionBlock = true;
	
					} else {
						hit ();
					}
				}
			}
		}
	}

	void displayShapes(bool b){
		triangleSprite.enabled = b;
		squareSprite.enabled = b;
		circleSprite.enabled = b;

	}

	void hit(){
		if (level.tutorial) {
			level.restartTutorialStep();
			hitbox.enabled = false;
			invicible = 2.0f;
			//Invoke ("activeHitBox", 2.0f);
		}else {
			level.endGame();
			this.gameObject.SetActive(false);
		}
	}

	public void setTo(string type){
		triangleAnimation.SetBool ("Active", type == "triangle");
		squareAnimation.SetBool ("Active", type == "square");
		circleAnimation.SetBool ("Active", type == "circle");
	}

	public void changeColor(Color color){
		triangleSprite.color = color;
		squareSprite.color = color;
		circleSprite.color = color;
	}


	void activeHitBox(){
		hitbox.enabled = true;
	}
}
