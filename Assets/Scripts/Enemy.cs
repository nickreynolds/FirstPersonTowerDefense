using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	
	public float MAX_HP;
	private float hp;

	public GameObject dyingEffectPrefab;
	public float DYING_EFFECT_TIME;

	// Use this for initialization
	protected virtual void Start () 
	{
		hp = MAX_HP;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void ReceiveDamage(float damage)
	{
		hp -= damage;
		if (hp <= 0)
		{
			StartCoroutine(die ());
		}
	}

	private IEnumerator die()
	{
		GameObject dyingEffect = GameObject.Instantiate(dyingEffectPrefab);
		dyingEffect.transform.position = gameObject.transform.position;		
		RunGameForever.instance.waitThenDestroyObject(DYING_EFFECT_TIME, dyingEffect);
		yield return null;
		Destroy(gameObject);
	}
}
