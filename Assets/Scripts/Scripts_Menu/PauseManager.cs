using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenuUI;        // El Canvas del men� de pausa
    public GameObject defaultButton;      // El primer bot�n del men� (opcional)
    public CanvasGroup canvasGroup;       // El CanvasGroup que controlar� el fade
    private bool isPaused = false;        // Estado de la pausa
    public float fadeDuration = 0.5f;     // Duraci�n del fade en segundos

    void Start()
    {
        // Aseguramos que el men� est� oculto al inicio
        pauseMenuUI.SetActive(false);
        canvasGroup.alpha = 0f;  // Lo ponemos completamente transparente
        canvasGroup.blocksRaycasts = false;  // Desactiva la interacci�n con el UI al inicio
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
        // Mostrar el men� de pausa con fade
        pauseMenuUI.SetActive(true);
        StartCoroutine(FadeMenu(1f));  // Fade In

        // Pausar el tiempo del juego
        Time.timeScale = 0f;
        isPaused = true;

        // Asegurar que el primer bot�n sea seleccionado
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);  // Desmarcar cualquier bot�n seleccionado
            EventSystem.current.SetSelectedGameObject(defaultButton); // Seleccionar el primer bot�n
        }
    }

    // Reanudar el juego
    void ResumeGame()
    {
        // Hacer fade out y ocultar el men�
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

        // Activar la interacci�n con el UI
        canvasGroup.blocksRaycasts = true;

        // Realizar la transici�n de opacidad
        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        // Desactivar la interacci�n con el UI cuando el fade termine si estamos cerrando el men�
        if (targetAlpha == 0f)
        {
            canvasGroup.blocksRaycasts = false;
            pauseMenuUI.SetActive(false);
        }
    }
}
