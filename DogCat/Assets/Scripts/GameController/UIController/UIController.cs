using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [HideInInspector]
    public static UIController m_sInstance;

    [SerializeField]
    private Text m_gameplayScoreText, m_gameEndScoreText;

    [SerializeField]
    private GameObject m_StartGameUI, m_EndGameUI;

    void Update()
    {
        if (GameController.m_sInstance)
        {
            if (GameController.m_sInstance._IsGameplayStarted())
            {
                m_sInstance._SetGameplayScoreText();
            }
        }
    }
    private void Awake()
    {
        _MakeInstance();
        m_sInstance._ShowStartGameUI();
        m_sInstance._HideEndGameUI();
    }

    private void _MakeInstance()
    {
        if (m_sInstance == null)
        {
            m_sInstance = this;
            //DontDestroyOnLoad(this);
        }
        else if (m_sInstance != null)
        {
            Destroy(this);
        }
    }
    public void _ShowEndGameUI()
    {
        if (m_EndGameUI)
        {
            m_EndGameUI.SetActive(true);
            m_sInstance._SetGameEndScoreText();
        }
    }
    public void _HideEndGameUI()
    {
        if (m_EndGameUI)
        {
            m_EndGameUI.SetActive(false);
        }
    }

    public void _ShowStartGameUI()
    {
        m_StartGameUI.SetActive(true);
    }
    public void _HideStartGameUI()
    {
        if (m_StartGameUI)
        {
            m_StartGameUI.SetActive(false);
        }
    }

    private void _SetGameplayScoreText()
    {
        if(GameController.m_sInstance)
        {
            if (GameController.m_sInstance._IsGameplayStarted() && m_gameplayScoreText)
            {
                m_gameplayScoreText.text = GameController.m_sInstance._GetScore().ToString();
            }
        }
    }

    public void _SetGameEndScoreText()
    {
        if (GameController.m_sInstance)
        {
            if (!GameController.m_sInstance._IsGameplayStarted() && m_gameplayScoreText)
            {
                m_sInstance.m_gameEndScoreText.text = GameController.m_sInstance._GetScore().ToString();
            }
        }
    }

    public void _OnButtonStartGameClick()
    {
        m_sInstance._HideStartGameUI();
        if (GameController.m_sInstance)
        {
            GameController.m_sInstance._StartGameplay();
        }
    }
    public void _OnButtonReplayClick()
    {
        m_sInstance._HideEndGameUI();
        m_sInstance._ShowStartGameUI();
        //if (GameController.m_sInstance)
        //{
        //    GameController.m_sInstance._StartNewgame();
        //}
    }
}
