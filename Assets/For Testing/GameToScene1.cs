using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GametoGameToScene1 : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("Scene1",LoadSceneMode.Single);
    }
}
