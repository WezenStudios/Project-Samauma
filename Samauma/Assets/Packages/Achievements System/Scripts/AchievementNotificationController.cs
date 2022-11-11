using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class AchievementNotificationController : MonoBehaviour
{
    [SerializeField] TMP_Text achievementTitle;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowNotification(Achievement achievement)
    {
        achievementTitle.text = achievement.title;
        anim.SetTrigger("Appear");
    }
}