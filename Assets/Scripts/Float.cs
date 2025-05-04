// Floater v0.0.2
// by Donovan Keith
//
// [MIT License](https://opensource.org/licenses/MIT)

using UnityEngine;
using System.Collections;

// Aquesta classe fa que els objectes flotin amunt i avall mentre giren suaument.
public class Floater : MonoBehaviour
{
    // Par�metres introdu�ts per l'usuari
    public float degreesPerSecond = 15.0f; // Graus de rotaci� per segon
    public float amplitude = 0.5f; // Al�ada m�xima del moviment de flotaci�
    public float frequency = 1f; // Freq��ncia del moviment de flotaci�

    // Variables per emmagatzemar la posici�
    Vector3 posOffset = new Vector3(); // Posici� inicial de l'objecte
    Vector3 tempPos = new Vector3(); // Posici� temporal modificada durant la flotaci�

    
    void Start()
    {
        // Emmagatzemem la posici� i rotaci� inicials de l'objecte
        posOffset = transform.position;
    }

    
    void Update()
    {
        // Fa girar l'objecte al voltant de l'eix Y
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Fa que l'objecte floti amunt i avall utilitzant una funci� Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
