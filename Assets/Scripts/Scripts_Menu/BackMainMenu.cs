using UnityEngine;
using UnityEngine.SceneManagement;

public class TornarAlMenuPrincipal : MonoBehaviour
{
    // Assignar al bot� des de l'Inspector
    public void CarregarMenuPrincipal()
    {
        Time.timeScale = 1f; // Assegura que el joc no estigui pausat
        StartCoroutine(RetardarCarrega()); // Comen�a la rutina per esperar
    }

    private System.Collections.IEnumerator RetardarCarrega()
    {
        yield return new WaitForSeconds(2f); // Espera 1 segon
        SceneManager.LoadScene("MainMenu");  // Carrega l'escena "MainMenu"
    }
}
