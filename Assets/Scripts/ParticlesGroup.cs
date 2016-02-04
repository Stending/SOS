using UnityEngine;
using System.Collections;

public class ParticlesGroup : MonoBehaviour {

	public enum Shape{One, TwoBotTop, TwoCentTop, TwoCentBot, Circular, Cone, Repeat}
	
	public Shape actualShape = Shape.One;
	public ParticleSystem linearPS1;
	public ParticleSystem linearPS2;
	public ParticleSystem linearPS3;
	public ParticleSystem circularPS;
	public ParticleSystem conePS;

	public void Update(){
		//print (linearPS2.GetComponent<ParticleSystemRenderer> ().material.color);
	}

	public void load(Shape shp, ParticleInfos pi, float orientation){
			actualShape = shp;
			print(pi.globalOrientation);
			this.transform.localEulerAngles = new Vector3 (orientation, this.transform.localEulerAngles.y, this.transform.localEulerAngles.z);
			switch(shp){
			case Shape.One :
			{
				linearPS1.transform.localEulerAngles = new Vector3(0,0,0);
				print (linearPS1.transform.localEulerAngles);
				setParticleSystem(linearPS1, pi);
				break;
			}
			case Shape.TwoBotTop :
			{
				linearPS1.transform.localEulerAngles = new Vector3(0,45,0);
				linearPS2.transform.localEulerAngles = new Vector3(0,-45,0);
				setParticleSystem(linearPS1, pi);
				setParticleSystem(linearPS2, pi);
				break;
			}
			case Shape.TwoCentTop :
			{
				linearPS1.transform.localEulerAngles = new Vector3(0,45,0);
				linearPS2.transform.localEulerAngles = new Vector3(0,0,0);
				setParticleSystem(linearPS1, pi);
				setParticleSystem(linearPS2, pi);
				break;
			}
			case Shape.TwoCentBot :
			{	
				linearPS1.transform.localEulerAngles = new Vector3(0,0,0);
				linearPS2.transform.localEulerAngles = new Vector3(0,-45,0);
				setParticleSystem(linearPS1, pi);
				setParticleSystem(linearPS2, pi);
				break;
			}
			case Shape.Cone :
			{
				conePS.transform.localEulerAngles = new Vector3(0,0,0);
				setParticleSystem(conePS, pi);
				break;
			}
			case Shape.Circular :
			{
				circularPS.transform.localEulerAngles = new Vector3(0,0,0);
				setParticleSystem(circularPS, pi);
				break;
			}
			default:
			{
				break;
			}
		}
	}


	void setParticleSystem(ParticleSystem ps, ParticleInfos pi){


		//ps.duration = pi.duration / music.tempo * 60;
		ps.Stop ();
		ps.startSize = pi.size;
		ps.startSpeed = pi.speed;
		if(pi.quantity > 0){

			ps.Emit (pi.quantity);
		
		}ps.emissionRate = pi.timeQuantity;
		ps.Play ();


	}

	public void changeColor(Color color, Material mat){
		/*ParticleSystemRenderer pr = (ParticleSystemRenderer)linearPS1.GetComponent<Renderer>();
		pr.renderMode = ParticleSystemRenderMode.VerticalBillboard;
		pr.material.color = color;*/
		//linearPS1.GetComponent<ParticleSystemRenderer> ().sharedMaterial = mat;	
		print (color);
		linearPS1.GetComponent<ParticleSystemRenderer> ().sharedMaterial.SetColor("_Color", color);


		//print (linearPS2.GetComponent<ParticleSystemRenderer> ().material.color);
	}



}
