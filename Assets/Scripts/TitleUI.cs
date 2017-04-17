using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown("z")) SceneManager.LoadScene("Main");
    }
}
