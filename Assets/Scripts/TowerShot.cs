using UnityEngine;
using System.Collections;

public class TowerShot : MonoBehaviour 
{

	public GameObject enemyToGoTowards;
	public float SHOT_SPEED;
	public float DISTANCE_FROM_ENEMY_TO_HIT;
	public float DAMAGE_WHEN_HITS_ENEMY;
	public GameObject hitEnemyExplosionPrefab;
	public float EXPLOSION_TIME;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		if (enemyToGoTowards != null)
		{
			if (Vector3.Distance(enemyToGoTowards.transform.position, gameObject.transform.position) < DISTANCE_FROM_ENEMY_TO_HIT)
			{
				// do some kind of animation and do some damangeto the enemy
				onHitEnemy();
			}
			else
			{
				Vector3 translationToEnemy = enemyToGoTowards.transform.position - gameObject.transform.position;
				Vector3 directionToEnemy = translationToEnemy.normalized;
				//Vector3 thisFramesTranslation = 
				gameObject.transform.position += directionToEnemy * SHOT_SPEED;
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	private void onHitEnemy()
	{
		GameObject hitEnemyExplosion = GameObject.Instantiate(hitEnemyExplosionPrefab) as GameObject;
		hitEnemyExplosion.transform.position = gameObject.transform.position;
		enemyToGoTowards.GetComponent<Enemy>().ReceiveDamage(DAMAGE_WHEN_HITS_ENEMY);
		RunGameForever.instance.waitThenDestroyObject(EXPLOSION_TIME, hitEnemyExplosion);
		Destroy(gameObject);
	}
}
