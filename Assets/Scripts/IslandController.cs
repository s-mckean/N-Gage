using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IslandController : MonoBehaviour, IslandInterface {

    public GameObject islandPrefab;
    private IslandController Instance;
    public int islandAmount = 5;
    private int islandSpacing = 2;
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
	}

    public void GenerateIslands()
    {
        islandWidth = islandPrefab.GetComponent<MeshRenderer>().bounds.size.x;
        islandHeight = islandPrefab.GetComponent<MeshRenderer>().bounds.size.y;
        numIsland = islandAmount - 1;

        GetPositions();

        for (int i = 0; i < islandAmount; i++)
        {
            GameObject temp = Instantiate(islandPrefab, positions[i], Quaternion.identity);
            temp.transform.SetParent(this.gameObject.transform, true);
            islands.Add(temp);
        }

        if (DoIslandsOverlap())
        {
            bool notDone = true;

            do
            {
                islandSpacing++;
                GetPositions();

                for (int i = 0; i < islandAmount; i++)
                {
                    islands[i].transform.position = positions[i];
                }

                if (!DoIslandsOverlap())
                {
                    notDone = false;
                }
            }
            while (notDone);
        }
    }

    public void EraseIslands()
    {
        foreach (GameObject child in islands)
        {
            DestroyImmediate(child);
        }
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

    public bool DoIslandsOverlap()
    {
        /*
        for (int i = 0; i < islandAmount; i++)
        {
            if (islands[i].GetComponent<Island>().wrongPlacement == true)
            {
                return true;
            }
        }
         * */
        return false;
    }
}
