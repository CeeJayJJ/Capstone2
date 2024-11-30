using System.Collections;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string lastExitName;

    void Start()
    {
        StartCoroutine(SetPlayerPosition());
    }

    IEnumerator SetPlayerPosition()
    {
        yield return new WaitForEndOfFrame(); // Wait for initialization
        if (PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            if (PlayerMovement.instance != null)
            {
                PlayerMovement.instance.transform.position = transform.position;
                PlayerMovement.instance.transform.eulerAngles = transform.eulerAngles;
            }
            else
            {
                Debug.LogError("PlayerMovement.instance is null!");
            }
        }
    }
}

