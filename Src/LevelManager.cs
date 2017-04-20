using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelManager: MonoBehaviour {
	

    public void loadLevel(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void nextLevel()
    {
        GamingManager.nextLevel();
    }

    public void selectLevel(int level)
    {
        GamingManager.selectLevel(level);
    }




    public void restartLevel()
    {
        GamingManager.restartLevel();
    }

    public void restartAll()
    {
        GamingManager.restartAll();
    }

    public static void loadLevelStatic(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public static void nextLevelStatic()
    {
        int id = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(id);
    }

}
