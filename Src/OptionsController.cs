using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider difficultySlider;
    private MusicPlayer musicPlayer;
	
	// Update is called once per frame
	void Start () {
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        musicVolumeSlider.value = PlayerPrefsManager.getMusicVolume();//show current values
        sfxVolumeSlider.value = PlayerPrefsManager.getSfxVolume();
        difficultySlider.value = PlayerPrefsManager.getDifficulty();
    }

    public void Default()
    {
        musicVolumeSlider.value = (musicVolumeSlider.maxValue+musicVolumeSlider.minValue)/2;
        sfxVolumeSlider.value = (sfxVolumeSlider.maxValue + sfxVolumeSlider.minValue) / 2;
        difficultySlider.value = (difficultySlider.maxValue + difficultySlider.minValue) / 2;
    }

    public void SaveAndExit()
    {
        PlayerPrefsManager.setMusicVolume(musicVolumeSlider.value); //save values and leave
        PlayerPrefsManager.setSfxVolume(sfxVolumeSlider.value);
        PlayerPrefsManager.setDifficulty((int)difficultySlider.value);
        LevelManager.loadLevelStatic("Menu");
    }

    void Update()
    {
        musicPlayer.changeMusicVolume(musicVolumeSlider.value);//change in music player
        musicPlayer.changeSfxVolume(sfxVolumeSlider.value);
    }
}
