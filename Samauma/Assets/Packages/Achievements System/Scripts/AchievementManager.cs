using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementManager : MonoBehaviour
{
    public AchievementDataBase dataBase;
    public AchievementNotificationController achievementNotificationController;

    public GameObject achievementItemPrefab;
    public Transform content;

    [SerializeField][HideInInspector]
    List<AchievementItemController> achievementItems;

    [SerializeField][HideInInspector]
    public Achievements achievementsToShow;

    void Start()
    {
        LoadAchievementsTable();
    }

    public void ShowNotification()
    {
        Achievement achievement = dataBase.achievements[(int)achievementsToShow];
        achievementNotificationController.ShowNotification(achievement);
    }

    [ContextMenu("LoadAchievementsTable()")]
    void LoadAchievementsTable()
    {
        foreach (AchievementItemController controller in achievementItems)
        {
            DestroyImmediate(controller.gameObject);
        }

        achievementItems.Clear();

        foreach (Achievement achievement in dataBase.achievements)
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
        UnlockAchievement(achievementsToShow);
    }

    public void UnlockAchievement(Achievements achievement)
    {
        AchievementItemController item = achievementItems[(int)achievement];
        if (item.unlocked)
        {
            return;
        }

        ShowNotification();
        PlayerPrefs.SetInt(item.achievement.id, 1);
        item.unlocked = true;
        item.RefreshView();
    }

    public void LockAllAchievements()
    {
        foreach (Achievement achievement in dataBase.achievements)
        {
            PlayerPrefs.DeleteKey(achievement.id);
        }

        foreach (AchievementItemController controller in achievementItems)
        {
            controller.unlocked = false;
            controller.RefreshView();
        }
    }
}