using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class code_TabSystem : MonoBehaviour
{
    [SerializeField] GameObject[] tabs;

    public void onTabSwitch(GameObject tab)
    {
        tab.SetActive(true);

        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i] != tab)
            {
                tabs[i].SetActive(false);
            }
        }
    }
}
