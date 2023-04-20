using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    //  Singleton
    public static GameManager Instance { get; private set; }

    //  Settings
    [SerializeField] private float scrollSpeed = 5f;

    public bool GameStarted { get; set; }
    public bool GamePlaying { get; private set; }
    public int Score { get; set; }

    //  References
    private BackgroundManager bgManager;
    [SerializeField] private GameObject player;

    //  Events
    public Action OnGameStart;
    public Action OnGameEnd;
    public Action<float> OnPointGain;
    public Action OnPointChange;
    public Action OnHealthLoss;

    void Awake()
    {
        if (Instance != this)
        {
            Instance = this;
        }

        bgManager = GetComponent<BackgroundManager>();
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        OnGameStart += GameStart;
        OnGameEnd += GameEnd;
        OnGameEnd += UpdateGold;
        OnPointGain += PointGain;
    }

    private void OnDisable()
    {
        OnGameStart -= GameStart;
        OnGameEnd -= GameEnd;
        OnGameEnd -= UpdateGold;
        OnPointGain -= PointGain;
    }

    private void UpdateGold()
    {
        CoinManager.Instance.AddCoins(Score);
    }

    private void PointGain(float points)
    {
        Score += (int)points;
        UIManager.Instance.UpdateScoreText();
        OnPointChange?.Invoke();
    }

    private void GameStart()
    {
        GamePlaying = true;
        player.SetActive(true);
        UIManager.Instance.CloseStartText();
        bgManager.scrollSpeed = scrollSpeed;
    }

    private void GameEnd()
    {
        GamePlaying = false;
        player.SetActive(false);
        UIManager.Instance.DisplayEndText();
        bgManager.scrollSpeed = 0f;

        if (PlayerPrefs.GetInt("highscore", 0) < Score)
        {
            PlayerPrefs.SetInt("highscore", Score);
            UIManager.Instance.UpdateHighScoreText();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
