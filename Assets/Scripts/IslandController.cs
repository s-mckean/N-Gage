using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IslandController : MonoBehaviour
{

    public GameObject islandPrefab;
    private GameObject _islandPrefab;
    public GameObject islandRing;

    private GameObject initialIsland;
    private GameObject forceField;

    private IslandController Instance;
    public int islandAmount = 5;
    public float islandSpacing = 4;
    public bool rotateClockwise = true;
    public float rotationSpeed = 1f;
    private float islandWidth;
    private float islandHeight;
    private float radius;
    private float spinOffset = 0f;
    private bool allGenTowersDestroyed = false;

    private List<Vector3> positions = new List<Vector3>();
    private List<GameObject> islands = new List<GameObject>();
    private List<GameObject> genTowers = new List<GameObject>();

    public Text towerAmount;

    // Use this for initialization
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        CheckForIslands();

        initialIsland.GetComponent<InitialIsland>().TriggeredStart();
        forceField = initialIsland.transform.Find("ForceField").gameObject;

        foreach (GameObject island in islands)
        {
            island.GetComponent<Island>().triggeredStart();
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

    void FixedUpdate()
    {
        foreach (GameObject island in islands)
        {
            island.GetComponent<Island>().triggeredUpdate();
            if (island.GetComponent<Island>().isGravityZoneEnemyFree())
            {
                island.GetComponent<Island>().DestroyGenTower();
                genTowers.Remove(island.GetComponent<Island>().getGenTower());
            }
        }

        if (rotateClockwise)
        {
            islandRing.transform.Rotate(0, rotationSpeed / 30f, 0);
        }
        else
        {
            islandRing.transform.Rotate(0, -rotationSpeed / 30f, 0);
        }

        initialIsland.GetComponent<InitialIsland>().TriggeredUpdate();

        if (allGenTowersDestroyed == false)
        {
            allGenTowersDestroyed = true;
            foreach (GameObject tower in genTowers)
            {
                tower.GetComponent<GeneratorTower>().setEndPoint(forceField.transform.position);
                allGenTowersDestroyed = false;
            }
        }
        else
        {
            ReleaseBossEnemy();
        }

        towerAmount.text = genTowers.Count.ToString();
    }

    public void ReleaseBossEnemy()
    {
        if (forceField != null)
        {
            Destroy(forceField);
        }
    }

    public void RandomizeFloatDistances()
    {
        CheckForIslands();

        foreach (GameObject island in islands)
        {
            island.GetComponent<Island>().distanceUpward += Random.Range(0, 10);
            island.GetComponent<Island>().distanceDownward -= Random.Range(-10, 0);
        }
    }

    public void RandomizeFloatDirections()
    {
        CheckForIslands();

        foreach (GameObject island in islands)
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

    public void RotateIslands()
    {
        CheckForIslands();

        foreach (GameObject island in islands)
        {
            float rotation = Random.Range(-180f, 180f);
            island.transform.Rotate(0, rotation, 0);
        }
    }

    public void GenerateIslands()
    {
        initialIsland = this.gameObject.transform.Find("InitialIsland").gameObject;

        islandWidth = initialIsland.GetComponent<MeshRenderer>().bounds.size.x;
        islandHeight = initialIsland.GetComponent<MeshRenderer>().bounds.size.y;

        GetPositions();

        for (int i = 0; i < islandAmount; i++)
        {
            _islandPrefab = Instantiate(islandPrefab, positions[i], Quaternion.identity);
            _islandPrefab.transform.SetParent(islandRing.transform, true);
            islands.Add(_islandPrefab);
        }
    }

    public void EraseIslands()
    {
        CheckForIslands();
        foreach (GameObject island in islands)
        {
            DestroyImmediate(island);
        }
        islands = new List<GameObject>();
    }

    public void GetPositions()
    {
        radius = islandWidth * islandSpacing;

        positions = new List<Vector3>();
        for (int i = 0; i < islandAmount; i++)
        {
            float angle = ((1.0f * i) + spinOffset) * (Mathf.PI * 2.0f) / (1.0f * islandAmount);
            float x = Mathf.Sin(angle) * radius;// *radiusX;
            float z = Mathf.Cos(angle) * radius;// *radiusZ;
            Vector3 pos = new Vector3(x, 0f, z) + initialIsland.transform.position;
            positions.Add(pos);
        }
    }

    public void GetPositions(float Offset)
    {
        radius = islandWidth * islandSpacing;

        positions = new List<Vector3>();
        for (int i = 0; i < islandAmount; i++)
        {
            var angle = ((float)i + Offset) * Mathf.PI * 2 / islandAmount;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            positions.Add(pos);
        }
    }

    public void RandomizeHeights()
    {
        CheckForIslands();
        Vector3 temp = new Vector3(0, 0, 0);

        foreach (GameObject island in islands)
        {
            temp = island.transform.position;
            temp.y += Random.Range(-40f, 40f);
            island.transform.position = temp;
        }
    }

    public void RandomizeScales()
    {
        CheckForIslands();
        Vector3 temp = new Vector3(0, 0, 0);

        foreach (GameObject island in islands)
        {
            temp = island.transform.localScale;
            temp.x += Random.Range(.1f, 10f);
            temp.y += Random.Range(.1f, 10f);
            temp.z += Random.Range(.1f, 10f);
            island.transform.localScale = temp;
        }
    }

    public void IncreaseRadius()
    {
        CheckForIslands();
        islandSpacing += 1f;
        GetPositions();
        for (int i = 0; i < islandAmount; i++)
        {
            islands[i].transform.position = positions[i];
        }
    }

    public void CheckForIslands()
    {
        initialIsland = this.gameObject.transform.Find("InitialIsland").gameObject;

        if (islands.Count == 0)
        {
            islands = new List<GameObject>();
            islandAmount = 0;
            foreach (Transform island in islandRing.GetComponentsInChildren<Transform>())
            {
                if (island.gameObject.tag == "Island")
                {
                    islandAmount++;
                    islands.Add(island.gameObject);
                }
            }
        }
    }

    public void RandomizeXY()
    {
        float random;
        Vector3 temp = new Vector3();

        foreach (GameObject island in islands)
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
