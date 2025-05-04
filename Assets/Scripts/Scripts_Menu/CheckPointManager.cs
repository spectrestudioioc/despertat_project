using UnityEngine;
using UnityEngine.SceneManagement;

// Aquesta classe s'encarrega de carregar una nova escena quan el jugador entra en una zona espec�fica (trigger)
public class LevelTransition : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        // Comprova si l'objecte que ha entrat al trigger t� l'etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Si �s el jugador, carrega l'escena anomenada "Video3Scene"
            SceneManager.LoadScene("Video3Scene");
        }
    }
}
