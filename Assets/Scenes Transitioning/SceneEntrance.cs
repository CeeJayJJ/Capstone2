using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    // Start is called before the first frame update

    public string lastExitName;

    void Start()
    {
        if(PlayerPrefs.GetString("LastExitName") == lastExitName)
        {
            PlayerMovement.instance.transform.position = transform.position;
            PlayerMovement.instance.transform.eulerAngles = transform.eulerAngles;
            QuestsManager.questsManager.AddQuestItem("Leave the house", 1);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
