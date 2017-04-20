using UnityEngine;
using System.Collections;

public class FadeCanvas : MonoBehaviour {
    public float fadeInTime;
    CanvasGroup cg;
	// Use this for initialization
	void Start () {
        cg = GetComponent<CanvasGroup>();
        StartCoroutine(fade());
	}

    IEnumerator fade()
    {
        float precision = .01f;
        for (float f = 0; f <= 1; f += precision)
        {
            cg.alpha = f;
            yield return new WaitForSeconds(precision * fadeInTime);
        }
    }


}
