using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        SceneManager.LoadScene("Introduction", LoadSceneMode.Single);
    }


    //public Image LoadingBarFill;

    //public void LoadScene(int SceneID)
    //{
    //   StartCoroutine(LoadSceneAsync(SceneID));
    //   AudioManager.Instance.musicSource.Stop();

    //    if(SceneID == 1)
    //    {
    //      AudioManager.Instance.PlayMusic("Theme2");
    //    }else
    //    {

    //    }

    //}

    //IEnumerator LoadSceneAsync(int SceneID)
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);

    //    LoadingScreens.SetActive(true);

    //    while(!operation.isDone)
    //    {
    //        float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
    //        LoadingBarFill.fillAmount = progressValue;

    //        yield return null;

    //    }
    //}

}
