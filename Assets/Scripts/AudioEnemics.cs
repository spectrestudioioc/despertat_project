using UnityEngine;

// Classe que gestiona el so dels enemics quan el jugador entra en una �rea de detecci�
public class EnemySound : MonoBehaviour
{
    // Refer�ncia a l'objecte AudioSource que cont� el so dels enemics
    public AudioSource Audio;

    // Variable per controlar si ja ha reprodu�t el so
    private bool hasAudio = false;

    // Es crida quan un altre collider entra dins del trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si encara no reprodu�t el so i el collider pertany al jugador
        if (!hasAudio && other.CompareTag("Player"))
        {
            // Reprodueix el so dels enemics
            Audio.Play();

            // Marca que ja ha reprodu�t el so, per evitar que torni a riure immediatament
            hasAudio = true;
        }
    }

    // Es crida quan un altre collider surt del trigger
    private void OnTriggerExit(Collider other)
    {
        // Si el collider que surt �s el del jugador
        if (other.CompareTag("Player"))
        {
            // Permet que torni a reproduir el so si el jugador entra de nou
            hasAudio = false;
        }
    }
}


