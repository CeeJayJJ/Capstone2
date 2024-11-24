using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Malee.List;

[CreateAssetMenu()]
public class AchievementDatabase : ScriptableObject
{
    [Reorderable(sortable = false, paginate = false)]
    public AchievementArray achievements;

    [System.Serializable]
    public class AchievementArray: ReorderableArray<Achievement>
    {

    }
}
