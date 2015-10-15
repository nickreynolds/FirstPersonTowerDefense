using UnityEngine;
using System.Collections;

public class DragAndTapFPSController : MonoBehaviour 
{
	public float MAX_TAP_LENGTH = 0.75f;
	private float timeBeganTap;
	public float sensitivity;
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;
	public float h = -180;
	
	float rotationY = 0F;

	private Vector3 startDragPos = Vector2.zero;

	private float x = 0.0f;
	private float y = 0.0f;

	Transform target;// : Transform;
	float distance = 2.0f;
	
	float xSpeed = 180.0f;
	float ySpeed = 90.0f;
	
	float yMinLimit = -80.0f;
	float yMaxLimit = 80f;

	void Start () 
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
		
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0)
		{
			Vector2 drag = Vector2.zero;
			if (Input.touchCount > 0)
			{
				drag = Input.GetTouch(0).deltaPosition;
			}
			float rotationX = transform.localEulerAngles.y + drag.x * sensitivity;
			rotationY += Input.GetAxis("Mouse Y") * sensitivity;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			transform.localEulerAngles = new Vector3(rotationY, -rotationX, 0);
		}
		else if (Input.GetMouseButtonDown(0))
		{
			timeBeganTap = Time.time;
			startDragPos = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0))
		{
			//float
			x -= Input.GetAxis("Mouse X") * sensitivity * 0.02f;
			y += Input.GetAxis("Mouse Y") * sensitivity * 0.02f;
			
			y = ClampAngle(y, yMinLimit, yMaxLimit);			
			var rotation = Quaternion.Euler(y, x, 0f);
			transform.rotation = rotation;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if(Time.time < (timeBeganTap + MAX_TAP_LENGTH))
			{
				Player.instance.placeTower();
			}
		}
	}

	static float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}
