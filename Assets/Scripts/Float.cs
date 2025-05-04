// Floater v0.0.2
// by Donovan Keith
//
// [MIT License](https://opensource.org/licenses/MIT)

using UnityEngine;
using System.Collections;

// Aquesta classe fa que els objectes flotin amunt i avall mentre giren suaument.
public class Floater : MonoBehaviour
{
    // Paràmetres introduïts per l'usuari
    public float degreesPerSecond = 15.0f; // Graus de rotació per segon
    public float amplitude = 0.5f; // Alçada màxima del moviment de flotació
    public float frequency = 1f; // Freqüència del moviment de flotació

    // Variables per emmagatzemar la posició
    Vector3 posOffset = new Vector3(); // Posició inicial de l'objecte
    Vector3 tempPos = new Vector3(); // Posició temporal modificada durant la flotació

    
    void Start()
    {
        // Emmagatzemem la posició i rotació inicials de l'objecte
        posOffset = transform.position;
    }

    
    void Update()
    {
        // Fa girar l'objecte al voltant de l'eix Y
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Fa que l'objecte floti amunt i avall utilitzant una funció Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}
