using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour{
    public static GameManager instance;
    public enum GameState{
        Gameplay,
        Paused,
        GameOver
    }

    // current state of game
    public GameState currentState;
    public GameState previousState;
    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;

    // Current stat displays
    public TMP_Text currentHealthDisplay;
    public TMP_Text currentRecoveryDisplay;
    public TMP_Text currentDamageDisplay;
    public TMP_Text currentDriftDisplay;
    public TMP_Text currentAccelDisplay;
    public TMP_Text currentHandlingDisplay;
    public TMP_Text currentSpeedDisplay;

    public bool isGameOver = false;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
        }

        DisableScreens();
    }
    
    void Update(){

        // behavior for each state
        switch (currentState){
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;
            
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            
            case GameState.GameOver:
                if(!isGameOver){
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("Game over");
                    DisplayResults();
                }
                break;
            
            default:
                // catch errors
                Debug.LogWarning("nonexistent state");
                break;
        }
    }

    // change state of game
    public void ChangeState(GameState newState){
        currentState = newState;
    }

    public void PauseGame(){
        if(currentState != GameState.Paused){
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; // stop game
            pauseScreen.SetActive(true);
            Debug.Log("Pause game");
        }
    }

    public void ResumeGame(){
        if(currentState == GameState.Paused){
            ChangeState(previousState);
            Time.timeScale = 1f; // reusme game
            pauseScreen.SetActive(false);
            Debug.Log("Resume game");
        }
    }

    void CheckForPauseAndResume(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(currentState == GameState.Paused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
        }
    }

    void DisableScreens(){
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void GameOver(){
        ChangeState(GameState.GameOver);
    }

    void DisplayResults(){
        resultsScreen.SetActive(true);
    }
}
