using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Apex;
using Apex.WorldGeometry;
using Apex.Utilities;
public class Player : MonoBehaviour 
{
	public static Player instance = null;

	// player status varibales
	public float STARTING_HP;
	private float hp;

	public enum GAME_STATE { PLACING_TOWERS, FIGHTING_WAVES };
	public GAME_STATE gameState;

	// GUI
	public Vector2 topLeftCorner;
	public Vector2 size;

	// raycasting variables
	private Transform cameraTransform;
	private Ray ray;
	private RaycastHit hit;
	private int counter;
	public float MAX_DIST_HIT;
	public float MIN_DIST_HIT;

	// tower variables
	public GameObject acceptableTowerPrefab;
	public GameObject unAcceptableTowerPrefab;
	private GameObject acceptableTower;
	private GameObject unAcceptableTower;
	public GameObject testTower;
	public List<GameObject> instantiatedTowers;
	public int TOWERS_TO_PLACE;
	private int placedTowers = 0;

	// pathfinding variables
	public GameObject gameWorldGrid;
	private GridComponent gc;
	private float cellSize;

	void Start () 
	{				
		hp = STARTING_HP;
		acceptableTower = GameObject.Instantiate(acceptableTowerPrefab) as GameObject;
		unAcceptableTower = GameObject.Instantiate(unAcceptableTowerPrefab) as GameObject;
		acceptableTower.transform.position = Vector3.zero;
		unAcceptableTower.transform.position = Vector3.zero;
		gc = gameWorldGrid.GetComponent<GridComponent>();
		cellSize = gc.grid.cellSize;
		instance = this;
	}

	void FixedUpdate () 
	{

	}

	void OnGUI()
	{
		if (gameState == GAME_STATE.PLACING_TOWERS)
		{
			
			string towerText = "TOWERS TO PLACE REMAINING: " + (TOWERS_TO_PLACE-placedTowers) + "\nGYRO: " + Input.gyro.attitude;
			GUI.Label(new Rect(topLeftCorner.x, topLeftCorner.y, size.x, size.y), towerText);
		}
		else
		{			
			string hpText = "HP: " + hp + "\nWAVE:" + RunGameForever.instance.waveNum;
			GUI.Label(new Rect(topLeftCorner.x, topLeftCorner.y, size.x, size.y), hpText);
		}
	}
	
	void Update()
	{
		if (gameState == GAME_STATE.PLACING_TOWERS)
		{
			cameraTransform = Camera.main.transform;
			ray = new Ray(cameraTransform.position, cameraTransform.forward);

			// (9 << 8) gets layer mask. only care about ground hits
			if(Physics.Raycast (ray, out hit, 5000.0f, (9 << 8)) && hit.distance < MAX_DIST_HIT && hit.distance > MIN_DIST_HIT)
			{

				// use cell size to place towers only in grid (gotta display the grid for real later)
				float xPoint = hit.point.x;
				float zPoint = hit.point.z;

				int cellsX = (int)Mathf.Round(xPoint/4.0f);
				int cellsZ = (int)Mathf.Round(zPoint/4.0f);

				xPoint = cellsX * cellSize;
				zPoint = cellsZ * cellSize;

				acceptableTower.transform.position = new Vector3(xPoint, -1.0f, zPoint);
			}
		}
		else
		{
			acceptableTower.transform.position = Vector3.zero;
			unAcceptableTower.transform.position = Vector3.zero;
		}
	}

	public void placeTower()
	{
		testTower = GameObject.Instantiate(acceptableTowerPrefab) as GameObject;
		testTower.transform.position = hit.point;
		testTower.transform.position = new Vector3(acceptableTower.transform.position.x, -1.0f, acceptableTower.transform.position.z);
		instantiatedTowers.Add(testTower);
		GridManager.instance.Update(testTower.GetComponent<BoxCollider>().bounds, 10);
		placedTowers++;
		if (placedTowers >= TOWERS_TO_PLACE)
		{						
			gameState = GAME_STATE.FIGHTING_WAVES;
			//gc.automaticInitialization = true;
			//gc.Disable(10);
			StartCoroutine(startWave());
		}
	}

	private IEnumerator startWave()
	{
		yield return new WaitForSeconds(.5f);
		RunGameForever.instance.shouldSpawnEnemies = true;
	}


}
