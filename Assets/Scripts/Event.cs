using UnityEngine;
using System.Collections;
[System.Serializable]
public class Event {

	public enum Type{RandomCameraAngle, PreciseCameraAngle, RandomColorChange, PreciseColorChange, RotationWarning, Text};
	public Type type;
	public float time;
	public float speed;
	public float angle;
	public int colorId;
	public string textValue;

	public Event(){

		type = Type.RandomCameraAngle;
		time = 0;
		speed = 0;
		angle = 0;
		textValue = "";

	}


	public Event (Type type, float time, float speed, float angle, int colorId){
		this.type = type;
		this.time = time;
		this.speed = speed;
		this.angle = angle;
		this.colorId = colorId;
	}



	public Event (Type type, float time, string textValue){ //text 
		this.type = type;
		this.time = time;
		this.textValue = textValue;
	}




	public Event (Type type, float time, float speed, float angle){//Camera rotation
		this.type = type;
		this.time = time;
		this.speed = speed;
		this.angle = angle;
	}

	public Event (Type type, float time, float speed, int colorId){ //Color Change
		this.type = type;
		this.time = time;
		this.speed = speed;
		this.colorId = colorId;
	}

	public Event (Type type, float time, float angle){
		this.type = type;
		this.time = time;
		this.angle = angle;
	}


}
