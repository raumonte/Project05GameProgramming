using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ResetLevelGM()
    {
        SceneManager.LoadScene("Prototype");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
