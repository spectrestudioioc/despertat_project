using UnityEngine;

public class DiariGestor : MonoBehaviour
{
    public int pickupID;  // Identificador del pickup associat a aquesta imatge
    public GameObject paginaDiari;  // Pàgina del diari corresponent a aquesta imatge
    

    // Mètode que activa i desactiva la pàgina quan es fa clic
    public void TogglePagina()
    {
        // Comprova si el pickup associat ha estat recollit
        if (GameManager.Instance.HaRecollitPickup(pickupID))
        {
            bool estatActiu = paginaDiari.activeSelf;
            paginaDiari.SetActive(!estatActiu);  // Activa o desactiva la pàgina
        }
        else
        {
            Debug.Log("Aquesta pàgina encara no està disponible, recull-la primer.");
        }
    }
}