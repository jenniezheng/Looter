using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FadeImage : MonoBehaviour
{
    public bool fadeIn;
    public float fadeInTime;
    Image image;
    Color c;
    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
        c = image.color;
        if(fadeIn) StartCoroutine(fadingIn());
        else StartCoroutine(fadingOut());
    }
    
    IEnumerator fadingIn()
    {
        float precision = .01f;
        for (float f = 0; f<= 1; f+=precision)
        {
            image.color = new Color(c.r, c.g, c.b, f);
            yield return new WaitForSeconds(precision* fadeInTime);
        }
    }

    IEnumerator fadingOut()
    {
        float precision = .01f;
        for (float f = 1; f >=0; f -= precision)
        {
            image.color = new Color(c.r, c.g, c.b, f);
            yield return new WaitForSeconds(precision * fadeInTime);
        }
        gameObject.SetActive(false);
    }
}