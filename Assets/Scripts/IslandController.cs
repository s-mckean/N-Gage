using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor;

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

        initialIsland.GetComponent<InitialIsland>().TriggeredStart();
        forceField = initialIsland.transform.Find("ForceField").gameObject;

        foreach (GameObject ring in rings)
        {
            ring.GetComponent<IslandRing>().TriggeredStart();
        }
    }

    void FixedUpdate()
    {
        initialIsland.GetComponent<InitialIsland>().TriggeredUpdate();
        
        allGenTowersDestroyed = true;
        genTowerAmount = 0;
        foreach( GameObject ring in rings )
        {
            ring.GetComponent<IslandRing>().TriggeredUpdate( forceField.transform.position );
            if( ring.GetComponent<IslandRing>().allGenTowersDestroyed == false )
            {
                allGenTowersDestroyed = false;
            }
            genTowerAmount += ring.GetComponent<IslandRing>().genTowerAmount;
        }

        if( allGenTowersDestroyed == true )
        {
            ReleaseBossEnemy();
        }

        towerAmount.text = genTowerAmount.ToString();
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
        for (int i = 1; i < ringAmount+1; i++)
        {
            _islandRingPrefab = Instantiate(islandRingPrefab, initialIsland.transform.position, Quaternion.identity);
            _islandRingPrefab.GetComponent<IslandRing>().RadiusX = i * initialIsland.GetComponent<MeshRenderer>().bounds.size.x*2;
            _islandRingPrefab.GetComponent<IslandRing>().RadiusZ = i * initialIsland.GetComponent<MeshRenderer>().bounds.size.x*2;
            _islandRingPrefab.transform.SetParent(ringMaster.transform, true);
            rings.Add(_islandRingPrefab);
        }
    }

    public void EraseRings()
    {
        foreach (Transform ring in ringMaster.GetComponentsInChildren<Transform>())
        {
            DestroyImmediate(ring.gameObject);
        }
    }
}
