using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    private PlayableDirector playableDirector;

    private void Awake()
    {
        // Find the PlayableDirector component attached to the Canvas
        playableDirector = FindObjectOfType<PlayableDirector>();

        if (playableDirector == null)
        {
            Debug.LogError("PlayableDirector not found in the scene!");
        }
    }

    public void OnSkipButtonClicked()
    {
        if (playableDirector != null)
        {
            Debug.Log("Playable Director state: " + playableDirector.state);

            if (playableDirector.state == PlayState.Playing)
            {
                Debug.Log("Stopping the Timeline.");
                // Stop the timeline
                playableDirector.Stop();
                playableDirector.time = playableDirector.duration; // Set time to the end

                // Load the next scene
                Debug.Log("Loading scene: Kai'sHouse");
                SceneManager.LoadScene("Kai'sHouse", LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("Timeline is not playing.");
            }
        }
    }
}
