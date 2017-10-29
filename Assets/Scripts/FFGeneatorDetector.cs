using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFGeneatorDetector : MonoBehaviour {

		
	// Update is called once per frame
	void Update () {
		// once the number of geneator is zero destroy force field
		if(GameObject.FindGameObjectsWithTag("Generator").Length <= 0) {
			Destroy(gameObject);
		}
	}
}
