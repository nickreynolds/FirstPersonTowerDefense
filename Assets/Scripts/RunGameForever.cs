using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunGameForever : MonoBehaviour 
{
	public float MIN_TIME_TO_SPAWN;
	public float MAX_TIME_TO_SPAWN;
	public float MIN_DIST_TO_SPAWN;
	public float MAX_DIST_TO_SPAWN;
	public int MIN_ENEMIES_TO_SPAWN;
	public int MAX_ENEMIES_TO_SPAWN;

	public GameObject enemyToSpawnPrefab;
	public List<GameObject> spawnedEnemies = new List<GameObject>();

	public bool shouldSpawnEnemies;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(spawnForever());
		instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public static RunGameForever instance;


	private IEnumerator spawnForever()
	{
		while (true)
		{
			if (shouldSpawnEnemies)
			{
				// basically just randomly spawn enemies in any direction a specified range away
				float distance = Random.Range(MIN_DIST_TO_SPAWN, MAX_DIST_TO_SPAWN);
				float angle = Random.Range(0.0f, Mathf.PI*2.0f);

				float startX = Mathf.Sin(angle) * distance;
				float startZ = Mathf.Cos(angle) * distance;

				int numEnemiesToSpawn = Random.Range(MIN_ENEMIES_TO_SPAWN, MAX_ENEMIES_TO_SPAWN);

				for(int i=0; i < numEnemiesToSpawn; i++)
				{
					GameObject enemyToSpawn = GameObject.Instantiate(enemyToSpawnPrefab) as GameObject;

					// spawn them above ground so they don't get stuck (shouldn't be a magic number, but...)
					// also offset each enemy by (2, 0, 2) from prevous so they're in a line of sorts, doing it in world space this way
					// will make formations at least a bit varied.
					enemyToSpawn.transform.position = new Vector3(startX + (i*2.0f), 4.0f, startZ + (i*2.0f));
					spawnedEnemies.Add(enemyToSpawn);
				}

				
				float timeWait = Random.Range(MIN_TIME_TO_SPAWN, MAX_TIME_TO_SPAWN);
				yield return new WaitForSeconds(timeWait);
			}
			else
			{
				yield return null;
			}
		}
	}

	public void waitThenDestroyObject(float time, GameObject go)
	{
		StartCoroutine(waitThenDestroyObjectCoroutine(time, go));
	}

	private IEnumerator waitThenDestroyObjectCoroutine(float time, GameObject go)
	{
		yield return new WaitForSeconds(time);
		Destroy(go);
	}
}
