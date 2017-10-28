using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTower : MonoBehaviour {

    private GameObject lightningBolt;
    private GameObject endPoint;

	// Use this for initialization
	public void TriggeredStart () 
    {
        lightningBolt = this.transform.Find("SimpleLightningBoltPrefab").gameObject;
        endPoint = lightningBolt.transform.Find("LightningEnd").gameObject;
	}

    public void setEndPoint(Vector3 pos)
    {
        endPoint.transform.position = pos;
    }
}
