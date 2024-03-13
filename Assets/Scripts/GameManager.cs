using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene"))
        {
            StartCoroutine(OnGameStart());
        }
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CreditsScene"))
        {
            StartCoroutine(OnCreditsSceneStart());
        }
    }

    public void ChangeState(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.InGame:
                StartCoroutine(OnGameStart());
                break;
        }
    }

    private IEnumerator OnGameStart()
    {
        yield return new WaitForSeconds(0.5f);
        ToggleStartScreen(true);
        yield return new WaitForSeconds(1.5f);
        ToggleStartScreen(false);
        yield return new WaitForSeconds(0.5f);
        StartGame();
    }

    private void ToggleStartScreen(bool isEnabled)
    {
        ScoreAdvancePanel.instance.gameObject.SetActive(isEnabled);
    }

    private void StartGame()
    {
        EnemyManager.instance.StartRound();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
        //StartCoroutine(LoadGameSceneHelper());
    }

    // Not in use, but keeping for reference
    public IEnumerator LoadGameSceneHelper()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("GameScene");
        while (!asyncOp.isDone)
        {
            yield return null;
        }
        StartCoroutine(OnGameStart());
    }

    public void OnPlayerDefeated()
    {
        Destroy(Player.instance.gameObject);
        StartCoroutine(EndGameRoutine());
    }

    public void OnPlayerWin()
    {
        StartCoroutine(EndGameRoutine());
    }

    public IEnumerator EndGameRoutine()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(WaitForLoadScene("CreditsScene"));
        Debug.Log("Entered Credits");

        yield return StartCoroutine(OnCreditsSceneStart());
    }

    public IEnumerator OnCreditsSceneStart()
    {
        Debug.Log("In Credits");
        yield return new WaitForSeconds(4f);
        yield return StartCoroutine(WaitForLoadScene("MainMenu"));
        Debug.Log("In Main Menu");
    }

    public IEnumerator WaitForLoadScene(string sceneName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOp.isDone)
        {
            yield return null;
        }
    }
}

public enum GameState
{
    MainMenu = 0,
    InGame = 10,
    Credits = 20
}