using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DamageFlash : MonoBehaviour
{
    public Image damageImage;
    public Color flashColor = new Color(1f, 0f, 0f, 0.4f); // Vermell translúcid
    public float flashDuration = 0.5f;

    private Coroutine flashCoroutine;

    public void TriggerFlash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        damageImage.color = flashColor;

        float elapsedTime = 0f;
        Color startColor = flashColor;
        Color endColor = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);

        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            damageImage.color = Color.Lerp(startColor, endColor, elapsedTime / flashDuration);
            yield return null;
        }

        damageImage.color = endColor;
        flashCoroutine = null;
    }
}
