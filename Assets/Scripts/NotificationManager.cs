using System;
using TMPro;
using UnityEngine;


// Aquesta classe gestiona diferents notificacions. Requereix que existeixi un gameObject del tipus GameManager

[RequireComponent(typeof(GameManager))]  
public class NotificationManager : MonoBehaviour
{
    [SerializeField] private TMP_Text notificationText; // Text UI per mostrar la notificació
    [SerializeField] private Animation notificationAnimation; // Animació de la notificació

    private static NotificationManager _instance; // Instància singleton

    // Retorna la instància única del NotificationManager
     
    public static NotificationManager Instance
    {
        get { return _instance; }
    }

    
     // Inicialitza la instància singleton del gestor de notificacions
     
    private void Awake()
    {
        _instance = this;
    }

    // Mètode per mostrar per pantalla un missatge, activant l'animació que té afegida com a component
    public void ShowNotification(String notification)
    {
        notificationText.text = notification;
        notificationAnimation.Play();
    }
}

