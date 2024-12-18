using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour
{
    public AchievementDatabase database;
    public AchievementNotificationController achievementNotificationController;
    public AchievementDropdownController achievementDropdownController;
    public GameObject achievementItemPrefab;
    public Transform content;
    public Achievements achievementToShow;
    [SerializeField][HideInInspector]
    private List<AchievementItemController> achievementItems;

    private void Start()
    {
        achievementDropdownController.onValueChanged += HandleAchievementDropdownValueChanged;
        LoadAchievementsTable();
    }
    public void ShowNotification()
    {
        Achievement achievement = database.achievements[(int)achievementToShow];
        achievementNotificationController.ShowNotification(achievement);
    }

    private void HandleAchievementDropdownValueChanged(Achievements achievements)
    {
        achievementToShow = achievements;
    }
    [ContextMenu("LoadAchievementsTable()")]
    private void LoadAchievementsTable()
    {
        foreach (AchievementItemController controller in achievementItems)
        {
            DestroyImmediate(controller.gameObject);
        }
        achievementItems.Clear();
        foreach (Achievement achievement in database.achievements)
        {
            GameObject obj = Instantiate(achievementItemPrefab, content);
            AchievementItemController controller = obj.GetComponent<AchievementItemController>();
            bool unlocked = PlayerPrefs.GetInt(achievement.id, 0) == 1;
            controller.unlocked = unlocked;
            controller.achievement = achievement;
            controller.RefreshView();
            achievementItems.Add(controller);
        }
    }

    public void UnlockAchievement()
    {
        UnlockAchievement(achievementToShow);
    }
    public void UnlockAchievement(Achievements achievement)
    {
        AchievementItemController item = achievementItems[(int)achievement];
        if (item.unlocked)
            return;
        ShowNotification();
        PlayerPrefs.SetInt(item.achievement.id, 1);
        item.unlocked = true;
        item.RefreshView();
    }

    public void LockAllAchievements()
    {
        foreach(Achievement achievement in database.achievements) 
        {
            PlayerPrefs.DeleteKey(achievement.id);
        }

        foreach(AchievementItemController controller in achievementItems)
        {
            controller.unlocked = false;
            controller.RefreshView();
        }
    }
}
