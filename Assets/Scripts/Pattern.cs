using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class Pattern {

	public string name;
	public enum Type {CameraChange, ColorChange, BasicObstacles}

	public int proba;

	public Type type = Type.BasicObstacles;
	private int mesure = 0;
	public List<ObstacleInfo> obstacles;
	public List<Event> events;
	public int patSize = 1;

	public Pattern(){
		obstacles = new List<ObstacleInfo> ();
		events = new List<Event> ();
	}


}
