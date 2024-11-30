using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class AdManager : MonoBehaviour
{
    public GameObject desktopPanel; // Reference to the DesktopPanel
    private GameObject[] adPanels; // Array to hold references to all ad panels
    private int adsClosed = 0;

    private void Start()
    {
        // Find all panels with the tag "Advertisement" or by their child Button component
        adPanels = GetAdPanels();

        foreach (GameObject panel in adPanels)
        {
            // Find the "Exit" button in each panel and add a listener
            Button exitButton = panel.GetComponentInChildren<Button>();
            if (exitButton != null)
            {
                exitButton.onClick.AddListener(() => CloseAd(panel));
            }
        }
    }

    private GameObject[] GetAdPanels()
    {
        // Assuming all ad panels are tagged or have a common identifiable component under DesktopPanel
        return desktopPanel.GetComponentsInChildren<Transform>(true) // True includes inactive objects
            .Where(t => t.CompareTag("Advertisement")) // Ensure you’ve tagged each ad panel
            .Select(t => t.gameObject)
            .ToArray();
    }

    private void CloseAd(GameObject adPanel)
    {
        // Deactivate the panel to simulate closing the ad
        adPanel.SetActive(false);
        adsClosed++;
        QuestsManager.questsManager.AddQuestItem("Remove Unwanted Pop-Up Ads from Lola's laptop.", 1);
        // Check if all ads are closed
        if (adsClosed >= adPanels.Length)
        {
            Debug.Log("All ads are removed! You win!");
            // Trigger win condition here (e.g., display a "You Win!" message)
            AchievementsManager.instance.UnlockAchievement(Achievements.FirstTechUse);
        }
    }
}
