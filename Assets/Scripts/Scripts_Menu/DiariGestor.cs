using UnityEngine;

public class ToggleDiari : MonoBehaviour
{
    public GameObject paginaDiari; // Inscriure p�gina de diari


    // M�tode que activa i desactiva la p�gina en el event Onclick
    public void TogglePagina()
    {
        if (paginaDiari != null)
        {
            bool estatActiu = paginaDiari.activeSelf; // Sent�ncia que activa la p�gina si �s true
            paginaDiari.SetActive(!estatActiu); // Sent�ncia que desactiva la p�gina si no �s true (false)
        }
    }
}