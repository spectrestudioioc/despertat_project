using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup; // Imatge negra per fer el fade
    public float fadeDuration = 1f;     // Temps que dura el fade
    public GameObject uiGroup;          // Conté la UI (botons, etc.)

    private void Start()
    {
        StartCoroutine(FadeIn()); // Comencem amb un fade-in
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName)); // Crida des d’un botó
    }

    public IEnumerator FadeIn()
    {
        uiGroup.SetActive(false); // Amaguem la UI al principi

        float t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            fadeCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        fadeCanvasGroup.alpha = 0;
        fadeCanvasGroup.blocksRaycasts = false; // Permetem clics
        uiGroup.SetActive(true); // Mostrem la UI
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadeCanvasGroup.blocksRaycasts = true; // Bloquegem clics

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        fadeCanvasGroup.alpha = 1;
        SceneManager.LoadScene(sceneName); // Carreguem l'escena
    }
}

