using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject cameraobject;
    [SerializeField] private float camSpeed;
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject levelLoseScreen;
    [SerializeField] private GameObject GameEndScreen;
    public void ChangeScene(string sceneName)
    {
        AudioManager.Instance.PlaySFX("Click_SFX");
        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable() {
        ZombieBase.zombieWin += DefeatedGame;
        SpawnManager.winGame += WinGame;
    }
    private void OnDisable() {
        ZombieBase.zombieWin -= DefeatedGame;
        SpawnManager.winGame -= WinGame;
    }

    public void ResumeGame()
    {
       AudioManager.Instance.PlaySFX("Click_SFX");
       GameManager.Instance.ChangeState(GameManager.GameState.GameState);
    }

    public void PauseGame()
    {
        AudioManager.Instance.PlaySFX("Click_SFX");
        GameManager.Instance.ChangeState(GameManager.GameState.PauseState);
    }

    public void WinGame()
    {
        AudioManager.Instance.PlaySFX("Level_Completed_SFX");
        GameEndScreen.SetActive(true);
        AudioManager.Instance.PlaySFX("Man_SFX");
    }

    public void NextLevel()
    {
        //Reset için
        //PlayerPrefs.DeleteAll();
        int levelIndex = PlayerPrefs.GetInt("levelIndex");
        PlayerPrefs.SetInt("levelIndex", levelIndex + 1);
        SceneManager.LoadScene("Chapter1");
    }
    public void DefeatedGame()
    {
        AudioManager.Instance.PlaySFX("Defeated_SFX");
        GameManager.Instance.ChangeState(GameManager.GameState.EndState);
        StartCoroutine(MoveTarget());
    }
    IEnumerator MoveTarget()
    {
        Time.timeScale= 0f;
        Vector3 moveTarget = startPoint.position;
        moveTarget.z = -10;
        moveTarget.y = cameraobject.transform.position.y;
        while (Mathf.Abs(cameraobject.transform.position.x - moveTarget.x) >= 0.1f)
        {
            cameraobject.transform.position = Vector3.MoveTowards(cameraobject.transform.position, moveTarget, camSpeed * Time.unscaledDeltaTime);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(2f);
        levelLoseScreen.SetActive(true);
    }

}
