using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    // Nonsense Variables {
    private int goofCount = 0;
    private float goofTimeLeft = 7f;
    private bool isGoofing = false;
    private Text goofText;
    // }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
    public void NextScene(Button button)
    {
        // Goof {
        goofText = button.GetComponentInChildren<Text>();
        if (goofCount == 0)
        {
            goofText.text = "Are you sure?";
        }
        else if (goofCount == 1)
        {
            goofText.text = "But are you really sure?";
        }
        else if (goofCount == 2)
        {
            goofText.text = "I dunno chief, I don't think you want to.";
        }
        else if (goofCount == 3)
        {
            goofText.text = "Oh I see, you meant to press quit, well you done got gamer goofed. I got you covered.";
            isGoofing = true;
        }

        goofCount++;
        // }

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ResetLevelGM()
    {
        SceneManager.LoadScene("Prototype");
        GameManager.instance.setLives += 3;
        GameManager.instance.setLives = GameManager.instance.lifePoints;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Goof {
    void Update()
    {
        if (isGoofing)
        {
            goofTimeLeft -= Time.deltaTime;
        }

        if (goofTimeLeft <= 3f && goofTimeLeft > 2f)
        {
            goofText.text = "in 3...";
        }
        else if (goofTimeLeft <= 2f && goofTimeLeft > 1f)
        {
            goofText.text = "2...";
        }
        else if (goofTimeLeft <= 1 && goofTimeLeft > 0f)
        {
            goofText.text = "1...";
        }
        else if (goofTimeLeft <= 0)
        {
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }
    // }
}
