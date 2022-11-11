using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Malee;
using Malee.List;

[CreateAssetMenu()]
public class AchievementDataBase : ScriptableObject
{
    [Reorderable(sortable = false, paginate = false)]
    public AchievementsArray achievements;

    [System.Serializable]
    public class AchievementsArray : ReorderableArray<Achievement> { }
}
