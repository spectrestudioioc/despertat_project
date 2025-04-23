using UnityEngine;

public class ToggleDiari : MonoBehaviour
{
    public GameObject paginaDiari; // Inscriure pàgina de diari


    // Mètode que activa i desactiva la pàgina en el event Onclick
    public void TogglePagina()
    {
        if (paginaDiari != null)
        {
            bool estatActiu = paginaDiari.activeSelf; // Sentència que activa la pàgina si és true
            paginaDiari.SetActive(!estatActiu); // Sentència que desactiva la pàgina si no és true (false)
        }
    }
}