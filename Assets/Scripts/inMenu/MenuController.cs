using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        AudioManager.Instance.PlaySFX("Click_SFX");
        SceneManager.LoadScene(sceneName);
    }

}
