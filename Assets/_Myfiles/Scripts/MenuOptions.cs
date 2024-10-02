using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit(1);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSongCreator()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
