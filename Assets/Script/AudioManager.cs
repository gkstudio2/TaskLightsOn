using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource AS_BGmusic;
    public AudioSource AS_Soundsfx;
    public AudioClip AC_BGmusic1, Winaudio;
    public AudioClip AC_button;
    float ConstantVolume = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        ConstantVolume = GetMusicPreferance();
        //  Debug.LogError("Volume loaded == " + ConstantVolume);
        MuteAudio(ConstantVolume);
    }
    public void SaveMusicPreferance(float Vol)
    {
        PlayerPrefs.SetFloat("Music", Vol);
    }
    public float GetMusicPreferance()
    {
        float Vol = PlayerPrefs.GetFloat("Music", 0.5f);
        return Vol;
    }
    public void MuteAudio(float Volume)
    {
        ConstantVolume = Volume;
        AS_Soundsfx.volume =  AS_BGmusic.volume = Volume;
        SaveMusicPreferance(Volume);
        // Debug.LogError("Volume saved == " + ConstantVolume);
    }
    public void PlayOneshot(float volume = 0.5f)
    {
        AS_Soundsfx.PlayOneShot(AC_button);
        if (ConstantVolume > 0)
        {
            AS_Soundsfx.volume = volume;
        }
    }
    public void LevelWin()
    {
        AS_Soundsfx.clip = Winaudio;
        AS_Soundsfx.Play();
    }
}
