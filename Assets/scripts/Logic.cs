//UGA CSCI 4830 Virtual Reality Spring 2016
//Jacob Webber & Kollin Adams

using UnityEngine;
using System.Collections;
using System.Timers;
using UnityEngine.UI;
public class Logic : MonoBehaviour {
	
	private MemoryBall[] balls = new MemoryBall[2];
	private int setsofballs;
	private int triesCount = 0;
	private int mismatchCount = 0;
	private float timer = 0;
	public Text countDisplay;
	public Text mismatchDisplay;
	public Text timerDisplay;
	private AudioSource source;


	void Awake() {
		source = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		timerDisplay.text = ((int)timer).ToString();

	}

	// Check two balls are equal, or assign the first ball if none are selected.
	public void CheckBalls(MemoryBall mc) {
		if (balls [0] == null) { // First ball click
			balls [0] = mc;
		} else {
			balls[1] = mc;
			triesCount++;
			countDisplay.text = triesCount.ToString();
			
			if (balls [0].ballnumber == balls [1].ballnumber) { // Equal check
				BallsMatching ();
			} else {
				ballsNotMatching ();
			}
			balls[0] = null;
			balls[1] = null;
		}
	}

	// Balls match, remove balls and subtract a set. Check for game end.
	void BallsMatching() {
		source.Play ();
		balls [0].RemoveBall ();
		balls [1].RemoveBall ();
		setsofballs--;
		if (setsofballs == 0) {
			Debug.Log ("GAME OVER. Tries: " + triesCount);
			Debug.Log ("Time: " + timer);
			Shuffle sn = gameObject.GetComponent<Shuffle> ();
			sn.Start ();
		}
	}
	// Balls do not match, start ball hide.
	void ballsNotMatching() {
		mismatchCount++;
		mismatchDisplay.text = mismatchCount.ToString();
		balls [0].Show ();
		balls [1].Show ();
		balls [0].Hide ();
		balls [1].Hide ();

	}
	IEnumerator NotMatching(){
		yield return new WaitForSeconds(2f);
		Debug.Log ("Balls don't match");
	}
		
	public void hideBalls(){
	}



	public void SetSetsOfBalls(int i){
		setsofballs = i;
	}
}
