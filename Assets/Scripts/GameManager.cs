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
}
