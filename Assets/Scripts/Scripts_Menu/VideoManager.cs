using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class VideoIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject skipButton; // Asigna el botón desde el inspector
    public float delayBeforeShowingButton = 5f;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;

        if (skipButton != null)
        {
            skipButton.SetActive(false); // Ocultar botón al principio
            StartCoroutine(ShowSkipButtonAfterDelay());
        }
    }

    IEnumerator ShowSkipButtonAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeShowingButton);
        skipButton.SetActive(true);
    }

    public void SkipIntro()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu");
    }
}
