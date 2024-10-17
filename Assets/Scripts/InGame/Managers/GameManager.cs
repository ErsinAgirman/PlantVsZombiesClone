using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameState currentState;
    [SerializeField] private TextMeshProUGUI gameStartText;
   public static GameManager Instance;
   public static Action gameAction;
   public static Action endAction;
   public static Action gameStartAction;

   private void Start() {
    ChangeState(GameState.StartState);
    AudioManager.Instance.PlayMusic("Music" + UnityEngine.Random.Range(1,7));
    AudioManager.Instance.PlaySFX("Start_SFX");
   }
   private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
   }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch(currentState)
        {
            case GameState.StartState : Time.timeScale= 1f; StartCoroutine(GameStart());  break;
            case GameState.PauseState : Time.timeScale= 0f; endAction.Invoke(); break;
            case GameState.GameState : Time.timeScale= 1f; gameAction?.Invoke(); break;
            case GameState.EndState  : endAction?.Invoke(); break;
        }
    }

    IEnumerator GameStart()
    {
        gameStartText.transform.parent.gameObject.SetActive(true);
        gameStartText.text = "READY!";
        yield return new WaitForSeconds(0.5f);
        gameStartText.text = "SET!";
        yield return new WaitForSeconds(0.5f);
        gameStartText.text = "PLANT!";
        yield return new WaitForSeconds(0.5f);
        gameStartText.transform.parent.gameObject.SetActive(false);
        gameStartAction?.Invoke();
        ChangeState(GameState.GameState);
    }

[System.Serializable]
    public enum GameState
    {
        StartState,
        PauseState,
        GameState,
        EndState
    }
}
