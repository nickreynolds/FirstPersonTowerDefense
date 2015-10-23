using UnityEngine;
using System.Collections;

public class OculusInputController : MonoBehaviour 
{

	
	public float MAX_TAP_LENGTH = 0.75f;
	private float timeBeganTap;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

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
