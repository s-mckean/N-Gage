using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    // Use this with buttons for switching scenes
    public void SwitchScene(string name)
    {
        Time.timeScale = 1;     // In case a different scene is loaded without resuming so everything isn't frozen
        SceneManager.LoadScene(name);
    }

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void quitApp()
    {
        Application.Quit();
    }
}
