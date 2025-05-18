using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class VideoIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reproductor de v�deo assignat des de l'Inspector
    public GameObject skipButton; // Bot� per saltar la introducci�, assignat des de l'Inspector
    public float delayBeforeShowingButton = 5f; // Retard abans de mostrar el bot� de salt

    void Start()
    {
        // Assigna una funci� que es crida quan el v�deo acaba
        videoPlayer.loopPointReached += OnVideoFinished;

        if (skipButton != null)
        {
            skipButton.SetActive(false); // Amaga el bot� al comen�ar
            StartCoroutine(ShowSkipButtonAfterDelay()); // Comen�a la coroutine per mostrar el bot� despr�s d'un retard
        }
    }

    // Coroutine que espera uns segons abans de mostrar el bot� de salt
    IEnumerator ShowSkipButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeShowingButton); // Espera el temps especificat
        skipButton.SetActive(true); // Mostra el bot�
    }

    // Funci� per saltar la introducci� i carregar el men� principal
    public void SkipIntro()
    {
        SceneManager.LoadScene("MainMenu"); // Carrega l�escena del men� principal
    }

    // Funci� que es crida autom�ticament quan el v�deo s�acaba
    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu"); // Carrega l�escena del men� principal
    }
}
