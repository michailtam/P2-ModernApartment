using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	private float animationSpeed = 5.0f; //0.8f;
	private Transform secondsPointer;		// Second pointer
	private Transform minutesPointer;		// Minute pointer
	private Transform hoursPointer;			// Hour pointer
	private bool isNewMinuteSet = false;	// Flag for the minutes
	private bool isNewHourSet = false;		// Flag for the hours

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
		SetMinute ();	// Set the minute
		SetHour();		// Set the hour
	}

	void SetMinute ()
	{
		// Rotate the minute pointer 6 degrees if one minute passed
		if (secondsPointer.eulerAngles.z > 359 && !isNewMinuteSet) {
			minutesPointer.Rotate (0,0,6.0f);
			isNewMinuteSet = true;
		} 
		else if (isNewMinuteSet && secondsPointer.eulerAngles.z > 0) {
			isNewMinuteSet = false;		// Reset the minute flag
		}
	}

	void SetHour ()
	{
		// // Rotate the hour pointer 30 degrees if one hour passed
		if (minutesPointer.eulerAngles.z < 0 && !isNewHourSet) {
			hoursPointer.Rotate (0,0,30.0f);
			isNewHourSet = true;
		} 
		else if(isNewHourSet && minutesPointer.eulerAngles.z > 0) {
			isNewHourSet = false;		// Reset the hour flag
		}
	}
}
