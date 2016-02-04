using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

	public LevelScript level;
	public float actualTime = 0;
	public float length = 0;
	public int tempo = 150;
	public int mesureLength;
	public int loop = 0;
	public AudioSource audioSource;

	public bool paused = false;

	private int lastTimeSamples = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		actualTime = audioSource.time;

		//print (audioSource.timeSamples + "   " + audioSource.clip.samples);
		if(level.playing){
			if (audioSource.timeSamples == audioSource.clip.samples || audioSource.timeSamples < lastTimeSamples){
				print ("Relaunch");
				changeTime(0);
				stop();
				play();
				loop++;
				lastTimeSamples = 0;
			}else{
				lastTimeSamples = audioSource.timeSamples;
			}
		}
	}

	public void load (AudioClip clip){

		audioSource.clip = clip;
		length = clip.length;

		mesureLength = (int)(length / (4f/tempo * 60f));
		loop = 0;

	}

	public void play(AudioClip clip){
		lastTimeSamples = 0;
		load (clip);
		play ();
	}
	public void play(){
		audioSource.Play ();
	}

	public void stop(){
		audioSource.Stop ();
	}


	public void changeTime(float time){
		actualTime = time;
		audioSource.time = time;
	}

	public void setVolume(float vol){
		audioSource.volume = vol;
	}

	void OnApplicationFocus(bool focusStatus) {
		paused = focusStatus;
	}

}
