using UnityEngine;
using System.Collections;
using Apex;
using Apex.Steering;
using Apex.Units;

public class NavigateToTowerEnemy : Enemy 
{

	public float MAX_DISTANCE_TO_DESTROY;

	public GameObject destination;
	// Use this for initialization
	protected override void Start () 
	{
		base.Start();
		destination = GameObject.Find("Tower").gameObject;
		Apex.UnityExtensions.GetUnitFacade(this).MoveTo(destination.transform.position, false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(gameObject.transform.position, destination.transform.position) < MAX_DISTANCE_TO_DESTROY)
		{
			Destroy (gameObject);
		}
	}
}
