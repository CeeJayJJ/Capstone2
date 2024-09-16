using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance { get; private set; } // Singleton

    private void Awake()
    {
        // Singleton implementation (similar to other core scripts)
    }

    public void LoadScene(string sceneName)
    {
        // Optionally, trigger a loading screen or transition effect
        // ...

        SceneManager.LoadScene(sceneName);
    }
}