using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

    public float distanceUpward = 10f;
    public float distanceDownward = 10f;
    public static float IslandMovementSpeed = 1;
    private float islandSpeed = IslandMovementSpeed / 45;
    public bool moveUpward = true;

    private Vector3 originalPos = new Vector3();
    private Vector3 pos = new Vector3();

    [HideInInspector]
    public bool wrongPlacement = false;

    public void triggeredStart()
    {
        originalPos = gameObject.transform.position;
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
}
