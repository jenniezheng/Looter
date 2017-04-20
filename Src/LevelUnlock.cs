using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelUnlock : MonoBehaviour {
    public int i;

	void Start () {

        if (i==1|| PlayerPrefsManager.isLevelUnlocked(i)) //1 always unlocked
            GetComponent<Button>().interactable = true;
        else GetComponent<Button>().interactable = false;
    }
}
