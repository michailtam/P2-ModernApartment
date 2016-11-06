using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	private float animationSpeed = 0.8f;
	private Transform secondsPointer;		// Second pointer
	private Transform minutesPointer;		// Minute pointer
	private Transform hoursPointer;			// Hour pointer
	private float currentMinute = 0.0f;
	private float currentHour = 0.0f;
	private bool isNewMinuteSet = false;	// Flag for the minutes

	// Use this for initialization
	void Start () {
		// Get the pointers of the clock
		secondsPointer = transform.FindChild ("Clock_Handle_Seconds");
		minutesPointer = transform.FindChild ("Clock_Handle_Minutes");
		hoursPointer = transform.FindChild ("Clock_Handle_Hours");

		// Setting the animation speed independent of the platform
		Animator anim = transform.FindChild ("Clock_Handle_Seconds").GetComponent<Animator>();
		if (anim == null)
			Debug.LogError ("ERROR: Animator Component not found");
		else
			anim.speed = animationSpeed * Time.deltaTime;	

		// Init pointer positions of the clock
		secondsPointer.Rotate(0,0,0);
		minutesPointer.Rotate (0,0,0);
		hoursPointer.Rotate (0,0,0);
	}

	void Update() 
	{
		SetMinute ();
		SetHour();
	}

	void SetMinute ()
	{
		// Rotate the minute pointer
		if (secondsPointer.eulerAngles.z > 359.0f) {
			// Check if minute was set. If not set the new minute
			if (!isNewMinuteSet) {
				currentMinute += 6.0f;
				minutesPointer.Rotate (0,0,currentMinute);
				isNewMinuteSet = true;
			}
		}
		else
			if (isNewMinuteSet) {
				isNewMinuteSet = false;		// Reset the minute flag
			}
	}

	void SetHour ()
	{
		Debug.Log ("DEGREE: " + minutesPointer.eulerAngles.z);

		// Rotate the minute pointer
		if (minutesPointer.eulerAngles.z == 360f) {
			currentHour += 30.0f;
			hoursPointer.Rotate (0,0,currentHour);
			Debug.Log ("CURRENT HOUR: " + currentHour);
		}
	}
}
