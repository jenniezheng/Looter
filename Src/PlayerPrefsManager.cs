using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    const string MASTER_MUSIC_VOLUME_KEY = "master_music_volume";
    const string MASTER_SFX_VOLUME_KEY = "master_sfx_volume";
    const string MASTER_DIFFICULTY_KEY = "master_difficulty";
    const string LEVEL_KEY = "level_unlocked_";
	
    public static bool hasDifficultyKey()
    {
        return PlayerPrefs.HasKey(MASTER_DIFFICULTY_KEY);
    }

    public static bool hasSfxVolumeKey()
    {
        return PlayerPrefs.HasKey(MASTER_SFX_VOLUME_KEY);
    }
    public static bool hasMusicVolumeKey()
    {
        return PlayerPrefs.HasKey(MASTER_MUSIC_VOLUME_KEY);
    }

    public static void setMusicVolume(float volume)
    {
        if (volume >= 0 && volume <= 1)
            PlayerPrefs.SetFloat(MASTER_MUSIC_VOLUME_KEY, volume);
        else Debug.LogError("Master volume out of range.");
    }

    public static float getMusicVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_MUSIC_VOLUME_KEY);
    }

    public static void setSfxVolume(float volume)
    {
        if (volume >= 0 && volume <= 1)
            PlayerPrefs.SetFloat(MASTER_SFX_VOLUME_KEY, volume);
        else Debug.LogError("Master volume out of range.");
    }

    public static float getSfxVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_SFX_VOLUME_KEY);
    }

    public static void setDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(MASTER_DIFFICULTY_KEY, difficulty);
    }

    public static int getDifficulty()
    {
        return PlayerPrefs.GetInt(MASTER_DIFFICULTY_KEY);
    }

    public static void unlockLevel(int level)
    {
        if (level >= 1)
            PlayerPrefs.SetInt(LEVEL_KEY+level, 1);//1 for true
         else Debug.LogError("Level out of range.");
    }

    public static bool isLevelUnlocked(int level)
    {
        return PlayerPrefs.GetInt(LEVEL_KEY + level)==1;
    }

    public static string prefsToString()
    {
        string s= "MUSIC_VOLUME: ";
        s += getMusicVolume();
        s+= "\nSFX_VOLUME: ";
        s += getSfxVolume();
        s += "\nLevel(s) Unlocked: ";
        for(int i=0; i<3; i++)
            if (isLevelUnlocked(i)) s += i + " ";
        return s;
    }

   
};
