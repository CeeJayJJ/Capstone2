using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameIntroduction : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        AchievementsManager.instance.UnlockAchievement(Achievements.FirstPlay);
        SceneManager.LoadScene("Introduction",LoadSceneMode.Single);
        PlayerPrefs.SetString("LastExitName", "Room");
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("Ambience_House");
        
    }
}
