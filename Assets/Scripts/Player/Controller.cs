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

	private Shape shape;

	void Start() {
		shape = gameObject.GetComponent<Shape> ();
	}

	void Update () {
		checkCrossButton ();
		checkAxes ();
		checkSquareButton ();
		checkSquareButtonHeld ();
		checkSquareButtonUp ();
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

	public float getXAxisRaw() {
		return Input.GetAxisRaw (CONTROLLER_PREFIX + mId + " X axis");
	}

	public float getYAxisRawInverted() {
		return -Input.GetAxisRaw (CONTROLLER_PREFIX + mId + " Y axis");
	}

	public bool getKeyDown(string ps4) {
			return Input.GetKeyDown(getJoystickButton(ps4));
	}

	public bool getKey(string ps4) {
		return Input.GetKey (getJoystickButton (ps4));
	}

	public bool getKeyUp(string ps4) {
		return Input.GetKeyUp (getJoystickButton (ps4));
	}

	private void checkCrossButton() {
		if (getKeyDown(CROSS_PS4)) {
			shape.CrossPressed();
		}
	}

	private void checkSquareButton() {
		if (getKeyDown(SQUARE_PS4)) {
			shape.SquarePressed();
		}
	}

	private void checkSquareButtonHeld () {
		if (getKey (SQUARE_PS4)) {
			shape.SquareHeld ();
		}
	}

	private void checkSquareButtonUp () {
		if (getKeyUp (SQUARE_PS4)) {
			shape.SquareUp ();
		}
	}

	private void checkCircleButton() {
		if (getKeyDown(CIRCLE_PS4)) {
			shape.CirclePressed();
		}
	}

	private void checkTriangleButton() {
		if (getKeyDown(TRIANGLE_PS4)) {
			shape.TrianglePressed();
		}
	}

	private void checkLeftBumperButton() {
		if (getKeyDown(LEFT_BUMPER_PS4)) {
			shape.LeftBumperPressed();
		}
	}

	private void checkRightBumperButton() {
		if (getKeyDown(RIGHT_BUMPER_PS4)) {
			shape.RightBumperPressed();
		}
	}

	private void checkLeftTriggerButton() {
		if (getKeyDown(LEFT_TRIGGER_PS4)) {
			shape.LeftTriggerPressed();
		}
	}

	private void checkRightTriggerButton() {
		if (getKeyDown(RIGHT_TRIGGER_PS4)) {
			shape.RightTriggerPressed();
		}
	}
}
