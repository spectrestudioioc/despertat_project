using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPausa; // Assigna el panell de pausa des de l'Inspector
    private bool jocPausat = false;
    private string escenaActual;

    void Update()
    {

        // Comprova el nom de l'escena actual
        string escenaActual = SceneManager.GetActiveScene().name;

        // Nom�s permet pausar si est�s a Nivell1 o Nivell2
        if (escenaActual == "Nivell 1" || escenaActual == "Nivell 2")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (jocPausat)
                {
                    Reprendre();
                }
                else
                {
                    Pausar();
                }
            }
        }
    }

    void Pausar()
    {
        menuPausa.SetActive(true); // Mostra el men� de pausa
        Time.timeScale = 0f; // Pausa el temps del joc

        // Canvia l'estat del cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Marca el joc com a pausat
        jocPausat = true;

        // Actualitza l'estat de les icones del diari
        foreach (IconaDiariController icona in FindObjectsOfType<IconaDiariController>())
        {
            icona.ActualitzaEstat();
        }
    }

    public void Reprendre()
    {
        menuPausa.SetActive(false); // Amaga el men� de pausa
        Time.timeScale = 1f; // Repr�n el temps del joc

        // Restaura l'estat del cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Marca el joc com a repr�s
        jocPausat = false;
    }
}
