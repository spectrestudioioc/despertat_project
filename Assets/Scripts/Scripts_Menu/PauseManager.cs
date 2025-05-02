using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPausa; // Assigna el panell de pausa des de l'Inspector
    private bool jocPausat = false;

    void Update()
    {
        // Comprova si la tecla "Tab" s'ha premut
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Si el joc est� pausat, es repr�n; si no, es pausa
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

    void Pausar()
    {
        menuPausa.SetActive(true); // Mostra el men� de pausa
        Time.timeScale = 0f; // Pausa el temps del joc

        // Canvia l'estat del cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Marca el joc com a pausat
        jocPausat = true;
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
