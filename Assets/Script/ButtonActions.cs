using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Needed for Button reference

[System.Serializable]
public class ButtonActions : MonoBehaviour
{
    public string sceneNameToLoad;

    public Button[] interactiveButtons; // Array to hold button references
    public string hoverSoundName = "ButtonHover";
    public string clickSoundName = "ButtonClick";

    // ... (QuitGame and LoadScene methods remain the same)


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            SceneManager.LoadScene(sceneNameToLoad);
            AudioManager.Instance.PlayMusic("Theme2");
        }
        else
        {
            Debug.LogWarning("No scene specified for LoadSceneButton!");
        }
    }



    // ... (QuitGame and LoadScene methods remain the same)

    public void PlayHoverSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(hoverSoundName);
        }
    }

    public void PlayClickSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX(clickSoundName);
        }
    }


}
