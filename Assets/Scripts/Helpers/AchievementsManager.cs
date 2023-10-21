using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour, AchievementPersistance
{
    public static AchievementsManager INSTANCE;

    public bool ACHIEVEMENT01;
    public int ENEMIES_LVL01 = 10;
    public int ENEMIES_KILLED;

    public bool ACHIEVEMENT02;
    public int ITEMS_TOTAL = 1;
    public int ITEMS_COLLECTED;

    public bool ACHIEVEMENT03;
    public int RADAR_COUNT;

    public bool ACHIEVEMENT04;
    public int BOMB_COUNT;

    public bool ACHIEVEMENT05;

    public bool ACHIEVEMENT06;

    private void Awake()
    {
        INSTANCE = this;
    }

    public void AchievementEarned()
    {

    }

    public void Achievement01()
    {
        if (ACHIEVEMENT01)
        {
            return;
        }

        ENEMIES_KILLED++;
        if (ENEMIES_KILLED == ENEMIES_LVL01)
        {
            ACHIEVEMENT01 = true;
            AchievementEarned();
        }
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement02()
    {
        if (ACHIEVEMENT02)
        {
            return;
        }

        ITEMS_COLLECTED++;
        if (ITEMS_COLLECTED == ITEMS_TOTAL)
        {
            ACHIEVEMENT02 = true;
            AchievementEarned();
        }
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement03()
    {
        if (ACHIEVEMENT03)
        {
            return;
        }

        RADAR_COUNT++;
        if (RADAR_COUNT == 5)
        {
            ACHIEVEMENT03 = true;
            AchievementEarned();
        }
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement04()
    {
        if (ACHIEVEMENT04)
        {
            return;
        }

        BOMB_COUNT++;
        if (BOMB_COUNT == 5)
        {
            ACHIEVEMENT04 = true;
            AchievementEarned();
        }
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement05()
    {
        if (ACHIEVEMENT05)
        {
            return;
        }

        ACHIEVEMENT05 = true;
        AchievementEarned();
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement06()
    {
        if (ACHIEVEMENT06)
        {
            return;
        }

        ACHIEVEMENT06 = true;
        AchievementEarned();
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    void AchievementPersistance.LoadAchievements(AchievementsData data)
    {
        ACHIEVEMENT01 = data.ACHIEVEMENT01;
        ACHIEVEMENT02 = data.ACHIEVEMENT02;
        ACHIEVEMENT03 = data.ACHIEVEMENT03;
        ACHIEVEMENT04 = data.ACHIEVEMENT04;
        ACHIEVEMENT05 = data.ACHIEVEMENT05;
        ACHIEVEMENT06 = data.ACHIEVEMENT06;

        ENEMIES_KILLED = data.ENEMIES_KILLED;
        ITEMS_COLLECTED = data.ITEMS_COLLECTED;
        RADAR_COUNT = data.RADAR_COUNT;
        BOMB_COUNT = data.BOMB_COUNT;
    }

    void AchievementPersistance.SaveAchievements(AchievementsData data)
    {
        data.ACHIEVEMENT01 = ACHIEVEMENT01;
        data.ACHIEVEMENT02 = ACHIEVEMENT02;
        data.ACHIEVEMENT03 = ACHIEVEMENT03;
        data.ACHIEVEMENT04 = ACHIEVEMENT04;
        data.ACHIEVEMENT05 = ACHIEVEMENT05;
        data.ACHIEVEMENT06 = ACHIEVEMENT06;

        data.ENEMIES_KILLED = ENEMIES_KILLED;
        data.ITEMS_COLLECTED = ITEMS_COLLECTED;
        data.RADAR_COUNT = RADAR_COUNT;
        data.BOMB_COUNT = BOMB_COUNT;
    }
}
