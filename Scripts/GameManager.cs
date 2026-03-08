using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameState { GS_GAME, GS_PAUSEMENU }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentState = GameState.GS_GAME;

    public Canvas gameHUD;
    public Canvas pauseMenu;

    private int score = 0;
    public TMP_Text labelScore;

    public void InGame()
    {
        SetGameState(GameState.GS_GAME);
    }

    public void PauseMenu()
    {
        SetGameState(GameState.GS_PAUSEMENU);
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;

        if (gameHUD != null)
            gameHUD.enabled = (currentState == GameState.GS_GAME);

        if (pauseMenu != null)
        {
            pauseMenu.enabled = (currentState == GameState.GS_PAUSEMENU);

            CanvasGroup cg = pauseMenu.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                bool isPause = (currentState == GameState.GS_PAUSEMENU);
                cg.alpha = isPause ? 1f : 0f;
                cg.interactable = isPause;
                cg.blocksRaycasts = isPause;
            }
        }

        Time.timeScale = (currentState == GameState.GS_GAME) ? 1f : 0f;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InGame();  
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (labelScore != null)
            labelScore.text = "Score: " + score;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && currentState == GameState.GS_GAME)
        {
            PauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentState == GameState.GS_PAUSEMENU)
        {
            InGame();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        if (labelScore != null)
            labelScore.text = score.ToString();
    }

    public void OnResumeButtonClicked()
    {
        InGame();
    }

    public void OnRestartButtonClicked()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    public void OnReturnToMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnLevel2ButtonClicked() 
    { 
        SceneManager.LoadScene("Level2"); 
    }
}
