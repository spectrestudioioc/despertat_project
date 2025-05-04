using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

// Aquesta classe gestiona la transició entre una escena amb vídeo i una altra escena
public class VideoSceneController : MonoBehaviour
{
    // Referència al component VideoPlayer que reproduirà el vídeo a l’escena
    public VideoPlayer videoPlayer;
    

    void Start()
    {
        // Es registra el mètode EndReached perquè s’executi quan el vídeo arribi al final
        videoPlayer.loopPointReached += EndReached;
    }

    // Aquest mètode es crida quan el vídeo ha acabat de reproduir-se
    void EndReached(VideoPlayer vp)
    {
        // Carrega l’escena anomenada "Nivell 2" un cop finalitza el vídeo
        SceneManager.LoadScene("Nivell 2");
    }
}

