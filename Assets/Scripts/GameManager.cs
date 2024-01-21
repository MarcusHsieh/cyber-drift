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

    [Header("Damage Text Popup")]
    public Canvas damageTextCanvas;
    public float textFontSize = 20;
    public TMP_FontAsset textFont;
    public Camera referenceCamera;
    [Header("Stopwatch")]
    public float timeLimit;
    float stopwatchTime;
    public TMP_Text stopwatchDisplay;

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
                UpdateStopwatch();
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

    public static void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f){
        if(!instance.damageTextCanvas) { return; }
        if(!instance.referenceCamera) { instance.referenceCamera = Camera.main; }
        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(text, target, duration, speed));
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

    IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f){
        GameObject textObj = new GameObject("Damage Floating Text");
        RectTransform rect = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI tmPro = textObj.AddComponent<TextMeshProUGUI>();
        tmPro.text = text;
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmPro.verticalAlignment = VerticalAlignmentOptions.Middle;
        tmPro.fontSize = textFontSize;
        if (textFont) tmPro.font = textFont;
        rect.position = referenceCamera.WorldToScreenPoint(target.position);

        Destroy(textObj, duration);

        textObj.transform.SetParent(instance.damageTextCanvas.transform);

        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0;
        float yOffset = 0;
        while(t < duration){
            // wait for frame and update time
            yield return w;
            t += Time.deltaTime;

            // fade text 
            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1-t/duration);

            // float text up
            yOffset += speed * Time.deltaTime;
            rect.position = referenceCamera.WorldToScreenPoint(target.position + new Vector3(0, yOffset));
        }
    }

    void UpdateStopwatch(){
        stopwatchTime += Time.deltaTime;

        UpdateStopwatchDisplay();

        if(stopwatchTime >= timeLimit){
            GameOver();
        }
    }

    void UpdateStopwatchDisplay(){
        int minutes = Mathf.FloorToInt(stopwatchTime/60);
        int seconds = Mathf.FloorToInt(stopwatchTime%60);

        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
