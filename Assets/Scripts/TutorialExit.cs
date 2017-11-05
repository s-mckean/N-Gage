using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialExit : MonoBehaviour {

    public string mainMenu;

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(EndTutorial());
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(mainMenu);
    }
}
