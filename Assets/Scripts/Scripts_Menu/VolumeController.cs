using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider; // Camp per inscriure l'slider

    void Start()
    {
        // Asignar valor inicial segons volum
        volumeSlider.value = AudioListener.volume;

        // Actualitza el volum amb el handler de l'slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // M�tode per gestionar el valor d'�udio
    private void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
