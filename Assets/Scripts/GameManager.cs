using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    [HideInInspector]public bool isGameOver;
    [HideInInspector]public bool isActiveGame;

    public AudioClip seGameOver;

    void Awake()
    {
        //シングルトン処理
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        StopGame();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z")) StartGame();
        if (Input.GetKeyDown("z") && isGameOver) SceneManager.LoadScene("Main");

    }

    public void StopGame()
    {
        Time.timeScale = 0;
        isActiveGame = false;
    }

    public void GameOver()
    {
        SoundManager.instance.StopBgm();
        SoundManager.instance.SeSound(seGameOver,0.1f);
        Time.timeScale = 0;
        isGameOver = true;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        isActiveGame = true;
    }
}
