using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	//Controller 1 to 4
	public int mId = 1; //Default id
	public const string CONTROLLER_PREFIX = "joystick ";
	
	public const string SQUARE_PS4 = " button 0";
	public const string CROSS_PS4 = " button 1";
	public const string CIRCLE_PS4 = " button 2";
	public const string TRIANGLE_PS4 = " button 3";
	public const string LEFT_BUMPER_PS4 = " button 4";
	public const string RIGHT_BUMPER_PS4 = " button 5";
	public const string LEFT_TRIGGER_PS4 = " button 6";
	public const string RIGHT_TRIGGER_PS4 = " button 7";
	public const string SHARE_PS4 = " button 8";
	public const string OPTIONS_PS4 = " button 9";
	public const string L3_PS4 = " button 10";
	public const string R3_PS4 = " button 11";
	public const string PS_PS4 = " button 12";
	public const string TOUCH_PAD_PS4 = " button 13";

	public const string SQUARE_PS3 = " button 3";
	public const string CROSS_PS3 = " button 2";
	public const string CIRCLE_PS3 = " button 1";
	public const string TRIANGLE_PS3 = " button 0";
	public const string LEFT_BUMPER_PS3 = " button 4";
	public const string RIGHT_BUMPER_PS3 = " button 5";
	public const string LEFT_TRIGGER_PS3 = " button 6";
	public const string RIGHT_TRIGGER_PS3 = " button 7";
	public const string START_PS3 = " button 9";
	public const string L3_PS3 = " button 10";
	public const string R3_PS3 = " button 11";
	public const string PS_PS3 = " button 12";

	private Shape shape;

	void Start() {
		shape = gameObject.GetComponent<Shape> ();
	}

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
		shape.Move (x, y);
	}

	private bool checkButton(string ps4, string ps3) {
		debugJoystickNames ();
		if (string.Equals(Input.GetJoystickNames()[mId - 1], "PLAYSTATION(R)3 Controller")) {
			Debug.Log ("ps3 controller found:" + mId);
			return Input.GetKeyDown(getJoystickButton(ps3));
		} else {
			return Input.GetKeyDown(getJoystickButton(ps4));
		}
	}

	private void debugJoystickNames() {
		for (int i = 0; i<Input.GetJoystickNames().Length; i++) {
			Debug.Log ("name: " + Input.GetJoystickNames()[i] + ", i: " + i + ", checking id: " + mId);
		}
	}
	
	private void checkCrossButton() {
		if (checkButton(CROSS_PS4, CROSS_PS3)) {
			shape.CrossPressed();
		}
	}

	private void checkSquareButton() {
		if (checkButton(SQUARE_PS4, SQUARE_PS3)) {
			shape.SquarePressed();
		}
	}

	private void checkCircleButton() {
		if (checkButton(CIRCLE_PS4, CIRCLE_PS3)) {
			shape.CirclePressed();
		}
	}

	private void checkTriangleButton() {
		if (checkButton(TRIANGLE_PS4, TRIANGLE_PS3)) {
			shape.TrianglePressed();
		}
	}

	private void checkLeftBumperButton() {
		if (checkButton(LEFT_BUMPER_PS4, LEFT_BUMPER_PS3)) {
			shape.LeftBumperPressed();
		}
	}

	private void checkRightBumperButton() {
		if (checkButton(RIGHT_BUMPER_PS4, RIGHT_BUMPER_PS3)) {
			shape.RightBumperPressed();
		}
	}

	private void checkLeftTriggerButton() {
		if (checkButton(LEFT_TRIGGER_PS4, LEFT_TRIGGER_PS3)) {
			shape.LeftTriggerPressed();
		}
	}

	private void checkRightTriggerButton() {
		if (checkButton(RIGHT_TRIGGER_PS4, RIGHT_TRIGGER_PS3)) {
			shape.RightTriggerPressed();
		}
	}
}
