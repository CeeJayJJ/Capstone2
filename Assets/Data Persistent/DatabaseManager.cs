using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
   private PlayerMovement playerMovement;  
   // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement component is missing on the Player object!");
            }
        }
        else
        {
            Debug.LogError("Player GameObject with the 'Player' tag was not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {   
        SaveGame.Load();
        SceneManager.LoadScene(SaveGame.Instance.currentScene);
        playerMovement.transform.position = SaveGame.Instance.PlayerPosition;
        playerMovement.techbar = SaveGame.Instance.techbar;


    }

    public void Save()
    {
        // Update the current scene before saving
        SaveGame.Instance.currentScene = SceneManager.GetActiveScene().name;
        SaveGame.Instance.PlayerPosition = playerMovement.transform.position;
        SaveGame.Instance.techbar = playerMovement.techbar;
        SaveGame.Save();
    }
}
