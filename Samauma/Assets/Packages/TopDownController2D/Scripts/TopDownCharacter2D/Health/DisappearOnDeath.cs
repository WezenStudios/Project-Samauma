using TopDownCharacter2D.FX;
using UnityEngine;

namespace TopDownCharacter2D.Health
{
    /// <summary>
    ///     Handles the removal of an entity when it dies
    /// </summary>
    public class DisappearOnDeath : MonoBehaviour
    {
        code_Gameplay gameplay;
        AchievementManager achievementManager;

        private void Start()
        {
            gameplay = GameObject.Find("Gameplay Manager").GetComponent<code_Gameplay>();
            achievementManager = GameObject.Find("Achievement Manager").GetComponent<AchievementManager>();
        }

        public void OnDeath()
        {
            foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
            {
                if (!(component is AudioSource) && !(component is TopDownFx))
                {
                    component.enabled = false;
                }
            }

            foreach (Renderer component in transform.GetComponentsInChildren<Renderer>())
            {
                if (!(component is ParticleSystemRenderer))
                {
                    component.enabled = false;
                }
            }

            gameplay.kills++;
            Destroy(gameObject, 20f);
        }

        public void OnPlayerDeath()
        {
            foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
            {
                if (!(component is AudioSource) && !(component is TopDownFx))
                {
                    component.enabled = false;
                }
            }

            foreach (Renderer component in transform.GetComponentsInChildren<Renderer>())
            {
                if (!(component is ParticleSystemRenderer))
                {
                    component.enabled = false;
                }
            }

            Destroy(gameObject, 20f);

            achievementManager.achievementsToShow = Achievements.A0;
            achievementManager.UnlockAchievement(Achievements.A0);

            gameplay.hudDefeat.SetActive(true);

            if (gameplay.hudDefeat.activeInHierarchy)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}