using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    [Header("Timer Components")]
    [SerializeField] private float gameTime;
    [SerializeField] TextMeshProUGUI timeTextBox;
    [Header("Time Events")]
    public UnityEvent onTimerExpired;

    private bool allowTimer;
    enum GameState
    {
        Waiting,
        Playing,
        Completed,
        End
    }
    private GameState gamestate;
    // Start is called before the first frame update
    void Start()
    {
        allowTimer = true;
        gamestate = GameState.Playing;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowTimer)
            UpdateGameTimer();

        if (gamestate == GameState.Playing)
            CheckTime();
        
        
    }


    private void UpdateGameTimer()
    {
        gameTime -= Time.deltaTime;

        var minutes = Mathf.FloorToInt(gameTime / 60);
        var seconds = Mathf.FloorToInt(gameTime - minutes * 60);

        string gameTimeClockDisplay = string.Format("{0:0}: {1:00}", minutes, seconds);

        timeTextBox.text = gameTimeClockDisplay;


    }

    private void CheckTime()
    {
        if(gameTime <= 0)
        {
            allowTimer = false;
            timeTextBox.text = "The End";
            onTimerExpired.Invoke();

            gamestate = GameState.End;
        }
    }

    public void TimeDeactivated()
    {
        timeTextBox.text = "Time Stopped";
        allowTimer = false;
        gamestate = GameState.Completed;

    }
}
