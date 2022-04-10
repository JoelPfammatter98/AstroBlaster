using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IGameOverTrigger
{
    [SerializeField] private GameObject UI;
    private GameObject OverheatUI;
    private GameObject ScoreUI;
    private GameObject FuelUI;
    private GameObject GameOverUI;
    private int OverheatValue = 0;
    private Text OverheatUIText;
    private Text ScoreUIText;
    private Text FuelUIText;
    private Text GameOverUIText;
    private bool OverheatBlock = false;

    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    public Text HealthUIText { get; private set; }

    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        OverheatUI = GameObject.FindWithTag("OverheatUIText");
        OverheatUIText = OverheatUI.GetComponent<Text>();
        OverheatUIText.text = "Overheat: " + OverheatValue + "%";

        ScoreUI = GameObject.FindWithTag("ScoreUIText");
        ScoreUIText = ScoreUI.GetComponent<Text>();

        FuelUI = GameObject.FindWithTag("FuelUIText");
        FuelUIText = FuelUI.GetComponent<Text>();

        GameOverUI = GameObject.FindWithTag("GameOverUIText");
        GameOverUIText = GameOverUI.GetComponent<Text>();

        var healthUI = GameObject.FindWithTag("HealthUIText");
        HealthUIText = healthUI.GetComponent<Text>();

        GameOverUI.SetActive(false);

        Cooling();
    }

    public void UpdateOverheatUp() {
        if(OverheatValue < 90) {
            OverheatValue = OverheatValue + 10;
        }
        else {
            OverheatValue = OverheatValue + 10;
            OverheatBlock = true;
        }
        OverheatUIText.text = "Overheat: " + OverheatValue + "%";
    }

    public bool GetOverheatBlock() {
        return OverheatBlock;
    }

    private void Cooling() {
        StartCoroutine(StartCooling(0.5f));
    }
 
    IEnumerator StartCooling(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if(OverheatValue > 0) {
            if(OverheatValue <= 50) {
                if(OverheatValue > 15) {
                    OverheatValue = OverheatValue - 15;
                }
                else if(OverheatValue == 10){
                    OverheatValue = OverheatValue - 10;
                }
                else {
                    OverheatValue = OverheatValue - 5;
                }
            }
            else {
                OverheatValue = OverheatValue - 10;
            }

            OverheatUIText.text = "Overheat: " + OverheatValue + "%";
            if(OverheatValue <= 90) {
                OverheatBlock = false;
            }
        }
        yield return StartCooling(delayTime);
    }

    public void UpdateScore(int value) {
        ScoreUIText.text = "score: " + value;
    }

    public void UpdateFuel(int value) {
        FuelUIText.text = "Fuel: " + value + "%";
    }

    public void UpdateHealth(int amount)
    {
        if (HealthUIText == null)
        {
            Debug.LogWarning($"No heatlh UI component defined. Health display will not be updated.");
        }
        else
        {
            HealthUIText.text = $"Health: {amount}";
        }
    }

    public void GameOver() {
        int FinalScore = ScoreManager.Instance.CalculateFinalScore();
        GameOverUI.SetActive(true);
        ScoreUIText.text = "score: " + FinalScore;
        Time.timeScale = 0;
        FuelManager.Instance.StopFuelDecrease();
    }
}
