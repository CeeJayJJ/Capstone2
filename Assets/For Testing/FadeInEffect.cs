using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Image fadeImage;            // Reference to the Image component
    public float fadeInDuration = 1f;  // Duration of the fade-in effect
    public float fadeOutDuration = 1f; // Duration of the fade-out effect

    private Coroutine fadeCoroutine;   // Store reference to the current coroutine

    private void Start()
    {
        if (fadeImage != null)
        {
            // Start fully transparent for testing both fade-in and fade-out
            SetImageAlpha(0f);
        }
    }

    // Method to fade in
    public void StartFadeIn()
    {
        // Stop any existing fade coroutine to prevent overlap
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeIn());
    }

    // Method to fade out
    public void StartFadeOut()
    {
        // Stop any existing fade coroutine to prevent overlap
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeOut());
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeInDuration); // Fade from opaque to transparent
            SetImageAlpha(alpha);
            yield return null;
        }
        SetImageAlpha(0f); // Ensure it's fully transparent at the end
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeOutDuration); // Fade from transparent to opaque
            SetImageAlpha(alpha);
            yield return null;
        }
        SetImageAlpha(1f); // Ensure it's fully opaque at the end
    }

    // Helper method to set the image alpha
    private void SetImageAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }

}
