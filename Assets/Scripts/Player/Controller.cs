using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//Controller 1 to 4
	public int mId = 1; //Default id
	private const string SQUARE = " button 0";
	private const string CROSS = " button 1";
	private const string CIRCLE = " button 2";
	private const string TRIANGLE = " button 3";
	private const string LEFT_BUMPER = " button 4";
	private const string RIGHT_BUMPER = " button 5";
	private const string LEFT_TRIGGER = " button 6";
	private const string RIGHT_TRIGGER = " button 7";
	private const string SHARE = " button 8";
	private const string OPTIONS = " button 9";
	private const string L3 = " button 10";
	private const string R3 = " button 11";
	private const string PS = " button 12";
	private const string TOUCH_PAD = " button 13";
	private const string CONTROLLER_PREFIX = "joystick ";

	void Update () {
		checkCrossButton ();
		checkAxes ();
		checkSquareButton ();
		checkTriangleButton ();
		checkCircleButton ();
		checkLeftBumperButton ();
		checkLeftTriggerButton ();
		checkRightBumperButton ();
		checkRightTriggerButton ();
	}

	private string getJoystickButton(string button) {
		return CONTROLLER_PREFIX + mId + button;
	}
	
	private void checkAxes() {
		float x = Input.GetAxisRaw (CONTROLLER_PREFIX + mId + " X axis");
		float y = Input.GetAxisRaw (CONTROLLER_PREFIX + mId + " Y axis");
		if (x == 0 && y == 0) {
			return;
		}
		gameObject.GetComponent<Shape>().Move (x, y);
	}
	
	private void checkCrossButton() {
		if (Input.GetKeyDown (getJoystickButton(CROSS))) {
			gameObject.GetComponent<Shape>().CrossPressed();
		}
	}

	private void checkSquareButton() {
		if (Input.GetKeyDown (getJoystickButton(SQUARE))) {
			gameObject.GetComponent<Shape>().SquarePressed();
		}
	}

	private void checkCircleButton() {
		if (Input.GetKeyDown (getJoystickButton(CIRCLE))) {
			gameObject.GetComponent<Shape>().CirclePressed();
		}
	}

	private void checkTriangleButton() {
		if (Input.GetKeyDown (getJoystickButton(TRIANGLE))) {
			gameObject.GetComponent<Shape>().TrianglePressed();
		}
	}

	private void checkLeftBumperButton() {
		if (Input.GetKeyDown (getJoystickButton(LEFT_BUMPER))) {
			gameObject.GetComponent<Shape>().LeftBumperPressed();
		}
	}

	private void checkRightBumperButton() {
		if (Input.GetKeyDown (getJoystickButton(RIGHT_BUMPER))) {
			gameObject.GetComponent<Shape>().RightBumperPressed();
		}
	}

	private void checkLeftTriggerButton() {
		if (Input.GetKeyDown (getJoystickButton(LEFT_TRIGGER))) {
			gameObject.GetComponent<Shape>().LeftTriggerPressed();
		}
	}

	private void checkRightTriggerButton() {
		if (Input.GetKeyDown (getJoystickButton(RIGHT_TRIGGER))) {
			gameObject.GetComponent<Shape>().RightTriggerPressed();
		}
	}
}
