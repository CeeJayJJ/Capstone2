using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{
    public List <Button> btns = new List<Button>();
    void Start()
    {
        GetButtons();
    }

    // Update is called once per frame
    void GetButtons()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");

        for(int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
        }
    }
}
