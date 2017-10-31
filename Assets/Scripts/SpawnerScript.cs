using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public Transform player;
    public GameObject enemyPrefab;
    public int enemyNumber = 3;

	// Use this for initialization
	void Start () {
		for(int i = 0; i < enemyNumber; i++)
        {
            Vector3 placement = new Vector3(Random.Range(0.0f, 10.0f), 0.0f, Random.Range(0.0f, 10.0f));
            GameObject enemy = Instantiate(enemyPrefab, transform.position + placement, Quaternion.identity);
            enemy.GetComponent<EnemyControl>().SetHero(player);
        }
	}
}
