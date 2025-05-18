using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class VideoIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reproductor de vídeo assignat des de l'Inspector
    public GameObject skipButton; // Botó per saltar la introducció, assignat des de l'Inspector
    public float delayBeforeShowingButton = 5f; // Retard abans de mostrar el botó de salt

    void Start()
    {
        // Assigna una funció que es crida quan el vídeo acaba
        videoPlayer.loopPointReached += OnVideoFinished;

        if (skipButton != null)
        {
            skipButton.SetActive(false); // Amaga el botó al començar
            StartCoroutine(ShowSkipButtonAfterDelay()); // Comença la coroutine per mostrar el botó després d'un retard
        }
    }

    // Coroutine que espera uns segons abans de mostrar el botó de salt
    IEnumerator ShowSkipButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeShowingButton); // Espera el temps especificat
        skipButton.SetActive(true); // Mostra el botó
    }

    // Funció per saltar la introducció i carregar el menú principal
    public void SkipIntro()
    {
        SceneManager.LoadScene("MainMenu"); // Carrega l’escena del menú principal
    }

    // Funció que es crida automàticament quan el vídeo s’acaba
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu"); // Carrega l’escena del menú principal
    }
}
