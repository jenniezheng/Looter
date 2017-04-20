using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour {
    public float waitSeconds;

	void Start () {
        StartCoroutine(Next());
	}

	IEnumerator Next() {
       yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene("Menu");
	}
}
