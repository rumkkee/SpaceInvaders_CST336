using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _scorePanel;

    private IEnumerator Start()
    {
        ToggleStartScreen(true);
        yield return new WaitForSeconds(4f);
        ToggleStartScreen(false);
        yield return new WaitForSeconds(1f);
        StartGame();
    }

    private void ToggleStartScreen(bool isEnabled)
    {
        _scorePanel.gameObject.SetActive(isEnabled);
    }

    private void StartGame()
    {

    }
}
