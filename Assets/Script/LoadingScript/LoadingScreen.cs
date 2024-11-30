using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LoadingScreen1 : MonoBehaviour
{
    public Slider progressBar; // Reference to your UI Slider
    public TextMeshProUGUI progressText;  // Optional: Text to show percentage


    void Start()
    {
        LoadScene("City"); // Automatically load the target scene when the loading screen starts
    }

    // Method to start loading a scene
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Start loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // Prevent scene activation until the loading is complete
        operation.allowSceneActivation = false;

        // While the scene is loading
        while (!operation.isDone)
        {
            // Calculate the progress (normalized from 0 to 1)
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // 90% is the maximum progress value

            // Update UI
            if (progressBar != null)
                progressBar.value = progress;
            if (progressText != null)
                progressText.text = Mathf.RoundToInt(progress * 100) + "%";

            // Activate the scene when fully loaded
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null; // Wait until the next frame
        }
    }
}
