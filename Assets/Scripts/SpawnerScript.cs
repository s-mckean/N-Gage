using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    public Transform player;
    public GameObject enemyPrefab;
    public int enemyNumber = 3;
    private List<EnemyControl> enemies = new List<EnemyControl>();
    EnemyControl enemyToBeRemoved;

	// Use this for initialization
	public void TriggeredStart () {
		for(int i = 0; i < enemyNumber; i++)
        {
            Vector3 placement = new Vector3(Random.Range(0.0f, 10.0f), 0.0f, Random.Range(0.0f, 10.0f));
            GameObject enemy = Instantiate(enemyPrefab, transform.position + placement, Quaternion.identity);
            enemy.GetComponent<EnemyControl>().TriggeredStart();
            enemies.Add(enemy.GetComponent<EnemyControl>());
        }
	}

    public void TriggeredUpdate()
    {
        foreach (EnemyControl enemy in enemies)
        {
            if (enemy.enemyIsDead)
            {
                enemyToBeRemoved = enemy;
            }
            else
            {
                enemy.TriggeredUpdate();
            }
        }

        if (enemyToBeRemoved != null)
        {
            enemies.Remove(enemyToBeRemoved);
            enemyToBeRemoved.Die();
        }
    }

    public void setPlayer(Transform p)
    {
        player = p;

        foreach (EnemyControl enemy in enemies)
        {
            enemy.SetHero(player);
        }
    }

    public void DestroyEnemy(GameObject enemy)
    {
        enemies.Remove(enemy.GetComponent<EnemyControl>());
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
