using UnityEngine;
using System.Collections;

[System.Serializable]

public class ObstacleInfo{
	
	public enum ObstacleType{BlueBall, GreenBar, Benoit};
	
	public ObstacleType type;
	public float time;
	public float speed;


	public ObstacleInfo(){
		type = ObstacleType.Benoit;
		time = 0;
		speed = 0;

	}
	
	public ObstacleInfo(string type, float time, float speed){
		
		switch(type){
		case "Ball":
			this.type = ObstacleType.BlueBall;
			break;
		case "Bar":
			this.type = ObstacleType.GreenBar;
			break;
		default:
			this.type = ObstacleType.Benoit;
			break;
		}
		this.time = time;
		this.speed = speed;
	}

	public ObstacleInfo(ObstacleType type, float time, float speed){
		
		this.type = type;
		this.time = time;
		this.speed = speed;
	}
	
	
	
}
