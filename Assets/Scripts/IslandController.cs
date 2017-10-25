using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IslandController : MonoBehaviour {

    public GameObject islandPrefab;
    private GameObject _islandPrefab;

    private IslandController Instance;
    public int islandAmount = 5;
    public float islandSpacing = 4;
    private float islandWidth;
    private float islandHeight;
    private int numIsland;

    private List<Vector3> positions = new List<Vector3>();
    private List<GameObject> islands = new List<GameObject>();

	// Use this for initialization
	void Start () {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        CheckForIslands();
	}

    public void GenerateIslands()
    {
        islandWidth = islandPrefab.GetComponent<MeshRenderer>().bounds.size.x;
        islandHeight = islandPrefab.GetComponent<MeshRenderer>().bounds.size.y;
        numIsland = islandAmount - 1;

        GetPositions();

        for (int i = 0; i < islandAmount; i++)
        {
            _islandPrefab = Instantiate(islandPrefab, positions[i], Quaternion.identity);
            _islandPrefab.transform.SetParent(this.gameObject.transform, true);
            islands.Add(_islandPrefab);
        }
    }

    public void EraseIslands()
    {
        CheckForIslands();
        foreach (GameObject child in islands)
        {
            DestroyImmediate(child);
        }
        islands = new List<GameObject>();
    }

    public void GetPositions()
    {
        float radius = islandWidth * islandSpacing;

        positions = new List<Vector3>();
        positions.Add(new Vector3(0, 0, 0));

        for (int i = 0; i < numIsland; i++)
        {
            var angle = i * Mathf.PI * 2 / numIsland;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            positions.Add(pos);
        }
    }

    public void RandomizeHeights()
    {
        CheckForIslands();
        Vector3 temp = new Vector3(0,0,0);      

        foreach (GameObject island in islands)
        {
            temp = island.transform.position;
            temp.y += Random.Range(-6f, 6f);
            island.transform.position = temp;
        }
    }

    public void RandomizeScales()
    {
        CheckForIslands();
        Vector3 temp = new Vector3(0,0,0);

        foreach (GameObject island in islands)
        {
            temp = island.transform.localScale;
            temp.x += Random.Range(.1f, 1f);
            temp.y += Random.Range(.1f, 1f);
            temp.z += Random.Range(.1f, 1f);
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
        if (islands.Count == 0)
        {
            islands = new List<GameObject>();
            islandAmount = 0;
            foreach (Transform child in transform)
            {
                islandAmount++;
                islands.Add(child.gameObject);
            }
        }
    }
}
