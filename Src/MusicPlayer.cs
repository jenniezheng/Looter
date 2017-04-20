using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour {
    // Use this for initialization
    public AudioClip MenuElement;
    public AudioClip Menu;
    public AudioClip Pass;
    public AudioClip Game;
    public AudioClip Lose;
    public AudioClip Win;
    AudioSource musicSource;
    AudioSource sfxSource;

    void Awake(){
        GameObject.DontDestroyOnLoad(gameObject);
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = PlayerPrefsManager.getMusicVolume();
        sfxSource = GetComponent<Transform>().GetChild(0).GetComponent<AudioSource>(); //two audio sources;
        sfxSource.volume = PlayerPrefsManager.getSfxVolume();
        SceneManager.sceneLoaded+=onSceneLoaded;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        //change sfx
        sfxSource.Stop();
        if (scene.name == "Lose") sfxSource.clip = Lose; 
        else if (scene.name == "Win") sfxSource.clip = Win;
        else if (scene.name == "Pass") sfxSource.clip = Pass;
        else if ((scene.name.Length >= 11 && scene.name.Substring(0, 11) == "Instruction") || scene.name == "Options") sfxSource.clip = MenuElement;
        else sfxSource.clip = null;
        if (sfxSource.clip != null) sfxSource.Play();

        //change music
        if ((scene.name.Length >= 11 && scene.name.Substring(0, 11) == "Instruction") || scene.name == "Options" || scene.name == "Menu" && musicSource.clip == Menu) return;
        else if (scene.name.Length >= 6 && scene.name.Substring(0, 5) == "Level" && musicSource.clip == Game) return; //don't stop the music when it's correct
        else
        {
            musicSource.Stop();
            if (scene.name == "Menu")  musicSource.clip = Menu; 
            // else if (scene.name == "Pass") audioSource.clip = Pass;
            else if (scene.name.Length >= 6 && scene.name.Substring(0, 5) == "Level") musicSource.clip = Game; 
            else musicSource.clip = null; //signify no music
            if(musicSource.clip!=null) musicSource.Play();
        };
    }

    public void changeMusicVolume(float vol)
    {
        musicSource.volume = vol;
    }


    public void changeSfxVolume(float vol)
    {
        sfxSource.volume = vol;
    }

};
