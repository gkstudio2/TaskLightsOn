using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] GameObject OffImage;
    float F_Volume;
    // Start is called before the first frame update
    void Start()
    {
        F_Volume = AudioManager.Instance.GetMusicPreferance();

        if (F_Volume > 0)
        {
            OffImage.SetActive(false);
        }
        else
        {
            OffImage.SetActive(true);
        }
    }

    public void ChangeMute()
    {
        AudioManager.Instance.PlayOneshot();
        if (F_Volume > 0)
        {
            F_Volume = 0;
            OffImage.SetActive(true);
            AudioManager.Instance.MuteAudio(F_Volume);
        }
        else
        {
            F_Volume = 0.5f;
            OffImage.SetActive(false);
            AudioManager.Instance.MuteAudio(F_Volume);
        }
    }

}
