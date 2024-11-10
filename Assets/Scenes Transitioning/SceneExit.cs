using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneExit : MonoBehaviour
{
    public string sceneToLoad;
    public string exitName;

    void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetString("LastExitName", exitName);
        SceneManager.LoadScene(sceneToLoad);
        QuestsManager.questsManager.AddQuestItem("Leave the house", 1);
    }

    
}
