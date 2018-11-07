using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    //public GameManager floor;
    public Player player;
    public Enemy enemyPrefab;
    public int startEnemies;
    public int maxEnemies;
    private int currentEnemies;

	// Use this for initialization
	void Start () {
        instance = this;
        for (int i = 0; i < startEnemies; i++) {
            InstantiateNewEnemy();
        }
        StartCoroutine(MoreEnemies());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void InstantiateNewEnemy() {
        Vector3 enemyPosition = new Vector3(Random.Range(-20f, 20f), 2.5f, Random.Range(-20f, 20f));
        while (Vector3.Distance(enemyPosition, player.transform.position) < 5) {
            enemyPosition = new Vector3(Random.Range(-20f, 20f), 2.5f, Random.Range(-20f, 20f));
        }

        Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
        currentEnemies++;
    }

    public IEnumerator MoreEnemies()
    {
        while (currentEnemies < maxEnemies) {
            yield return new WaitForSeconds(1f);
            InstantiateNewEnemy();
        }
    }
}
