using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance { get; private set; }

    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject mainPanel;

    private void Awake()
    {
        // Singleton pattern enforcement
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize panels (hide all except mainPanel)
        if (panels != null && panels.Length > 0)
        {
            foreach (var panel in panels)
            {
                if (panel != null)
                {
                    panel.SetActive(panel == mainPanel);
                }
            }
        }
        else
        {
            Debug.LogWarning("No panels assigned to PanelManager!");
        }
    }

    public void ShowPanel(GameObject panelToShow)
    {
        if (panels != null && panels.Length > 0)
        {
            foreach (var panel in panels)
            {
                if (panel != null)
                {
                    panel.SetActive(panel == panelToShow);
                }
            }
        }
    }
}

