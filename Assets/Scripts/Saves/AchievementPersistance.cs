using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AchievementPersistance
{
    void LoadAchievements(AchievementsData data);
    void SaveAchievements(AchievementsData data);
}
