using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LicenseLoad : MonoBehaviour {

    private ScreenFadeManager fadeManager;

    void Awake()
    {
        fadeManager = ScreenFadeManager.Instance;
    }

    // Use this for initialization
    void Start () {
        fadeManager.FadeIn(1.5f, Color.black, () => {
            // フェードイン後に行う処理
            // フェードアウト
            fadeManager.FadeOut(1.5f, Color.black, () => {
                // フェードアウト後に行う処理
                Debug.Log("フェードイン完了");
                SceneManager.LoadScene("Title");
            });
        });
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
