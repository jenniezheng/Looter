using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamingManager : MonoBehaviour {
    public static int maxLevel=9;
    public static int startLives = 3;
    
    static Text tScore;
    static Text tLives;
    static int score;
    static int level;
    static float lives;
    new AudioSource audio;

    //for testing only
    bool testing = true;
    public GameObject[] icons;


    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        level = 1;
        lives = startLives;
        audio = GetComponent<AudioSource>();
        checkPlayerPrefKeys();
        SceneManager.sceneLoaded += onSceneLoaded;
        StartCoroutine(ticker());
    }

    void Update()
    {
        if (testing) test();
    }

    void checkPlayerPrefKeys()
    {
        if (!PlayerPrefsManager.hasDifficultyKey()) PlayerPrefsManager.setDifficulty(4);
        if (!PlayerPrefsManager.hasSfxVolumeKey()) PlayerPrefsManager.setSfxVolume(.5f);
        if (!PlayerPrefsManager.hasMusicVolumeKey()) PlayerPrefsManager.setMusicVolume(.15f);
    }

    void test()
    {
        if(SceneManager.GetActiveScene().name.Length>=5 && SceneManager.GetActiveScene().name.Substring(0, 6) == "Level_")//in levels
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            if (Input.GetKeyDown(KeyCode.Alpha1)) Instantiate(icons[0], mousePos, icons[0].transform.rotation);
            else if (Input.GetKeyDown(KeyCode.Alpha2)) Instantiate(icons[1], mousePos, icons[1].transform.rotation);
            else if (Input.GetKeyDown(KeyCode.Alpha3)) Instantiate(icons[2], mousePos, icons[2].transform.rotation);
            else if (Input.GetKeyDown(KeyCode.Alpha4)) Instantiate(icons[3], mousePos, icons[3].transform.rotation);
            else if (Input.GetKeyDown(KeyCode.Alpha5)) Instantiate(icons[4], mousePos, icons[4].transform.rotation);
            else if (Input.GetKeyDown(KeyCode.Alpha6)) Instantiate(icons[5], mousePos, icons[5].transform.rotation);
        }
    }

    void onSceneLoaded(Scene newScene, LoadSceneMode mode)
    {
        if (newScene.name == "Menu"||newScene.name=="Splash"|| newScene.name == "LevelSelect" || newScene.name == "Options"||
            newScene.name.Length >= 8 && newScene.name.Substring(0, 8) == "Instruct") return;
        else if (newScene.name == "Pass"|| newScene.name == "Win"|| newScene.name == "Lose")
        {
            if (GameObject.FindWithTag("Level") != null) GameObject.FindWithTag("Level").GetComponent<Text>().text = "level " + level;
            GameObject.FindWithTag("Score").GetComponent<Text>().text = "Total Score: " + score;
        }
        else if(newScene.name.Length>=5 && newScene.name.Substring(0,5)=="Level")//level?
        {
            tLives=GameObject.FindWithTag("Lives").GetComponent<Text>();
            tLives.text = lives+ "";
            tScore = GameObject.FindWithTag("Score").GetComponent<Text>();
            tScore.text = ""+score;
            GameObject.FindWithTag("Level").GetComponent<Text>().text="Level: "+level;
        }
    }

    public static void restartAll()
    {
        score = 0;
        level = 1;
        lives = startLives;
        LoadLevel("Level_0" + level);
    }

    public static void restartLevel()
    {
        lives = startLives;
        LoadLevel("Level_0" + level);
        score = 0;
    }

    public static void nextLevel()
    {
        level++;
        if (level > maxLevel) LoadLevel("Win");
        else
        {
            Debug.Log("Loading next level");
            PlayerPrefsManager.unlockLevel(level);//unlock the new level 
            LoadLevel("Level_0" + level);
        }
    }

    public static void selectLevel(int lvl)
    {
        lives = startLives;
        score = 0;
        level = lvl;
        LoadLevel("Level_0" + level);
    }

    public static void upScore(int points)
    {
        score += points;
        if(tScore!=null)
            tScore.text = "" + score;
    }

    public void changeLife(float num)
    {
        lives+=num;
        if(tLives!=null) tLives.text = "" + lives;
        if (lives < 0) LoadLevel("Lose");//0 is ok
    }

    public void ballDied()
    {
        StartCoroutine(ballDying());
    }

    public void ballShot()
    {
        //don't play typical lose sound
        changeLife(-1);
        Ball ball = FindObjectOfType<Ball>();
        ball.started = false;
    }

    IEnumerator ballDying()
    {
        yield return new WaitForSeconds(.5f);
        audio.volume = PlayerPrefsManager.getSfxVolume();
        audio.Play();
        yield return new WaitForSeconds(1f);
        
        changeLife(-1);
        Ball ball = FindObjectOfType<Ball>();
        if(ball!=null) ball.started = false;
    }

    public void allDone()
    {
        StartCoroutine(allDoneHelper());
    }

    IEnumerator allDoneHelper()
    {
        yield return new WaitForSeconds(1.5f);
        LoadLevel("Pass");
    }
    public static void LoadLevel(string name)
    {
        if (GameObject.FindObjectOfType<Paddle>()!=null)
            GameObject.FindObjectOfType<Paddle>().resetSize();
        Block.numBlocks = 0;
        Ball.numBalls = 0;
        Ball.secondsStrong = 0;
        PlayerFire.secondsFire = 0;
        LevelManager.loadLevelStatic(name);
    }

    IEnumerator ticker()//cont counter
    {
        while (true)
        {
            if (Ball.secondsStrong > 0) Ball.secondsStrong--;
            if (PlayerFire.secondsFire > 0) PlayerFire.secondsFire--;
            yield return new WaitForSeconds(1);
        }
        
    }


  
};
