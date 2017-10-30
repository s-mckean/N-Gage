﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandRing : MonoBehaviour {

    public GameObject islandPrefab;
    private GameObject _islandPrefab;

    public float RadiusX = 1;
    public float RadiusZ = 1;
    public bool rotateClockwise = true;
    public float rotationSpeed = 1f;
    public int islandAmount = 5;
    public int genTowerAmount = 0;

    public Vector3 centerPoint = new Vector3(0, 0, 0);

    [HideInInspector]
    public List<Vector3> positions = new List<Vector3>();
    [HideInInspector]
    private List<GameObject> genTowers = new List<GameObject>();

    public bool allGenTowersDestroyed = false;

	// Use this for initialization
	public void TriggeredStart () 
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            island.gameObject.GetComponent<Island>().triggeredStart();
            if (island.transform.Find("GeneratorTowerObject") != null)
            {
                genTowers.Add(island.transform.Find("GeneratorTowerObject").gameObject);
            }
        }

        foreach (GameObject tower in genTowers)
        {
            tower.GetComponent<GeneratorTower>().TriggeredStart();
        }
	}
	
	// Update is called once per frame
	public void TriggeredUpdate ( Vector3 forceFieldPos ) 
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            island.gameObject.GetComponent<Island>().triggeredUpdate();
            if (island.gameObject.GetComponent<Island>().isGravityZoneEnemyFree())
            {
                island.gameObject.GetComponent<Island>().DestroyGenTower();
                genTowers.Remove(island.gameObject.GetComponent<Island>().getGenTower());
            }
        }

        if (rotateClockwise)
        {
            transform.Rotate(0, rotationSpeed / 30f, 0);
        }
        else
        {
            transform.Rotate(0, -rotationSpeed / 30f, 0);
        }

        if (allGenTowersDestroyed == false)
        {
            allGenTowersDestroyed = true;
            foreach (GameObject tower in genTowers)
            {
                tower.GetComponent<GeneratorTower>().setEndPoint(forceFieldPos);
                allGenTowersDestroyed = false;
            }
        }
	}

    public void RandomizeFloatDistances()
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            island.gameObject.GetComponent<Island>().distanceUpward += Random.Range(0, 10);
            island.gameObject.GetComponent<Island>().distanceDownward -= Random.Range(-10, 0);
        }
    }

    public void RandomizeFloatDirections()
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            int temp = Random.Range(-2, 2);
            if (temp <= 0)
            {
                island.gameObject.GetComponent<Island>().moveUpward = true;
            }
            else
            {
                island.gameObject.GetComponent<Island>().moveUpward = false;
            }
        }
    }

    public void RotateIslands()
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            float rotation = Random.Range(-180f, 180f);
            island.gameObject.transform.Rotate(0, rotation, 0);
        }
    }

    public void GenerateIslands()
    {
        GetPositions();

        for (int i = 0; i < islandAmount; i++)
        {
            _islandPrefab = Instantiate(islandPrefab, positions[i], Quaternion.identity);
            _islandPrefab.transform.SetParent(this.transform, true);
        }
    }

    public void EraseIslands()
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            DestroyImmediate(island.gameObject);
        }
    }

    public void GetPositions()
    {
        positions = new List<Vector3>();
        for (int i = 0; i < islandAmount; i++)
        {
            float angle = (1.0f * i) * (Mathf.PI * 2.0f) / (1.0f * islandAmount);
            float x = Mathf.Sin(angle) *RadiusX;
            float z = Mathf.Cos(angle) *RadiusZ;
            Vector3 pos = new Vector3(x, 0f, z) + centerPoint;
            positions.Add(pos);
        }
    }

    public void RandomizeHeights()
    {
        Vector3 temp = new Vector3(0, 0, 0);

        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            temp = island.position;
            temp.y += Random.Range(-40f, 40f);
            island.position = temp;
        }
    }

    public void RandomizeScales()
    {
        Vector3 temp = new Vector3(0, 0, 0);

        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            temp = island.localScale;
            temp.x += Random.Range(.1f, 10f);
            temp.y += Random.Range(.1f, 10f);
            temp.z += Random.Range(.1f, 10f);
            island.localScale = temp;
        }
    }

    public void RandomizeXY()
    {
        float random;
        Vector3 temp = new Vector3();

        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            random = Random.Range(-30f, 30f);
            temp = island.position;
            temp.x += random;
            random = Random.Range(-30f, 30f);
            temp.z += random;
            island.position = temp;
        }
    }
}
