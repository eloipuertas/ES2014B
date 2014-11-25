using UnityEngine;
using System.Collections;

public class Gameflow : MonoBehaviour {
	private Transform[] spawnPoints;
	public float minTimeBetweenSpawns = 10;
	public float maxTimeBetweenSpawns = 20;
	public int maxSpidersSpawned = 0;

	private AbstractEntity playerEntity;
	private int phase;
	private const int INITIAL_PHASE = 1;
	private const int TROLL_FIGHT = 2;
	private const int LEVEL_CLEARED = 3;
	private const int GAME_OVER = 0;

	void Awake() {
		phase = INITIAL_PHASE;
		
		Debug.Log("PlayerPrefs.GetString(\"player\"): " + PlayerPrefs.GetString("player"));

		string playerTemplate = PlayerPrefs.GetString("player");
		if ( playerTemplate != null ) {
			Instantiate (Resources.Load (playerTemplate));
		}

		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player != null) {
			playerEntity = player.GetComponent<AbstractEntity>();
		}
		GameObject[] goSpawnPoints = GameObject.FindGameObjectsWithTag ("Spawner");
		spawnPoints = new Transform[goSpawnPoints.Length];
		for (int i=0; i<goSpawnPoints.Length; i++) {
			spawnPoints[i] = goSpawnPoints[i].transform;
		}

		if (playerEntity != null) {
			phase = INITIAL_PHASE;
			InvokeRepeating ("phase_control",0f,2.5f);
		}
	}

	void phase_control(){

		if (playerEntity.isAlive ()) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
			GameObject enemy;
			AbstractEntity enemyEntity;
			if (phase == INITIAL_PHASE) {
				bool spidersCleared = true;
				for (int i=0;i<enemies.Length;i++){
					enemy = enemies[i];
					enemyEntity = enemy.GetComponent<AbstractEntity>();
					if (enemyEntity!=null){
						if (!enemyEntity.isAlive ()){
							//enemy.GetComponent<SpiderState>().destroyWithDelay(1f);
						}else{
							spidersCleared = false;
						}
					}
				}
				if (spidersCleared){
					phase = TROLL_FIGHT;

					for (int i=0;i<enemies.Length;i++){
						enemy = enemies[i];
						enemyEntity = enemy.GetComponent<AbstractEntity>();
						if (enemyEntity!=null){
							if (!enemyEntity.isAlive ()){
								enemy.GetComponent<SpiderState>().destroyObject();
							}
						}
					}

					//spawnSpiders ();
					Invoke ("spawnSpiders",3f);
					//TODO enable door
					//TODO spawn troll
				}
			}
			/* // Disabled to allow no more horde-massive-spiderduspawning shit-storm-scale
			} else if (phase == TROLL_FIGHT) {
				SpiderState spiderstate;
				//TrollState trollstate;
				for (int i=0;i<enemies.Length;i++){
					enemy = enemies[i];
					enemyEntity = enemy.GetComponent<AbstractEntity>();
					if (enemyEntity!=null){
						if (!enemyEntity.isAlive ()){
							spiderstate = enemy.GetComponent<SpiderState>();
							//trollstate = enemy.GetComponent<TrollState>();
							if (spiderstate!=null){
								spiderstate.destroyWithDelay(2.5f);
								Invoke ("spawnSpiders",2f);
							}//else if trollstate!=null
						}
					}
				}
			} */
		}else if (phase != GAME_OVER){
			phase = GAME_OVER;
		}
	}
	private void spawnSpiders(){
		Debug.Log ("Gameflow: spawnSpiders");
		int curr_enemies = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		for (int i=0; i<spawnPoints.Length; i++) {
			if (!Physics.CheckSphere (spawnPoints[i].position, 0.5f) && curr_enemies < maxSpidersSpawned) {
				Debug.Log ("Gameflow: Spawning spider");

				Object prefab = Resources.LoadAssetAtPath("Assets/spider/prefabs/black_spider.prefab", typeof(GameObject));
				GameObject clone = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
				clone.transform.position = spawnPoints[i].position;
				clone.GetComponent<SpiderState> ().enabled = true;
				clone.GetComponent<BasicSpiderAI> ().enabled = true;
			} 
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
