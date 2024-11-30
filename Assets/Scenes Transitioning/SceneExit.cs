using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public string sceneToLoad;
    public string exitName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("LastExitName", exitName);
            PlayerPrefs.Save(); // Explicitly save PlayerPrefs
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

