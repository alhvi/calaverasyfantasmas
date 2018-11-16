using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public List<Player> players;
    public Enemy enemyPrefab;
    public int startEnemies;
    public int maxEnemies;
    private int currentEnemies;

	
	void Start () {


        for (int i = 0; i < GamepadManager.Instance.Inputs.Length && i < players.Count; i++) {
            if (GamepadManager.Instance.Inputs[i].active != false && GamepadManager.Instance.Inputs[i].device != null) {
                players[i].gameObject.SetActive(true);
            } else {
                players[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < startEnemies; i++) {
            InstantiateNewEnemy();
        }
        StartCoroutine(MoreEnemies());
	}
	
	
	void Update () {
		
	}

    void InstantiateNewEnemy() {
        Vector3 enemyPosition = new Vector3(Random.Range(-20f, 20f), 2.5f, Random.Range(-20f, 20f));
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
