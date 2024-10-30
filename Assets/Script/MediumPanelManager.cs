using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;
    [SerializeField] private GameObject mainPanel;

    private PlayerData playerData;
    private Transform playerTransform;
    private List<ItemDataSerializable> inventoryItems;
    private List<AchievementManager.Achievement> achievements;
    void Start()
    {
        PanelManager.Instance.panels = panels;
        PanelManager.Instance.mainPanel = mainPanel;
        PanelManager.Instance.playerData = playerData;
        PanelManager.Instance.playerTransform = playerTransform;
    }

}