using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IslandController : MonoBehaviour
{
    public GameObject islandRingPrefab;
    private GameObject _islandRingPrefab;

    public GameObject initialIsland;
    public GameObject ringMaster;
    private GameObject forceField;

    private IslandController Instance;
    public int ringAmount = 3;
    private bool allGenTowersDestroyed = false;

    public GameObject player;

    private List<GameObject> rings = new List<GameObject>();

    private int genTowerAmount = 0;
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

        //initialIsland.GetComponent<InitialIsland>().TriggeredStart();
        forceField = initialIsland.transform.Find("ForceField").gameObject;

        foreach (Transform ring in ringMaster.GetComponentsInChildren<Transform>())
        {
            if (ring.gameObject.GetComponent<IslandRing>() != null)
            {
                ring.gameObject.GetComponent<IslandRing>().TriggeredStart(player.transform);
            }
        }
    }

    void FixedUpdate()
    {
        //initialIsland.GetComponent<InitialIsland>().TriggeredUpdate();

        allGenTowersDestroyed = true;
        genTowerAmount = 0;
        foreach (Transform ring in ringMaster.GetComponentsInChildren<Transform>())
        {
            if (ring.gameObject.GetComponent<IslandRing>() != null)
            {
                if (forceField != null)
                {
                    ring.gameObject.GetComponent<IslandRing>().TriggeredUpdate(forceField.transform.position);
                }
                if (ring.gameObject.GetComponent<IslandRing>().genTowerAmount > 0)
                {
                    allGenTowersDestroyed = false;
                }
                genTowerAmount += ring.gameObject.GetComponent<IslandRing>().genTowerAmount;
            }
        }
        towerAmount.text = genTowerAmount.ToString();


        if (allGenTowersDestroyed == true)
        {
            ReleaseBossEnemy();
        }
    }

    public void ReleaseBossEnemy()
    {
        if (forceField != null)
        {
            Destroy(forceField);
        }
    }

    public void GenerateRings()
    {
        for (int i = 1; i < ringAmount + 1; i++)
        {
            _islandRingPrefab = Instantiate(islandRingPrefab, initialIsland.transform.position, Quaternion.identity);
            _islandRingPrefab.GetComponent<IslandRing>().RadiusX = i * initialIsland.GetComponent<MeshRenderer>().bounds.size.x * 2;
            _islandRingPrefab.GetComponent<IslandRing>().RadiusZ = i * initialIsland.GetComponent<MeshRenderer>().bounds.size.x * 2;
            _islandRingPrefab.transform.SetParent(ringMaster.transform, true);
            rings.Add(_islandRingPrefab);
        }
    }

    public void EraseRings()
    {
        foreach (Transform ring in ringMaster.GetComponentsInChildren<Transform>())
        {
            if (ring.GetComponent<IslandRing>() != null)
            {
                DestroyImmediate(ring.gameObject);
            }
        }
    }
}
