using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconaDiariController : MonoBehaviour
{
    public int pickupID; // ID d’aquesta pàgina
    public GameObject iconaBN;     // Imatge en blanc i negre
    public GameObject iconaColor;  // Imatge en color amb botó

    void Start()
    {
        ActualitzaEstat();
    }

    public void ActualitzaEstat()
    {
        bool recollit = GameManager.Instance.HaRecollitPickup(pickupID);

        iconaBN.SetActive(!recollit);
        iconaColor.SetActive(recollit);
    }
}
