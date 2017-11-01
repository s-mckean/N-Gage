using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public Transform player;
    public GameObject enemyPrefab;
    public int enemyNumber = 3;
    private List<GameObject> enemies = new List<GameObject>();

	// Use this for initialization
	public void TriggeredStart () {
		for(int i = 0; i < enemyNumber; i++)
        {
            Vector3 placement = new Vector3(Random.Range(0.0f, 10.0f), 0.0f, Random.Range(0.0f, 10.0f));
            GameObject enemy = Instantiate(enemyPrefab, transform.position + placement, Quaternion.identity);
            enemies.Add(enemy);
        }

        foreach( GameObject enemy in enemies )
        {
            enemy.GetComponent<EnemyControl>().TriggeredStart();
        }
	}

    public void TriggeredUpdate()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyControl>().TriggeredUpdate();
        }
    }

    public void setPlayer(Transform p)
    {
        player = p;

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyControl>().SetHero(player);
        }
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public bool AreEnemiesLeft()
    {
        if (enemies.Count > 0)
        {
            return true;
        }
        return false;
    }
}
