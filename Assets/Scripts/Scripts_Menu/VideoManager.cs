using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [SerializeField] private GameObject canvasObject; // Camp per inscriure el canvas
    [SerializeField] private GameObject AudioObject; // Camp per inscriure el àudio

    void Start() // Event per quan el vídeo acaba de reproduïr
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public void PlayVideo()
    {
        Invoke("HideCanvasAndPlayVideo", 1.5f);  // Espera 1 segon en fer la transició
    }

    // Mètode per reproduïr el vídeo i amagar el canvas
    private void HideCanvasAndPlayVideo()
    {
        if (canvasObject!= null)
        {
            canvasObject.SetActive(false);  // Amagar Canvas
        }

        if (AudioObject != null)
        {
            AudioObject.SetActive(false);  // Amagar Canvas
        }
        videoPlayer.Play();  // Reproduïr Vídeo
    }

    // Mètode que s'activa quan el vídeo finalitza
    private void OnVideoFinished(VideoPlayer vp)
    {
        Invoke("ShowCanvasAndStopVideo", 1.5f); // Espera 1 segon abans de mostrar el Canvas i parar el vídeo
    }

    // Mètode separat per mostrar el canvas després del delay
    private void ShowCanvasAndStopVideo()
    {
        if (canvasObject != null)
        {
            canvasObject.SetActive(true);  // Mostra el Canvas
        }

        if (AudioObject != null)
        {
            AudioObject.SetActive(true);
        }

        videoPlayer.Stop(); // Atura el vídeo
    }
}
