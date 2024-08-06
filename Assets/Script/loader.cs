using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loader : MonoBehaviour
{
   public void OpenMapScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
