//UGA CSCI 4830 Virtual Reality Spring 2016
//Jacob Webber & Kollin Adams
//Original FPS provided by Dr. Kyle Johnsen, modified to suit this project.

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;  //must include this to execute events and have access to event classes
public class GazeCursor : MonoBehaviour {

	public Camera cam; //must assign the camera in the inspector that will do the raycasting.  Make sure this camera is a physics raycaster if you want cursor clicks to work too.
	public float defaultDistance=10.0f; //how far away is the cursor if nothing is intersected?

	void Update () {
		if (cam == null) {  //don't execute if failed to assign the camera
			Debug.Log ("camera not assigned to gaze cursor");
			return;
		} else {
			Cursor.lockState = CursorLockMode.Locked; 
			Cursor.visible = false;
		}

		//do a quick raycast through the camera center
		GameObject hitObject = null;
		RaycastHit rh;
		if (Physics.Raycast (new Ray (cam.transform.position, cam.transform.forward), out rh)) {
			hitObject = rh.transform.gameObject;
			this.transform.position = rh.point; //move to the hit point if successful
		} else {
			this.transform.position = cam.transform.position + cam.transform.forward * defaultDistance; //otherwise set to default.

		}

		//do a click action if we are over an object and the user hits the E Key on the keyboard or clicks
		if(Input.GetKeyDown (KeyCode.E) || Input.GetMouseButtonDown(0)){
			if(hitObject != null){
				Debug.Log ("here: " + hitObject.name);
				//ExecuteEvents.Execute(hitObject,pointer,ExecuteEvents.pointerClickHandler);
				if (rh.transform.GetComponent<MemoryBall> () != null) { //MemoryObject was clicked
					rh.transform.GetComponent<MemoryBall> ().Show ();
					Vector3 moveDir = rh.transform.position - cam.transform.position; //Movement vector for moving objects away from click location
					rh.transform.GetComponent<Rigidbody>().AddForce(moveDir, ForceMode.Impulse);
				}
			}
		}
	}
}
