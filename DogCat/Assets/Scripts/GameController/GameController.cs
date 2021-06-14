using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController m_sInstance;

    public static float m_fWorldWidth, m_fWorldHeight;

    public static bool m_bIsGamePlaySarted;

    public static int m_iScore;

    private void Awake()
    {
        _MakeInstance();

        m_fWorldHeight = Camera.main.orthographicSize; // Camera Height
        m_fWorldWidth = m_fWorldHeight * Screen.width / Screen.height; // Camera Width

        _StartGamePlay();
    }

    public void _StopGamePlay()
    {
        m_bIsGamePlaySarted = false;
    }

    public void _StartGamePlay()
    {
        m_bIsGamePlaySarted = true;
        m_iScore = 0;
    }

    public bool _IsGamePlayStarted()
    {
        return m_bIsGamePlaySarted;
    }
    private void Start()
    {
        //_StartGamePlay();
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
