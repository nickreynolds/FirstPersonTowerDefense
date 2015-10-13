using UnityEngine;
using System.Collections;

public class DefenseTower : MonoBehaviour 
{

	private float timeSinceLastShot;
	public GameObject shotPrefab;

	public GameObject shotSpawnLocation;

	public float MIN_TIME_BETWEEN_SHOTS;
	public float SHOT_RANGE;

	// Use this for initialization
	void Start () 
	{
		timeSinceLastShot = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		// fire at closest enemy if within range and enough time has passed since last shot
		if ((timeSinceLastShot + MIN_TIME_BETWEEN_SHOTS) < Time.time)
		{
			if (RunGameForever.instance != null)
			{
				GameObject closestEnemy = null;
				float closestDistance = float.MaxValue;

				foreach(GameObject enemy in RunGameForever.instance.spawnedEnemies)
				{
					if (enemy != null)
					{
						float distanceBetweenTowerAndEnemy = Vector3.Distance(enemy.transform.position, gameObject.transform.position);
						if (distanceBetweenTowerAndEnemy < closestDistance)
						{
							closestDistance = distanceBetweenTowerAndEnemy;
							closestEnemy = enemy;
						}
					}
				}

				if (closestEnemy != null && closestDistance < SHOT_RANGE)
				{
					timeSinceLastShot = Time.time;
					fireShot(closestEnemy);
				}
	        }
		}
	}

	private void fireShot(GameObject enemy)
	{
		GameObject shot = GameObject.Instantiate(shotPrefab) as GameObject;
		shot.transform.position = shotSpawnLocation.transform.position;
		shot.GetComponent<TowerShot>().enemyToGoTowards = enemy;
	}


}
