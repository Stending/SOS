using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class ParticleInfos {

	public ParticlesGroup.Shape shapeType = ParticlesGroup.Shape.One;
	public bool topSystem = true;
	public bool bottomSystem = true;
	public float time;
	public float duration = 0.50f;
	public int quantity = 1;
	public int timeQuantity = 1;
	public int speed = 5;
	public int globalOrientation = 0;
	public int size = 1;


	public ParticleInfos(float time, ParticleInfos pi){
		this.topSystem = pi.topSystem;
		this.bottomSystem = pi.bottomSystem;
		this.duration = pi.duration;
		this.globalOrientation = pi.globalOrientation;
		this.quantity = pi.quantity;
		this.timeQuantity = pi.timeQuantity;
		this.speed = pi.speed;
		this.size = pi.size;
		this.shapeType = pi.shapeType;

		this.time = time;
	}

}
