using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    public float distanceUpward = 10f;
    public float distanceDownward = 10f;
    public static float IslandMovementSpeed = 2;
    private float islandSpeed = IslandMovementSpeed / 30;
    public bool moveUpward = true;

    private Vector3 originalPos = new Vector3();
    private Vector3 pos = new Vector3();

    [HideInInspector]
    public bool wrongPlacement = false;

    private GameObject gravityZone;
    private GeneratorTower genTower;
    private SpawnerScript spawner;

    public int ringPosition = 1;

    public void triggeredStart(Transform player)
    {
        originalPos = gameObject.transform.position;
        gravityZone = this.gameObject.transform.Find("GravityZone").gameObject;

        if (this.gameObject.transform.Find("GeneratorTowerObject") != null)
        {
            genTower = this.gameObject.transform.Find("GeneratorTowerObject").GetComponent<GeneratorTower>();
            genTower.TriggeredStart();
        }

        spawner = this.gameObject.transform.Find("Spawner").GetComponent<SpawnerScript>();
        spawner.TriggeredStart();
        spawner.setPlayer(player);
    }

    public void triggeredUpdate(Vector3 forceFieldPos)
    {
        floatUpDown();
        if (genTower != null)
        {
            genTower.setEndPoint(forceFieldPos);
        }

        spawner.TriggeredUpdate();
    }

    void floatUpDown()
    {
        if (moveUpward == true)
        {
            pos = gameObject.transform.position;
            if (pos.y >= (originalPos.y + distanceUpward))
            {
                moveUpward = false;
            }
            else
            {
                pos.y += islandSpeed;
                gameObject.transform.position = pos;
            }
        }
        if (moveUpward == false)
        {
            pos = gameObject.transform.position;
            if (pos.y <= (originalPos.y - distanceDownward))
            {
                moveUpward = true;
            }
            else
            {
                pos.y -= islandSpeed;
                gameObject.transform.position = pos;
            }
        }
    }


    public bool isGravityZoneEnemyFree()
    {
        if (spawner.AreEnemiesLeft())
        {
            return false;
        }
        return true;
    }


    public GeneratorTower getGenTower()
    {
        if (genTower == null)
        {
            return null;
        }
        return genTower;
    }
}
