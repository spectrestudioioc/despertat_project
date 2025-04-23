using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [SerializeField] private GameObject canvasObject; // Camp per inscriure el canvas
    [SerializeField] private GameObject AudioObject; // Camp per inscriure el �udio

    void Start() // Event per quan el v�deo acaba de reprodu�r
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    public void PlayVideo()
    {
        Invoke("HideCanvasAndPlayVideo", 1.5f);  // Espera 1 segon en fer la transici�
    }

    // M�tode per reprodu�r el v�deo i amagar el canvas
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
        videoPlayer.Play();  // Reprodu�r V�deo
    }

    // M�tode que s'activa quan el v�deo finalitza
    private void OnVideoFinished(VideoPlayer vp)
    {
        Invoke("ShowCanvasAndStopVideo", 1.5f); // Espera 1 segon abans de mostrar el Canvas i parar el v�deo
    }

    // M�tode separat per mostrar el canvas despr�s del delay
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

        videoPlayer.Stop(); // Atura el v�deo
    }
}
