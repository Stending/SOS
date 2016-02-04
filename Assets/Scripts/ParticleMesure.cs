using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]

public class ParticleMesure{

	public string name = "Mesure";
	public List<ParticleInfos> particlesInfos;
	public int mesSize = 1;

	public ParticleMesure(){
		particlesInfos = new List<ParticleInfos>();
	}

}
