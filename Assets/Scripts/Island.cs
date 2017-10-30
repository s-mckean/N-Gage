using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

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
    private GameObject genTower;

    public int ringPosition = 1;

    public void triggeredStart()
    {
        originalPos = gameObject.transform.position;
        gravityZone = this.gameObject.transform.Find("GravityZone").gameObject;
        genTower = this.gameObject.transform.Find("GeneratorTowerObject").gameObject;
    }

    public void triggeredUpdate()
    {
        floatUpDown();
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
        foreach (Transform child in gravityZone.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Enemy")
            {
                return false;
            }
        }
        return true;
    }

    public void DestroyGenTower()
    {
        Destroy(genTower);
    }

    public GameObject getGenTower()
    {
        return genTower;
    }
}
