using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController m_sInstance;

    public static float m_fWorldWidth, m_fWorldHeight;

    public static int m_bGameplayState; // Store game state: Start = 0, GamePlay = 1, Result = 2

    public static int m_iScore;

    private static bool m_bIsStartedNewGame;

    private void Awake()
    {
        _MakeInstance();

        m_fWorldHeight = Camera.main.orthographicSize; // Camera Height
        m_fWorldWidth = m_fWorldHeight * Screen.width / Screen.height; // Camera Width

        m_bGameplayState = 0;
        _StartNewgame();
    }

    public void _StartGameplay()
    {
        m_bGameplayState = 1;
        m_iScore = 0;
        m_bIsStartedNewGame = false;

    }

    public void _StartNewgame()
    {
        m_bIsStartedNewGame = true;
    }

    public bool _IsStartedNewGame()
    {
        return m_bIsStartedNewGame;
    }
    public void _StopGameplay()
    {
        m_bGameplayState = 2;
        if(UIController.m_sInstance)
        {
            UIController.m_sInstance._ShowEndGameUI();
        }
    }     
    public void _StartMenu()
    {
        m_bGameplayState = 0;
    }

    public void _SetGameplayState(int state)
    {
        m_bGameplayState = state;
    }

    public int _GetScore()
    {
        return m_iScore;
    }

    public bool _IsGameplayStarted()
    {
        return m_bGameplayState == 1;
    }
    
    public bool _IsEndGameMenu()
    {
        return m_bGameplayState == 2;
    }
     public bool _IsStartMenu()
    {
        return m_bGameplayState == 0;
    }


    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }    

    void _MakeInstance()
    {
        if (m_sInstance == null)
        {
            m_sInstance = this;
        }
    }

    public float _GetWorldWidth()
    {
        return m_fWorldWidth;
    }

    public float _GetWorldHeight()
    {
        return m_fWorldHeight;
    }

    public void _IncreaseScore()
    {
        m_iScore ++;
    }
}
