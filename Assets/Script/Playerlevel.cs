using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlevel : MonoBehaviour
{
    public static Playerlevel Instance;
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
    int currentlevel;
    public int GetCurrentselectedlevels()
    {
        currentlevel = PlayerPrefs.GetInt("CurrentSelectedlevel", 0);
        return currentlevel;
    }
    public void SetCurrentselectedlevels(int i)
    {
       PlayerPrefs.SetInt("CurrentSelectedlevel", i);
    }
    int unlockedlevel;
    public int GetMaxUnlockedlevels()
    {
        unlockedlevel = PlayerPrefs.GetInt("Maxunlockedlevels", 0);
        return unlockedlevel;
    }
    public void SetMaxUnlockedlevels(int i)
    {
        PlayerPrefs.SetInt("Maxunlockedlevels", i);
    }

    public void Scoresaver(int level,int score)
    {
        PlayerPrefs.SetInt("Level"+level.ToString(), score);
    }
    public int Getsavedscore(int level)
    {
       int score= PlayerPrefs.GetInt("Level"+level.ToString(), 0);
       return score;
    }
}
