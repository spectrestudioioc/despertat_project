using UnityEngine;

// Classe que gestiona el so dels enemics quan el jugador entra en una àrea de detecció
public class EnemySound : MonoBehaviour
{
    // Referència a l'objecte AudioSource que conté el so dels enemics
    public AudioSource Audio;

    // Variable per controlar si ja ha reproduït el so
    private bool hasAudio = false;

    // Es crida quan un altre collider entra dins del trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si encara no reproduït el so i el collider pertany al jugador
        if (!hasAudio && other.CompareTag("Player"))
        {
            // Reprodueix el so dels enemics
            Audio.Play();

            // Marca que ja ha reproduït el so, per evitar que torni a riure immediatament
            hasAudio = true;
        }
    }

    // Es crida quan un altre collider surt del trigger
    private void OnTriggerExit(Collider other)
    {
        // Si el collider que surt és el del jugador
        if (other.CompareTag("Player"))
        {
            // Permet que torni a reproduir el so si el jugador entra de nou
            hasAudio = false;
        }
    }
}


