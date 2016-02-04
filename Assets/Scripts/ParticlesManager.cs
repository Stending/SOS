using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticlesManager : MonoBehaviour {

	public List<ParticleInfos> particleList;
	public MusicScript music;

	public ParticlesGroup topParticleGrp;
	public ParticlesGroup bottomParticleGrp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((music.actualTime + (music.length * music.loop)) > particleList [0].time) {

			play (particleList[0]);
			particleList[0].time += music.length;
			particleList.Add (particleList[0]);
			particleList.RemoveAt(0);
		}
	}

	void play(ParticleInfos pi){
		if (pi.bottomSystem)
						bottomParticleGrp.load (pi.shapeType, pi, -pi.globalOrientation);
		if (pi.topSystem)
						topParticleGrp.load (pi.shapeType, pi, pi.globalOrientation);

	}

	void play(ParticleSystem ps, ParticleInfos pi){
		//ps.duration = pi.duration / music.tempo * 60;
		ps.Stop ();
		ps.startSize = pi.size;
		ps.startSpeed = pi.speed;
		if(pi.quantity > 0)
			ps.Emit (pi.quantity);
		ps.emissionRate = pi.timeQuantity;
		ps.Play ();
	}

	public void changeColor(Color color, Material mat){
		topParticleGrp.changeColor(color, mat);
		bottomParticleGrp.changeColor(color, mat);
	}

	/*public void changeColor(ParticleSystem ps, Color color){
		ps.startColor = color;
	}*/

}
