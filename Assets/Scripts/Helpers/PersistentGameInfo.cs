using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistentGameInfo //Variables and constants that are persistent between scenes
{
    public static string Scene2Load = "CutsceneIntro";
    public static float VolumeSFX = 1f;
    public static float VolumeMus = 1f;
    public static bool skipTutorial = false;
    public static float _totalTime, _progressTime;
    public static int _enemyCount, _enemyKill, _collectableFound, _deathCount;
    public static bool _noHit;
}
