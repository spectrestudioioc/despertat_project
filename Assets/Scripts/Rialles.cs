using UnityEngine;

// Classe que gestiona el so de rialla de l'enemic quan el jugador entra en una �rea de detecci�
public class EnemyRialles : MonoBehaviour
{
    // Refer�ncia a l'objecte AudioSource que cont� el so de la rialla
    public AudioSource laughAudio;

    // Variable per controlar si ja ha rigut o no
    private bool hasLaughed = false;

    // Es crida quan un altre collider entra dins del trigger
    private void OnTriggerEnter(Collider other)
    {
        // Si encara no ha rigut i el collider pertany al jugador
        if (!hasLaughed && other.CompareTag("Player"))
        {
            // Reprodueix el so de la rialla
            laughAudio.Play();

            // Marca que ja ha rigut, per evitar que torni a riure immediatament
            hasLaughed = true;
        }
    }

    // Es crida quan un altre collider surt del trigger
    private void OnTriggerExit(Collider other)
    {
        // Si el collider que surt �s el del jugador
        if (other.CompareTag("Player"))
        {
            // Permet que torni a riure si el jugador entra de nou
            hasLaughed = false;
        }
    }
}


