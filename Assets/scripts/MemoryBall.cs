//UGA CSCI 4830 Virtual Reality Spring 2016
//Jacob Webber & Kollin Adams

using UnityEngine;
using System.Collections;
using System.Timers;

public class MemoryBall : MonoBehaviour {
	public Texture blank;
	public int ballnumber;
	public AudioClip ballCollision;
	public bool selected = false;
	private bool hide = false;
	private int timerCount = 0;
	private Timer timer;
	private Texture memorytexture;
	private Logic logic;

	/* Sound definitions */
	private float lowRange = .75f;
	private float highRange = 1.5f;
	private float velToVol = .2f;
	private AudioSource source;

	// Create source for audio
	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	
	// Use this for initialization
	void Start () {
		logic = Camera.main.GetComponent<Logic>();
		if(!selected){
			selected = true;
			StartCoroutine("hideballs");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (hide && timerCount == 25) { // Hide materials after 25 frames have passed
			GetComponent<Renderer> ().materials [0].mainTexture = blank;
			hide = false;
			timerCount = 0;
		} else if(hide){
			timerCount++;
		}

	}

	//Assign a texture and number
	public void SetMemoryball(Texture t, int number) {
		GetComponent<Renderer>().materials[0].mainTexture = t;
		memorytexture = t;
		ballnumber = number;
	}
	//Display pool ball texture
	public void Show() {
		if(!selected){
			selected = true;
			GetComponent<Renderer> ().materials [0].mainTexture = memorytexture;
			logic.CheckBalls(this);
		}
	}

	// Set ball texture blank
	public void Hide() {
		hide = true;
		Update ();
		selected = false;
	}
	// Hide balls
	IEnumerator hideballs (){
		yield return new WaitForSeconds (2f); // wait (5sec's) before hide balls
		selected = false;
		GetComponent<Renderer> ().materials [0].mainTexture = blank;
	}

	// Remove balls upon correct selection
	public void RemoveBall() {
		StartCoroutine("Remove");
	}
	IEnumerator Remove() {
		yield return new WaitForSeconds(.5f);
		Destroy(gameObject);
	}




	// Detect collision strength for sound
	void OnCollisionEnter (Collision coll){
		source.pitch = Random.Range (lowRange, highRange);
		float hitVol = coll.relativeVelocity.magnitude * velToVol;
		if (hitVol > .30) { // sound play threshold
			source.PlayOneShot (ballCollision, hitVol);
		}
	}
}