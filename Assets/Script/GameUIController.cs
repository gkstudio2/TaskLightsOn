using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameUIController : MonoBehaviour
{
    public GameObject Playpopup;
    public GameObject Winmenupopup;
    public GameObject Pausepopup;
    public GameObject Exitpopup;
    public GameObject[] levelbuttons;
    public TextMeshProUGUI score;
    int unlockedlevels;
    void Start()
    {
        Playpopup.SetActive(false);
        Pausepopup.SetActive(false);
        Exitpopup.SetActive(false);
        Winmenupopup.SetActive(false);
        openplaypopup();
    }

    public void OpenExitPopup()
    {
        Playpopup.SetActive(false);
        Pausepopup.SetActive(false);
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

    public void backtomenu()
    {
        AudioManager.Instance.PlayOneshot();
        SceneManager.LoadScene("Menu");
    }
    public void Restartlevel()
    {
        AudioManager.Instance.PlayOneshot();
        SceneManager.LoadScene("Game");
        
    }

    public void Pausebuttonpressed()
    { 
        AudioManager.Instance.PlayOneshot();
        Pausepopup.SetActive(true);
        AudioManager.Instance.PlayOneshot();
        Time.timeScale = 0;
    }
    public void Playbuttonpressed()
    {
        Pausepopup.SetActive(false);
        AudioManager.Instance.PlayOneshot();
        Time.timeScale = 1;
    }

    void openplaypopup()
    {
        Playpopup.SetActive(true);
        score.text=Playerlevel.Instance.Getsavedscore(Playerlevel.Instance.GetCurrentselectedlevels()).ToString();
    }
    public void StartPlaying()
    {
        Playpopup.SetActive(false);
        GameManager.Instance.startlevel();
    }
}
