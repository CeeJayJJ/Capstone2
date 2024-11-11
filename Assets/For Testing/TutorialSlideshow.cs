using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSlideshow : MonoBehaviour
{
    public GameObject[] panels;     // Array to store each tutorial panel
    public Button nextButton;       // Button to go to the next panel
    public Button prevButton;       // Button to go to the previous panel
    public Button doneButton;       // Button to finish the tutorial

    private int currentPanelIndex = 0;

    private void Start()
    {
        UpdatePanelVisibility();

        // Button listeners
        nextButton.onClick.AddListener(NextPanel);
        prevButton.onClick.AddListener(PreviousPanel);
        doneButton.onClick.AddListener(FinishTutorial);

        // Ensure only Next and Done buttons are active at start
        prevButton.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(false);
    }

    private void UpdatePanelVisibility()
    {
        // Loop through panels to show only the current one
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentPanelIndex);
        }

        // Update button visibility based on the current panel index
        prevButton.gameObject.SetActive(currentPanelIndex > 0);
        nextButton.gameObject.SetActive(currentPanelIndex < panels.Length - 1);
        doneButton.gameObject.SetActive(currentPanelIndex == panels.Length - 1);
    }

    public void NextPanel()
    {
        if (currentPanelIndex < panels.Length - 1)
        {
            currentPanelIndex++;
            UpdatePanelVisibility();
        }
    }

    public void PreviousPanel()
    {
        if (currentPanelIndex > 0)
        {
            currentPanelIndex--;
            UpdatePanelVisibility();
        }
    }

    private void FinishTutorial()
    {
        // Optionally close the tutorial, load a new scene, or trigger a tutorial completed event
        gameObject.SetActive(false);
        Debug.Log("Tutorial completed!");
    }
}
