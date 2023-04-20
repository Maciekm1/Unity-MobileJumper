using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //  Singleton
    public static UIManager Instance { get; private set; }

    //  References
    [SerializeField] private TMP_Text startText;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private Image blackBG;
    [SerializeField] private TMP_Text goldAmount;

    [SerializeField] private List<Image> HealthList;

    private void OnEnable()
    {
        GameManager.Instance.OnPointChange += ScorePopOut;
        GameManager.Instance.OnGameStart += HideHighscore;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPointChange -= ScorePopOut;
        GameManager.Instance.OnGameStart -= HideHighscore;
    }

    private void ScorePopOut()
    {
        LeanTween.scale(scoreText.gameObject, new Vector2(1.2f, 1.2f), 0.5f).setEaseOutBack();
        LeanTween.scale(scoreText.gameObject, new Vector2(1f, 1f), 0.5f).setEaseOutExpo().setDelay(0.5f);
    }

    private void HideHighscore()
    {
        highscoreText.GetComponent<CanvasGroup>().LeanAlpha(0f, 1.5f).setEaseInOutCubic();
    }


    void Start()
    {
        if (Instance != this)
        {
            Instance = this;
        }

        UpdateHighScoreText();
    }

    public void CloseStartText()
    {
        if (startText != null) { LeanTween.scale(startText.gameObject, Vector3.zero, 1f).setEaseInBack().setOnComplete(DisableStartText); }
    }

    private void DisableStartText()
    {
        startText.enabled = false;
    }

    public void DisplayEndText()
    {
        // Update coins text
        goldAmount.text = CoinManager.Instance.Coins.ToString();

        if (endText != null) { endText.SetActive(true); LeanTween.scale(endText.gameObject, new Vector3(6, 10, 0), 0.3f).setEaseOutBack(); }
        if (restartButton != null) { restartButton.SetActive(true); LeanTween.scale(restartButton.gameObject, new Vector3(4.5f, 7.5f, 0), 0.4f).setEaseOutBack(); }

        if (blackBG != null) { blackBG.GetComponent<CanvasGroup>().LeanAlpha(1f, 0.35f); }
    }

    public void UpdateScoreText()
    {
        if (scoreText != null) { scoreText.text = GameManager.Instance.Score.ToString(); }
    }

    public void UpdateHighScoreText()
    {
        if (highscoreText != null) { highscoreText.text = "High score : " + PlayerPrefs.GetInt("highscore", 0).ToString(); }

        //Gold
        goldAmount.text = CoinManager.Instance.Coins.ToString();
    }

    public void RemoveHealth()
    {
        LeanTween.scale(HealthList[HealthList.Count - 1].gameObject, Vector3.zero, 0.5f).setEaseInBounce().setOnComplete(DisableHealth);
    }

    private void DisableHealth()
    {
        HealthList[HealthList.Count - 1].enabled = false;
        HealthList.RemoveAt(HealthList.Count - 1);
    }

}