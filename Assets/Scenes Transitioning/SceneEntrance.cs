using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string lastExitName;

    void Start()
    {
        if(PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
           PlayerMovement.instance.transform.position = transform.position;
           PlayerMovement.instance.transform.eulerAngles = transform.eulerAngles;
        }
    }
}
