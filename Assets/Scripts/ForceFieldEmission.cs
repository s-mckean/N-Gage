using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldEmission : MonoBehaviour {


	// changing the emission intensity color
	Material material;

	Color emissionColor;
	float intensity;

	int numberOfGenerator;

	void Awake() {
		material = GetComponent<Renderer>().material;
		material.EnableKeyword("_EMISSION");
		
		emissionColor = material.GetColor("_EmissionColor");
		intensity = 2.0f;
	}

	void Start() {
		numberOfGenerator = GameObject.FindGameObjectsWithTag("Generator").Length;
	}

	// Update is called once per frame
	void Update () {
		int currentCount = GameObject.FindGameObjectsWithTag("Generator").Length;
		// decrease intensity if number of generator change
		if(currentCount != numberOfGenerator) {
			float scalar = (float)currentCount/(float)numberOfGenerator;
			emissionColor.r = emissionColor.r * intensity * scalar;
			emissionColor.g = emissionColor.g * intensity * scalar;
			emissionColor.b = emissionColor.b * intensity * scalar;
			material.SetColor("_EmissionColor", emissionColor);

			numberOfGenerator = currentCount;
		}
	}
}
