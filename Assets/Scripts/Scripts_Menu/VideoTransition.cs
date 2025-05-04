using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

// Aquesta classe gestiona la transici� entre una escena amb v�deo i una altra escena
public class VideoSceneController : MonoBehaviour
{
    // Refer�ncia al component VideoPlayer que reproduir� el v�deo a l�escena
    public VideoPlayer videoPlayer;
    

    void Start()
    {
        // Es registra el m�tode EndReached perqu� s�executi quan el v�deo arribi al final
        videoPlayer.loopPointReached += EndReached;
    }

    // Aquest m�tode es crida quan el v�deo ha acabat de reproduir-se
    void EndReached(VideoPlayer vp)
    {
        // Carrega l�escena anomenada "Nivell 2" un cop finalitza el v�deo
        SceneManager.LoadScene("Nivell 2");
    }
}

