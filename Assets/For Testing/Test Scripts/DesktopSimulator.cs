using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesktopSimulator : MonoBehaviour
{
    // References to the panels
    public GameObject mainPanel;   // Main desktop panel
    public GameObject emailPanel;  // Email panel
    public GameObject browserPanel;  // Browser panel

    // Method to open the Email panel and hide the Main panel
    public void OpenEmailPanel()
    {
        mainPanel.SetActive(false);   // Hide the main desktop panel
        emailPanel.SetActive(true);   // Show the email panel
    }

    // Method to open the Browser panel and hide the Main panel
    public void OpenBrowserPanel()
    {
        mainPanel.SetActive(false);   // Hide the main desktop panel
        browserPanel.SetActive(true);   // Show the browser panel
    }

    // Method to return to the Main (desktop) panel
    public void ReturnToDesktop()
    {
        emailPanel.SetActive(false);  // Hide the email panel
        browserPanel.SetActive(false); // Hide the browser panel
        mainPanel.SetActive(true);    // Show the main desktop panel
    }
}
