using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementItemController : MonoBehaviour
{
    [SerializeField] TMP_Text titleLabel, descriptionLabel;
    [SerializeField] Image lockedIcon, unlockedIcon;

    public bool unlocked;
    public Achievement achievement;

    public void RefreshView()
    {
        titleLabel.text = achievement.title;
        descriptionLabel.text = achievement.description;

        unlockedIcon.enabled = unlocked;
        lockedIcon.enabled = !unlocked;
    }

    void OnValidate()
    {
        RefreshView();
    }
}
