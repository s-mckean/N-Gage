using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    public Text TutorialText;
    public string message;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            TutorialText.text = message;
        }
    }
}
