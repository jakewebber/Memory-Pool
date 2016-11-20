//UGA CSCI 4830 Virtual Reality Spring 2016
//Jacob Webber & Kollin Adams

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class Shuffle : MonoBehaviour {

	public GameObject memoryball;
	public Texture[] balls;
	private List<Ball> ballslist = new List<Ball>();

	// Define ball (with a texture and value)
	class Ball {
		public int number;
		public Texture texture;
		
		public Ball(int n, Texture t){
			number = n;
			texture = t;
		}
	}
	
	// Use this for initialization
	public void Start () {
		ballslist = new List<Ball> ();
		for(int i=0; i<balls.Length; i++){ // Define 2 balls for each texture
			ballslist.Add(new Ball(i, balls[i]));
			ballslist.Add(new Ball(i, balls[i]));
		}
		if(balls.Length > 0) //Shuffle as long as some balls exist
			ShuffleBalls();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Shuffle routine for randomly assigning ball positions
	void ShuffleBalls() {
		int ballsCount = ballslist.Count;
		Camera.main.GetComponent<Logic>().SetSetsOfBalls(balls.Length);
		List<Ball> temp = new List<Ball>();
		for(int i = 0; i < ballsCount; i++) {
			int r = Random.Range(0, ballsCount - i);
			temp.Add(ballslist[r]);
			ballslist.RemoveAt(r);
		}
		ballslist = temp;
		generateBalls();
	}

	//Instantiate the balls in a grid based on number of balls in the scene
	public void generateBalls() {
		int row = 4;
		int column = ballslist.Count/row;
		if(ballslist.Count % row > 0)
			column += 1;
		float space = .2f;

		//Grid Layout instantiation
		for(int i=0; i<ballslist.Count; i++){
			GameObject mc = Instantiate(memoryball, 
				//           (--------------------X------------------) (--Y--)  (--------------------Z---------------------)
				new Vector3( (i%row+(i%row*space)) - (row/2f) + space, 	1, 		(i/row+(i/row*space)) - (column/2f) + space), 
				memoryball.transform.rotation) as GameObject;
			mc.GetComponentInChildren<MemoryBall>().SetMemoryball(ballslist[i].texture, ballslist[i].number);
		}
	}
}
