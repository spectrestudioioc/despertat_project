using UnityEngine;
using UnityEngine.SceneManagement;

public class TornarAlMenuPrincipal : MonoBehaviour
{
    // Aquest mètode es pot assignar al botó des de l'Inspector
    public void CarregarMenuPrincipal()
    {
        Time.timeScale = 1f; // Assegura't que el joc no estigui pausat
        StartCoroutine(RetardarCarrega()); // Comença la rutina per esperar
    }

    private System.Collections.IEnumerator RetardarCarrega()
    {
        yield return new WaitForSeconds(2f); // Espera 1 segon
        SceneManager.LoadScene("MainMenu");  // Carrega l'escena "MainMenu"
    }
}
