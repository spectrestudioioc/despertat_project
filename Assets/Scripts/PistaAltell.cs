using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistaAltell : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.MostraPistaAltell();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AmagaPistaAltell();
            gameObject.SetActive(false); // Desactiva el GameObject amb el collider perquè no es pugui activar més
        }
    }
}
