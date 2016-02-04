using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float nextAngle = 0;
	public float speed = 10;

	public bool turning = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(this.transform.eulerAngles.z != nextAngle){
			this.transform.eulerAngles = Vector3.Lerp(this.transform.eulerAngles, new Vector3(0,0,nextAngle),0.1f);
		}
		
	}
	
	public void ChangeTo(float angle, float speed){
		this.nextAngle = angle;
		this.speed = speed;

		if (!turning) {
			turning = true;
			//StartCoroutine(Rotate ());


		}
		

	}


	public IEnumerator Rotate(){




		/*if (nextAngle > 180)
						nextAngle -= 360;*/

		float actualAngle = this.transform.eulerAngles.z%360;

		/*if (actualAngle > 180)
						actualAngle -= 360;*/
		string direction = "left";


		if (Mathf.Abs(actualAngle - nextAngle) < 180) {
			if(actualAngle > nextAngle)
				direction = "left";
			else
				direction = "right";
		}else{
			if(actualAngle > nextAngle)
				direction = "right";
			else
				direction = "left";
		}


	if(direction == "right"){
		while(this.transform.eulerAngles.z < nextAngle){

				this.transform.eulerAngles = new Vector3(0,0, this.transform.eulerAngles.z + speed );
				yield return null;

		}
			this.transform.eulerAngles = new Vector3(0,0,nextAngle);
	}else if(direction == "left"){
			while(this.transform.eulerAngles.z > nextAngle){
				
				this.transform.eulerAngles = new Vector3(0,0, this.transform.eulerAngles.z - speed );
				yield return null;
				
			}
			this.transform.eulerAngles = new Vector3(0,0,nextAngle);
	}
		turning = false;

	}
	
}
