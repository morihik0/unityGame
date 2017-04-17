using UnityEngine;
using System.Collections;

public class GameOverMsg : MonoBehaviour {

    public AudioClip seGameOverBouise;
    private bool isSE;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.isGameOver && transform.position.y > 50) transform.position = new Vector3(0, transform.position.y - 8, 0);
        else if(transform.position.y <= 50 && !isSE)
        {
            isSE = true;
            SoundManager.instance.SeSound(seGameOverBouise, 0.3f);
        }
	}
}
