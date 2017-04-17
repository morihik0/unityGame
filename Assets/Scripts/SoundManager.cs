using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource seSource; //効果音
    public AudioSource BGMSource; //BGM

    public static SoundManager instance = null;

    void Awake()
    {
        //シングルトン処理
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        //DontDestroyOnLoad(gameObject);
    }
    
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }



    public void PlaySingle(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    public void StopBgm()
    {
        BGMSource.Stop();
    }

    public void SeSound(AudioClip clip, float volume = 1)
    {
        seSource.PlayOneShot(clip, volume);
    }

    public void loadVolume()
    {
        BGMSource.volume = PlayerPrefs.GetFloat("BgmVolume", 0.3f);
        seSource.volume = PlayerPrefs.GetFloat("SeVolume", 0.3f);
    }
}
