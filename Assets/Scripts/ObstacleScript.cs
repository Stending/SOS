using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {
	public enum ObstacleType{Blue, Green, Red, Nothing};

	public ObstacleType type = ObstacleType.Green;

	public float hitTime = 10.0f;
	public float position = 1.0f;
	public float mt;
	public float speed = 1.0f;
	public MusicScript music;
	public float musicSize;

	public Animator anim;
	public bool active = true;

	// Use this for initialization
	void Start () {
		music = GameObject.FindWithTag ("Music").GetComponent<MusicScript>();
		musicSize = music.length;

		this.transform.position = new Vector3((((hitTime - (music.actualTime + (music.length * music.loop))) * speed) + position) , 0, this.transform.position.z);
		//appearTime = music.time;
		//this.rigidbody2D.velocity = new Vector3 (-10, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if(active){
			this.transform.position = new Vector3((((hitTime - (music.actualTime + (music.length * music.loop))) * speed) + position), 0, this.transform.position.z);
		}
	}

	void FixedUpdate(){
		if (!active) {
			this.GetComponent<Rigidbody2D>().velocity = new Vector3 (speed, 0, 0);
		}
	}

	public void die(){

		//this.GetComponent<Rigidbody2D>().velocity = new Vector3 (speed, 0, 0);
		
		anim.SetBool ("Active", false);
		active = false;
		Destroy (this.gameObject, 1.0f);

	}
}