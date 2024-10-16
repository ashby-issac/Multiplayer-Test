using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private PauseMenu pauseMenuSimulation;
    //[SerializeField] private PlayerStats playerStats;

    public static bool isSimulating;

    public static Action<int> OnScoreChange;

    private void Awake()
    {
        isSimulating = false;

        pauseMenuSimulation.Initialize(this);
        //playerStats.Initialize(this);
    }

    private void OnEnable()
    {
        OnScoreChange += UpdateScoreText;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    private void OnDisable()
    {
        OnScoreChange -= UpdateScoreText;
    }
}
