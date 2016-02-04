using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	
	public Color nextColor = Color.white;
	public float speed = 10;
	
	public SpriteRenderer spriteRend;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(spriteRend.color != nextColor){
			spriteRend.color = Color.Lerp(spriteRend.color, nextColor,speed/60.0f);
		}
		
	}
	
	public void ChangeTo(Color color, float speed){
		this.nextColor = color;
		this.speed = speed;
	}
}
