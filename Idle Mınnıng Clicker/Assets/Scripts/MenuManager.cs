using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Method to handle the Start button click
    public void StartButton()
    {
        // Load the scene with index 1
        SceneManager.LoadScene(1);
    }

    // Method to handle the Exit button click
    public void ExitButton()
    {
        // Quit the application
        Application.Quit();
    }
}
