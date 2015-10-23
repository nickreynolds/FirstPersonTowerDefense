using UnityEngine;
using System.Collections;

public class DragAndTapFPSController : MonoBehaviour 
{
	public float MAX_TAP_LENGTH = 0.75f;
	private float timeBeganTap;
	public float sensitivity;
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.touchCount > 0)
		{
			Touch t = Input.touches[0];
			if(t.phase == TouchPhase.Moved)
			{
				transform.eulerAngles -= new Vector3(-t.deltaPosition.y * sensitivity, t.deltaPosition.x * sensitivity, 0f);
				//transform.Rotate(t.deltaPosition.y * sensitivity, t.deltaPosition.x * sensitivity, 0f, Space.Self);
			}
		}
		
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
