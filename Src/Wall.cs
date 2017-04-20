using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(destroyAfterTime());
	}
	
	// Update is called once per frame
	IEnumerator destroyAfterTime() {
       yield return new WaitForSeconds(6);
       Destroy(gameObject);
	}
}
