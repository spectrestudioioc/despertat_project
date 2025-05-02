using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Nivell 1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu 1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
        // Aquest mètode només funciona amb la build.
        Debug.Log("Joc Tancat");
    }
}
