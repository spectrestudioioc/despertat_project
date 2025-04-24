using System;
using TMPro;
using UnityEngine;

/**
 * Gestor de notificacions implementat com un singleton simplificat.
 * 
 * Permet afegir notificacions fàcilment des de qualsevol altra classe.
 * 
 * ⚠ ALERTA: Com que només es mostren durant el joc, aquest component ha d'estar lligat a un component UI Display.
 */
[RequireComponent(typeof(GameManager))]
public class NotificationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text notificationText; // Text UI per mostrar la notificació
    [SerializeField] private Animation notificationAnimation; // Animació de la notificació

    private static NotificationManager _instance; // Instància singleton

    /**
     * Retorna la instància única del NotificationManager.
     */
    public static NotificationManager Instance
    {
        get { return _instance; }
    }

    /**
     * Inicialitza la instància singleton del gestor de notificacions.
     */
    private void Awake()
    {
        _instance = this;
    }

    /**
     * Mostra una notificació en pantalla amb una animació.
     * 
     * paràmetre: notification- Missatge de la notificació a mostrar.
     */
    public void ShowNotification(String notification)
    {
        notificationText.text = notification;
        notificationAnimation.Play();
    }
}

