using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameButtons : MonoBehaviour
{
    [SerializeField] private GameObject startTip;
    [SerializeField] private GameObject losingWindow;
    [SerializeField] private GameObject victoryWindow;


    private void OnEnable()
    {
        BallMovement.OnBallActivationEvent += StartTipDeactive;
        LevelLosing.OnLevelLosingEvent += PauseGame;
        LevelLosing.OnLevelLosingEvent += EnableLosingWindow;
        Block.OnLevelCompleteEvent += PauseGame;
        Block.OnLevelCompleteEvent += EnableVictoryWindow;
    }
    private void OnDisable()
    {
        BallMovement.OnBallActivationEvent -= StartTipDeactive;
        LevelLosing.OnLevelLosingEvent -= PauseGame;
        LevelLosing.OnLevelLosingEvent -= EnableLosingWindow;
        Block.OnLevelCompleteEvent -= PauseGame;
        Block.OnLevelCompleteEvent -= EnableVictoryWindow;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void NextLevel()
    {
        int nextLevel = LevelLoader.loadedLevel + 1;
        if (nextLevel <= LevelLoader.maxLevel)
        {
            LevelLoader.loadedLevel = nextLevel;
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
            Debug.Log("Congratulations! The GAME completed!");
        }
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void StartTipDeactive()
    {
        startTip.gameObject.SetActive(false);
    }
    private void EnableLosingWindow()
    {
        losingWindow.SetActive(true);
    }
    private void EnableVictoryWindow()
    {
        victoryWindow.SetActive(true);
    }
}
