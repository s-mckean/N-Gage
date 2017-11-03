using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandRing : MonoBehaviour
{

    public GameObject islandPrefab;
    private GameObject _islandPrefab;

    public float RadiusX = 1;
    public float RadiusZ = 1;
    public bool rotateClockwise = true;
    public float rotationSpeed = 1f;
    public int islandAmount = 5;
    public int genTowerAmount = 0;

    public Vector3 centerPoint = new Vector3(0, 0, 0);

    public List<Vector3> positions = new List<Vector3>();
    private List<GeneratorTower> genTowers = new List<GeneratorTower>();
    private List<Island> islands = new List<Island>();

    // Use this for initialization
    public void TriggeredStart(Transform player)
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if (island.GetComponent<Island>() != null)
            {
                island.GetComponent<Island>().triggeredStart(player);
                if (island.transform.Find("GeneratorTowerObject") != null)
                {
                    genTowers.Add(island.transform.Find("GeneratorTowerObject").GetComponent<GeneratorTower>());
                }
                islands.Add(island.GetComponent<Island>());
            }
        }

        foreach (GeneratorTower tower in genTowers)
        {
            tower.TriggeredStart();
        }
        genTowerAmount = genTowers.Count;
    }

    // Update is called once per frame
    public void TriggeredUpdate(Vector3 forceFieldPos)
    {
        foreach (Island island in islands)
        {
            island.triggeredUpdate(forceFieldPos);

            if (island.getGenTower() != null)
            {
                if (island.isGravityZoneEnemyFree())
                {
                    GameObject temp = island.getGenTower().gameObject;
                    genTowers.Remove(island.getGenTower());
                    Destroy(temp);
                    genTowerAmount = genTowers.Count;
                }
            }
        }

        if (rotateClockwise)
        {
            this.transform.Rotate(0, rotationSpeed / 30f, 0);
        }
        else
        {
            this.transform.Rotate(0, -rotationSpeed / 30f, 0);
        }
    }

    public void RandomizeFloatDistances()
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if (island.GetComponent<Island>() != null)
            {
                island.GetComponent<Island>().distanceUpward += Random.Range(0, 10);
                island.GetComponent<Island>().distanceDownward -= Random.Range(-10, 0);
            }
        }
    }

    public void RandomizeFloatDirections()
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if (island.GetComponent<Island>() != null)
            {
                int temp = Random.Range(-2, 2);
                if (temp <= 0)
                {
                    island.GetComponent<Island>().moveUpward = true;
                }
                else
                {
                    island.GetComponent<Island>().moveUpward = false;
                }
            }
        }
    }

    public void RotateIslands()
    {
        foreach (Transform island in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if (island.GetComponent<Island>() != null)
            {
                float rotation = Random.Range(-180f, 180f);
                island.gameObject.transform.Rotate(0, rotation, 0);
            }
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
        findIslands();

        foreach (Island island in islands)
        {
            DestroyImmediate(island.gameObject);
        }
    }

    public void findIslands()
    {
        islands = new List<Island>();
        foreach (Transform thing in this.gameObject.GetComponentsInChildren<Transform>())
        {
            if (thing.GetComponent<Island>() != null)
            {
                islands.Add(thing.GetComponent<Island>());
            }
        }
    }

    public void GetPositions()
    {
        positions = new List<Vector3>();
        for (int i = 0; i < islandAmount; i++)
        {
            float angle = (1.0f * i) * (Mathf.PI * 2.0f) / (1.0f * islandAmount);
            float x = Mathf.Sin(angle) * RadiusX;
            float z = Mathf.Cos(angle) * RadiusZ;
            Vector3 pos = new Vector3(x, 0f, z) + centerPoint;
            positions.Add(pos);
        }
    }

    public void RandomizeHeights()
    {
        Vector3 temp = new Vector3(0, 0, 0);
        findIslands();

        foreach (Island island in islands)
        {
            temp = island.transform.position;
            temp.y += Random.Range(-40f, 40f);
            island.transform.position = temp;

        }
    }

    public void RandomizeScales()
    {
        Vector3 temp = new Vector3(0, 0, 0);
        findIslands();

        foreach (Island island in islands)
        {
                temp = island.transform.localScale;
                temp.x += Random.Range(.1f, 10f);
                temp.y += Random.Range(.1f, 10f);
                temp.z += Random.Range(.1f, 10f);
                island.transform.localScale = temp;

        }
    }

    public void RandomizeXY()
    {
        float random;
        Vector3 temp = new Vector3();

        findIslands();

        foreach (Island island in islands)
        {
                random = Random.Range(-30f, 30f);
                temp = island.transform.position;
                temp.x += random;
                random = Random.Range(-30f, 30f);
                temp.z += random;
                island.transform.position = temp;
        }
    }
}
