using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PanelSwitcher", menuName = "Panel Management/Panel Switcher")]
public class PanelSwitcherData : ScriptableObject
{
    public UnityEvent onButtonClicked;

    [System.Serializable]
    public struct PanelPair
    {
        public GameObject[] panelsToDisable; // Array to store multiple panels
        public GameObject panelToDisplay;
    }

    public PanelPair[] panelPairs;

    public void SwitchPanels()
    {
        onButtonClicked.Invoke();

        foreach (var pair in panelPairs)
        {
            foreach (var panel in pair.panelsToDisable) // Iterate through the array
            {
                if (panel != null)
                    panel.SetActive(false);
            }

            if (pair.panelToDisplay != null)
                pair.panelToDisplay.SetActive(true);
        }
    }
}

