using UnityEngine;
using System.Collections;

public class ReadyText : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.isActiveGame) Destroy(this.gameObject);
	}
}
