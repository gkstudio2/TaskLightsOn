using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject Levelpopup;
    public GameObject Exitpopup;
    public GameObject[] levelbuttons;
    int unlockedlevels;
    // Start is called before the first frame update
   
    void Start()
    {
        Levelpopup.SetActive(false);
        Exitpopup.SetActive(false);

        getMaxunlockedlevels();
        if (getMaxunlockedlevels() == 0)
        {
            setMaxunlockedlevels();
        }
    }
    #region Exitlogic
    public void OpenExitPopup()
    {
        Levelpopup.SetActive(false);
        Exitpopup.SetActive(true);
        AudioManager.Instance.PlayOneshot();
    }
    public void ExitGame()
    {
        Application.Quit();
        AudioManager.Instance.PlayOneshot();
    }
    public void Closeexitpopup()
    {
        Exitpopup.SetActive(false);
        AudioManager.Instance.PlayOneshot();
    }
    #endregion

    public void OpenLevelsPopup()
    {
        AudioManager.Instance.PlayOneshot();
        Levelpopup.SetActive(true);
        Exitpopup.SetActive(false);
        setingunlockedlevel();
    }

    public void OpenLevels(int lvl)
    {
        Playerlevel.Instance.SetCurrentselectedlevels(lvl);
        AudioManager.Instance.PlayOneshot();
        Debug.Log("OpeningLevel press");
        SceneManager.LoadScene("Game");
    }
  
    public int getMaxunlockedlevels()
    {
        unlockedlevels = PlayerPrefs.GetInt("Maxunlockedlevels", 0);
        return unlockedlevels;
    }
    void setMaxunlockedlevels()
    {
        unlockedlevels = 1;
        PlayerPrefs.SetInt("Maxunlockedlevels", unlockedlevels);
    }
    void setingunlockedlevel()
    {
        getMaxunlockedlevels();
       
        for(int i = 0; i < levelbuttons.Length; i++)
        {
            levelbuttons[i].transform.GetChild(1).gameObject.SetActive(true);
            levelbuttons[i].GetComponent<Button>().interactable= false;

        }
        for (int i = 0; i<unlockedlevels; i++)
        {
            levelbuttons[i].transform.GetChild(1).gameObject.SetActive(false);
            levelbuttons[i].GetComponent<Button>().interactable = true;
        }
    }
   
}
