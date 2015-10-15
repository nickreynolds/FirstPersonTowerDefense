using UnityEngine;
using System.Collections;

public class OrientCameraAndTapFPSController : MonoBehaviour 
{

	public float MAX_TAP_LENGTH = 0.75f;
	private float timeBeganTap;
	public GameObject player; // First Person Controller parent node
	public GameObject head; // First Person Controller camera
	
	// The initials orientation
	private float initialOrientationX;
	private float initialOrientationY;
	private float initialOrientationZ;
	
	// Use this for initialization
	void Start () {
		// Activate the gyroscope
		Input.gyro.enabled = true;
		
		// Save the firsts values
		initialOrientationX = Input.gyro.rotationRateUnbiased.x;
		initialOrientationY = Input.gyro.rotationRateUnbiased.y;
		initialOrientationZ = -Input.gyro.rotationRateUnbiased.z;
	}
	// Update is called once per frame
	void Update () 
	{
		//transform.rotation = Input.gyro.attitude;
		transform.Rotate (initialOrientationX -Input.gyro.rotationRateUnbiased.x, initialOrientationY - Input.gyro.rotationRateUnbiased.y, initialOrientationZ + Input.gyro.rotationRateUnbiased.z);

		if (Input.GetMouseButtonDown(0))
		{
			timeBeganTap = Time.time;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if(Time.time < (timeBeganTap + MAX_TAP_LENGTH))
			{
				Player.instance.placeTower();
			}
		}
	}
}
