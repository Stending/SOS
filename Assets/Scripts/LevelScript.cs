using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour {


	public Material mat;
	public int levelId;
	//public float tempo = 165.0f;

	public Stats stats;
	//public float counter = 12.0f;
	public int mesure = 1;
	public int mesureProgression = 1;

	public int shakeCounter = 4;
	
	public bool playing = false;
	public PatternGroup tutoPatterns = new PatternGroup();
	public bool tutorial = false;
	public int tutorialStep = 0;
	public float nextTutorialStep = 0;
	public bool tutoStepLost = false;

	public float mt;

	public List<ParticleMesure> particleMesures = new List<ParticleMesure>();
	public List<int> songParticleMesures = new List<int>();
	public ParticlesManager particlesManager;

	public int colorCounter = 0;
	public int colorLoop = 0;
	//public List<ColorPhase> defaultLevelColors = new List<ColorPhase> ();
	public List<ColorPhase> levelColors = new List<ColorPhase> ();
	public List<MusicPart> musicParts = new List<MusicPart> ();
	public PatternGroup basicPatterns = new PatternGroup();

	public bool startAtBeginning = false;
	public List<int> levelStart = new List<int>();

	public List<ObstacleInfo> obstacleInfos = new List<ObstacleInfo>();
	public List<Event> events = new List<Event>();
	public MusicScript music;
	public AudioSource playerAudio;
	

	public int exceptedCameraAngle = 0;

	public AudioClip music1;

	public AudioClip deathSound;

	public float obstacleSpeed = 15.0f;
	public Color generalColor;

 	public InterfaceScript inGameInterface;

	public PlayerScript player;
	public Animator playerAnim;


	public SpriteRenderer lineSpriteRend;
	public Animator lineAnim;

	public BackgroundScript background;
	public CameraScript cam;
	public Animation camAnim;

	public Object mesureSignal;
	public Object rotationSignal;
	public Object bar;
	public Object ball;

	public Color menuMainColor;
	public Color menuBackgroundColor;


	public bool didRotation = false;

	public int score = 0;
	public int highscore = 0;
	// Use this for initialization
	void Start () {

		Screen.SetResolution (600, 600, false);
		stats = GameObject.FindWithTag ("Statistics").GetComponent<Stats>();
		changeMainColor (menuMainColor);
		background.ChangeTo(menuBackgroundColor,10);

		inGameInterface.updateHighscore (stats.levelsScores[levelId]);
		gameStart ();
		//StartCoroutine (createObstacles());
	}
	
	// Update is called once per frame
	void Update () {


	
		mt = music.actualTime;
		if ((music.actualTime + (music.length * music.loop)) > ((float)shakeCounter / (float)music.tempo * 60)) {
			camAnim.Stop("CameraShake1");
			camAnim.Play("CameraShake1");
			shakeCounter++;
		}

		if (playing) {
			while (events.Count > 0 && (music.actualTime + (music.length * music.loop)) > events[0].time) {
				executeEvent (events [0]);
				events.RemoveAt (0);
			}
			while (obstacleInfos.Count > 0 && (music.actualTime + (music.length * music.loop)) > obstacleInfos [0].time - 10) {
				instantiateObstacle (obstacleInfos [0]);
				obstacleInfos.RemoveAt (0);
			}

			while(music.actualTime + (music.length * music.loop) > ((float)((levelColors[colorCounter].mesure + colorLoop * music.mesureLength)*4)/music.tempo * 60)){
				ColorPhase cp = levelColors[colorCounter];
				changeColorPhase(cp);
				if(colorCounter < levelColors.Count-1)
					colorCounter++;
				else{
					colorCounter = 0;
					colorLoop++;
				}

				//print((9*4)/music.tempo * 60);
			}
			if((music.actualTime + (music.length * music.loop)) > ((float)(mesureProgression-5)*4)/(float)music.tempo * 60){
				createMesureSignal(((float)(mesureProgression)*4)/(float)music.tempo * 60);
				mesureProgression++;
			}


			if (tutorial) {
				if((music.actualTime + (music.length * music.loop)) > nextTutorialStep){
					if(!tutoStepLost)
						tutorialStep++;
					if(tutorialStep < tutoPatterns.patterns.Count-1){
						tutoStepLost = false;
						nextTutorialStep = (mesure * 4) / music.tempo * 60;
						addPatternToObstacles (tutoPatterns.getPattern (tutorialStep+1));


					}else{

						//fin

					}
				}

				
			}else{
				while (obstacleInfos.Count < 20) {
					createObstacles ();
				}
			
			}

		}else{
			if(Input.GetKey("space")){
				gameStart();
			}
		}


	
	}

	void createMesureSignal(float time){
		GameObject signalGO = Instantiate (mesureSignal) as GameObject;
		//barGO.transform.position = new Vector3 (40, 0, 0);
		ObstacleScript obsScript = signalGO.GetComponent<ObstacleScript> ();
		signalGO.GetComponent<SpriteRenderer> ().color = generalColor;
		obsScript.hitTime = time;
		obsScript.speed = obstacleSpeed;
	}
	void createRotationSignal(float time, int angle){
		GameObject rotSignalGO = Instantiate (rotationSignal) as GameObject;
		rotSignalGO.transform.eulerAngles = new Vector3 (0, 0, angle);
		//barGO.transform.position = new Vector3 (40, 0, 0);
		ObstacleScript obsScript = rotSignalGO.GetComponent<ObstacleScript> ();
		rotSignalGO.GetComponent<SpriteRenderer> ().color = generalColor;
		obsScript.hitTime = time;
		obsScript.speed = obstacleSpeed;
	}

	void createBar(float time, float speed){
		GameObject barGO = Instantiate (bar) as GameObject;
		//barGO.transform.position = new Vector3 (40, 0, 0);
		ObstacleScript obsScript = barGO.GetComponent<ObstacleScript> ();
		barGO.GetComponent<SpriteRenderer> ().color = generalColor;
		obsScript.hitTime = time;
		obsScript.speed = speed;
	}
	void createBall(float time, float speed){
		GameObject ballGO = Instantiate (ball) as GameObject;
		//ballGO.transform.position = new Vector3 (40, 0, 0);
		ObstacleScript obsScript = ballGO.GetComponent<ObstacleScript> ();
		ballGO.GetComponent<SpriteRenderer> ().color = generalColor;
		obsScript.hitTime = time;
		obsScript.speed = speed;
	}



	public void addPatternToObstacles(Pattern pattern){
		if (pattern.type == Pattern.Type.CameraChange && didRotation) {
			return;
		}
		int i;
		for (i=0; i<pattern.obstacles.Count; i++) {
			ObstacleInfo oi = pattern.obstacles[i];
			//obstacleInfos.Add (oi);
			obstacleInfos.Add (new ObstacleInfo(oi.type, (mesure*4+oi.time)/music.tempo * 60, oi.speed));
			didRotation = false;
		}
		for (i=0; i<pattern.events.Count; i++) {
			Event e = pattern.events[i];

			if(e.type == Event.Type.RandomCameraAngle && !didRotation){
				int angle;
				do{
					int rand = Random.Range(0,4);

					switch (rand){
					case 0 : angle = 0;
						break;
					case 1 : angle = 90;
						break;
					case 2 : angle = 180;
						break;
					case 3 : angle = 270;
						break;
					default:
						angle = 0;
						break;
					}
				}while(angle == exceptedCameraAngle);
				createRotationSignal(((mesure*4 + e.time)/music.tempo * 60), exceptedCameraAngle - angle);
				exceptedCameraAngle = angle;
				//print ("Create a signal at " + ((mesure*4 + e.time)/music.tempo * 60));
				//events.Add (new Event(Event.Type.RotationWarning, (mesure*4 + e.time-2)/music.tempo * 60, e.speed, -angle, e.colorId));
				events.Add (new Event(Event.Type.RandomCameraAngle, (mesure*4 + e.time)/music.tempo * 60, e.speed, angle, e.colorId));
				//print ("Create rotate event at " + ((mesure*4 + e.time)/music.tempo * 60));

				didRotation = true;
			}else if(e.type == Event.Type.PreciseCameraAngle && !didRotation){
				events.Add (new Event(Event.Type.RotationWarning, (mesure*4 + e.time-2)/music.tempo * 60, e.speed, -e.angle, e.colorId));
				events.Add (new Event(Event.Type.PreciseCameraAngle, (mesure*4 + e.time)/music.tempo * 60, e.speed, e.angle, e.colorId));
				didRotation = true;
			}else if(e.type != Event.Type.RandomCameraAngle && e.type != Event.Type.PreciseCameraAngle){
				if(e.type == Event.Type.RandomColorChange || e.type == Event.Type.PreciseColorChange){
					events.Add (new Event(e.type, (mesure*4 + e.time)/music.tempo * 60, e.speed, e.colorId));
				}else if(e.type == Event.Type.Text){
					events.Add (new Event(e.type, (mesure*4 + e.time)/music.tempo * 60, e.textValue));
				}
			}

		}
		mesure += pattern.patSize;

	}

	public void changeColorPhase(ColorPhase cp){

		int rand = Random.Range (0, cp.colorGroups.Count);
		changeMainColor(cp.colorGroups[rand].objectsColor);
		changeObstacleColor(cp.colorGroups[rand].objectsColor);
		background.ChangeTo(cp.colorGroups[rand].backgroundColor,10);

	}

	public void changeMainColor(Color color){
		player.changeColor(color);
		changeObstacleColor(color);
		lineSpriteRend.color = color;
		inGameInterface.changeGlobalColor (color);
		generalColor = color;
		mat.color = color;
		//print ("lel " + mat.color);
		particlesManager.changeColor (color, mat);
	}
	public void changeObstacleColor(Color color){
		GameObject[] actualObstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		for(int i=0;i<actualObstacles.Length;i++){
			actualObstacles[i].GetComponent<SpriteRenderer>().color = color;
		}

	}

	public void createObstacles(){

		//addPatternToObstacles (basicPatterns.getAPattern ());
		int i = 0;
		PatternGroup pats = basicPatterns;

		while(i<musicParts.Count){
			//print (i + " " + musicParts.Count);
			if(musicParts[i].doesHaveMesure(mesure)){
				if(musicParts[i].additional){
					pats = new PatternGroup(pats, musicParts[i].patterns);
				}else{
					pats = musicParts[i].patterns;
					break;
				}
			}
			i++;

		}

		addPatternToObstacles(pats.getAPattern());



		/*int rand = Random.Range (0, 6);

		if (rand < 1) {

			obstacleInfos.Add(new ObstacleInfo("Ball", counter/music.tempo*60, 16));
			obstacleInfos.Add(new ObstacleInfo("Bar", (counter+1)/music.tempo*60, 16));
            obstacleInfos.Add(new ObstacleInfo("Ball", (counter+2)/music.tempo*60, 16));
			obstacleInfos.Add(new ObstacleInfo("Bar", (counter+3)/music.tempo*60, 16));

			counter +=4;

			didRotation = false;
		}else if (rand == 1) {
			if(!didRotation){
				float angle = 0;
				int random = Random.Range(0,4);
				switch(random){
					case 0: angle = 0;
							break;
					case 1: angle = 180;
							break;
					case 2: angle = 90;
							break;
					case 3: angle = 270;
						break;
					default:angle = 0;
						break;
				}
				events.Add(new Event("RotationWarning", (counter-1)/music.tempo*60, -angle));
				events.Add(new Event("CameraRotation", (counter+2)/music.tempo*60, 10, angle));
				counter+=4;
				didRotation = true;
			}

		}else if (rand == 2) {
			Color col;
			int random = Random.Range(0,12);
			if(random<3)
				col = Color.red;
			else if(random<6)
				col = new Color(1,1,0,1);
			else if(random<9)
				col = Color.green;
			else
				col = new Color(1,0,1,1);
			events.Add (new Event("BackgroundColor", (counter)/music.tempo*60, 10, col));

			//didRotation = false;
		}else if (rand == 3) {
			obstacleInfos.Add(new ObstacleInfo("Ball", counter/music.tempo*60, 16));
			obstacleInfos.Add(new ObstacleInfo("Bar", (counter+1)/music.tempo*60, 16));
			obstacleInfos.Add(new ObstacleInfo("Bar", (counter+2)/music.tempo*60, 16));
			obstacleInfos.Add(new ObstacleInfo("Bar", (counter+3)/music.tempo*60, 16));

			counter +=4;

			didRotation = false;
		}else if(rand == 4){

			obstacleInfos.Add(new ObstacleInfo("Bar", counter/music.tempo*60, 16));
			obstacleInfos.Add(new ObstacleInfo("Bar", (counter+1)/music.tempo*60, 16));
			obstacleInfos.Add(new ObstacleInfo("Bar", (counter+2)/music.tempo*60, 16));
			obstacleInfos.Add(new ObstacleInfo("Bar", (counter+3)/music.tempo*60, 16));
			counter +=4;

			didRotation = false;
		}else if (rand == 5) {
		obstacleInfos.Add(new ObstacleInfo("Bar", counter/music.tempo*60, 16));
		obstacleInfos.Add(new ObstacleInfo("Bar", (counter+1)/music.tempo*60, 16));
		obstacleInfos.Add(new ObstacleInfo("Ball", (counter+2)/music.tempo*60, 16));
		obstacleInfos.Add(new ObstacleInfo("Ball", (counter+3)/music.tempo*60, 16));
		
		counter +=4;
		
		didRotation = false;
	}*/

	}

	

	public void instantiateObstacle(ObstacleInfo oi){
		if (oi.type == ObstacleInfo.ObstacleType.GreenBar) {
						createBar (oi.time, obstacleSpeed);
		}else if (oi.type == ObstacleInfo.ObstacleType.BlueBall) {
						createBall (oi.time, obstacleSpeed);
		}

	}

	public void executeEvent(Event evt){

		if (evt.type == Event.Type.RandomColorChange) {
			int rand = Random.Range(0,levelColors.Count);
			/*background.ChangeTo (levelColors[rand].backgroundColor, evt.speed);
			changeMainColor (levelColors[rand].objectsColor);*/
		} else if (evt.type == Event.Type.PreciseColorChange) {
			/*background.ChangeTo (levelColors[evt.colorId].backgroundColor, evt.speed);
			changeMainColor (levelColors[evt.colorId].objectsColor);*/
		}else if(evt.type == Event.Type.Text){
			print (evt.textValue);
			inGameInterface.createText(evt.textValue);
		}else if(evt.type == Event.Type.RotationWarning){
			if((int)cam.nextAngle != (int)evt.angle){
				inGameInterface.appearWarning(evt.angle,4/music.tempo*60);
			}
		}else if(evt.type == Event.Type.RandomCameraAngle || evt.type == Event.Type.PreciseCameraAngle){
			switch((int)evt.angle){
			case 270:
				player.trianglePush = "Up";
				player.squarePush = "Down";
				cam.ChangeTo(270,evt.speed);
				break;
			case 180:
				player.trianglePush = "Left";
				player.squarePush = "Right";
				cam.ChangeTo(180,evt.speed);
				break;
			case 90:
				player.trianglePush = "Down";
				player.squarePush = "Up";
				cam.ChangeTo(90,evt.speed);
				break;
			case 0:
				player.trianglePush = "Right";
				player.squarePush = "Left";
				cam.ChangeTo(0,evt.speed);
				break;
			default:
				cam.ChangeTo(0,evt.speed);
				break;
			}
		}
	}

	public void incrementScore(int point){
		score += point;
		if (score > 50) {
			lineAnim.SetBool("InGame", false);
		}
		inGameInterface.updateScores (score);
	}


	public void gameStart(){
		if (!playing) {
			if(!tutorial){
				//fillLevelColors();
						/*int randColor = Random.Range (0,levelColors.Count);
						changeMainColor (levelColors[randColor].objectsColor);
						background.ChangeTo (levelColors[randColor].backgroundColor, 10);*/
						exceptedCameraAngle = 0;

						playing = true;
						player.transform.localPosition = new Vector3 (0, 0, 0);
						//music.loop = 1;
						music.play (music1);
						
						colorLoop = 0;
						colorCounter = 0;

						int startID;
						if(!startAtBeginning)
							startID = Random.Range (0, levelStart.Count);
						else
							startID = 0;
						music.changeTime((float)levelStart[startID] * 4 /music.tempo * 60);
						print ((levelStart[startID] * 4 /music.tempo * 60) + "   " + (levelStart[startID] * 4) + "     " + (levelStart[startID]+2));
						shakeCounter = levelStart[startID] * 4;
						mesure = mesureProgression = levelStart[startID]+2;
						fillParticleList(levelStart[startID]);
						/*if (startID == 0) {
								music.changeTime (0);
								shakeCounter = 0;
								counter = 12.0f;
								mesure = mesureProgression = 3;
						} else if (startID == 1) {
								music.changeTime ((95 / music.tempo * 60));
								shakeCounter = 95;
								counter = 104.0f;
								mesure = mesureProgression = 26;
						} else if (startID == 2) {
								music.changeTime ((191 / music.tempo * 60));
								shakeCounter = 191;
								counter = 200.0f;
								mesure = mesureProgression = 50;
						}*/

						for (int i=0; i<5; i++)
								createObstacles ();

			}else{
				playing = true;
				player.transform.localPosition = new Vector3 (0, 0, 0);
				
				music.play (music1);
				tutorialStart();

			}
						score = 0;
		
						player.trianglePush = "Right";
						player.squarePush = "Left";
		
						inGameInterface.updateScores (score);
		
		

		
						player.gameObject.SetActive (true);
						playerAnim.SetBool ("InGame", true);
						lineAnim.SetBool ("InGame", true);

						player.setTo ("triangle");
						player.state = "triangle";

						inGameInterface.buttonsDisappear ();
						inGameInterface.scoresAppear ();
			}
	}

	public void endGame(){ 

		if (score > stats.levelsScores[levelId]) {
			stats.levelsScores[levelId] = score;
			inGameInterface.updateHighscore(stats.levelsScores[levelId]);
			//PlayerPrefs.SetInt("Highscore", highscore);
		}

		changeMainColor (menuMainColor);
		background.ChangeTo(menuBackgroundColor,10);
		cam.ChangeTo(0,10);

		playing = false;
		music.stop ();
		killAllObstacles ();
		obstacleInfos.Clear ();
		events.Clear ();

		playerAnim.SetBool ("InGame", false);
		lineAnim.SetBool ("InGame", false);

		inGameInterface.buttonsAppear ();
		inGameInterface.scoresDisappear ();

	}

	public void tutorialStart(){
		mesure = 0;
		tutorialStep = 0;
		addPatternToObstacles (tutoPatterns.getPattern (0));
		nextTutorialStep = (mesure * 4) / music.tempo * 60;
		addPatternToObstacles (tutoPatterns.getPattern (1));

		//mesure += tutoPatterns.getPattern (0).patSize;


	}

	public void restartTutorialStep(){
		if(!tutoStepLost){
			tutoStepLost = true;
			obstacleInfos.Clear ();
			events.Clear();
			mesure -= tutoPatterns.getPattern(tutorialStep+1).patSize;
			nextTutorialStep = (mesure * 4) / music.tempo * 60;
			killObstaclesBefore((mesure*4)/music.tempo * 60);
			addPatternToObstacles(tutoPatterns.getPattern(tutorialStep));

	
			/*addPatternToObstacles (tutoPatterns.getPattern (tutorialStep));
			nextTutorialStep = (mesure * 4) / music.tempo * 60;*/
		}

	}

	public void musicVolume(float vol){
		music.setVolume(vol);
		playerAudio.volume = vol / 2.5f;
	}

	public void fillParticleList(int startMesure){
		print ("Debut : " + startMesure);
		int mesCount = 0;
		particlesManager.particleList.Clear();
		int i = 0,j = 0, k;
		while (i<startMesure && j< songParticleMesures.Count) {
			i += particleMesures[songParticleMesures[j]].mesSize;
			j++;
		}
		print ("On commence à la mesure : " + i + " avec la pattern : " + j);
		mesCount = i;
		for (j = j; j<songParticleMesures.Count; j++) {
			ParticleMesure actualPM = particleMesures[songParticleMesures[j]];
			for(k=0; k < actualPM.particlesInfos.Count; k++){
				ParticleInfos actualPI = actualPM.particlesInfos[k];
				particlesManager.particleList.Add(new ParticleInfos(((mesCount * 4 + actualPI.time)/music.tempo * 60), actualPI));
			}
			mesCount += actualPM.mesSize;
		}
	}
	/*IEnumerator createObstacles(){
		int counter = 10;
		while (true) {
			int rand = Random.Range (0,2);
			if(rand == 0){
				createBar(counter/music.tempo * 60, 20);
			}else{
				createBall(counter/music.tempo * 60, 20);
			}
			counter++;
			yield return new WaitForSeconds(0.2f);
		}

	}*/

	public void killObstaclesBefore(float time){
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		for (int i=0; i < obstacles.Length; i++) {
			ObstacleScript scr = obstacles[i].GetComponent<ObstacleScript>();
			if(scr.type != ObstacleScript.ObstacleType.Nothing){
				if(scr.hitTime > time){
					Destroy (obstacles[i]);
				}
			}

		}
	}

	public void killObstaclesAfter (float time){
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		for (int i=0; i < obstacles.Length; i++) {
			ObstacleScript scr = obstacles[i].GetComponent<ObstacleScript>();
			if(scr.type != ObstacleScript.ObstacleType.Nothing){
				if(scr.hitTime < time){
					Destroy (obstacles[i]);
				}
			}
			
		}
	}

	public void killAllObstacles(){
		GameObject[] obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
		for (int i=0; i < obstacles.Length; i++) {
			Destroy (obstacles[i]);			
		}
	}
}


