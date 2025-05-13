using UnityEngine;

public class DiariGestor : MonoBehaviour
{
    public int pickupID;  // Identificador del pickup associat a aquesta imatge
    public GameObject paginaDiari;  // P�gina del diari corresponent a aquesta imatge
    

    // M�tode que activa i desactiva la p�gina quan es fa clic
    public void TogglePagina()
    {
        // Comprova si el pickup associat ha estat recollit
        if (GameManager.Instance.HaRecollitPickup(pickupID))
        {
            bool estatActiu = paginaDiari.activeSelf;
            paginaDiari.SetActive(!estatActiu);  // Activa o desactiva la p�gina
        }
        else
        {
            Debug.Log("Aquesta p�gina encara no est� disponible, recull-la primer.");
        }
    }
}