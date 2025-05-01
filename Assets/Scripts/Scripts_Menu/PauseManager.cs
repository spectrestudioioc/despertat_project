using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;        // El Canvas del menú de pausa
    public GameObject defaultButton;      // El primer botón del menú (opcional)
    public CanvasGroup canvasGroup;       // El CanvasGroup que controlará el fade
    private bool isPaused = false;        // Estado de la pausa
    public float fadeDuration = 0.5f;     // Duración del fade en segundos

    void Start()
    {
        // Aseguramos que el menú esté oculto al inicio
        pauseMenuUI.SetActive(false);
        canvasGroup.alpha = 0f;  // Lo ponemos completamente transparente
        canvasGroup.blocksRaycasts = false;  // Desactiva la interacción con el UI al inicio
    }

    void Update()
    {
        // Si presionamos la tecla TAB, alternamos entre pausar o reanudar el juego
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pausar el juego
    void PauseGame()
    {
        // Mostrar el menú de pausa con fade
        pauseMenuUI.SetActive(true);
        StartCoroutine(FadeMenu(1f));  // Fade In

        // Pausar el tiempo del juego
        Time.timeScale = 0f;
        isPaused = true;

        // Asegurar que el primer botón sea seleccionado
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);  // Desmarcar cualquier botón seleccionado
            EventSystem.current.SetSelectedGameObject(defaultButton); // Seleccionar el primer botón
        }
    }

    // Reanudar el juego
    void ResumeGame()
    {
        // Hacer fade out y ocultar el menú
        StartCoroutine(FadeMenu(0f));  // Fade Out

        // Reanudar el tiempo del juego
        Time.timeScale = 1f;
        isPaused = false;
    }

    // Coroutine para hacer el fade
    private IEnumerator FadeMenu(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0f;

        // Activar la interacción con el UI
        canvasGroup.blocksRaycasts = true;

        // Realizar la transición de opacidad
        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        // Desactivar la interacción con el UI cuando el fade termine si estamos cerrando el menú
        if (targetAlpha == 0f)
        {
            canvasGroup.blocksRaycasts = false;
            pauseMenuUI.SetActive(false);
        }
    }
}
