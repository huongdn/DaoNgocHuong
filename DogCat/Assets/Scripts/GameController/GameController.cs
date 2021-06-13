using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController m_sInstance;

    public static float m_fWorldWidth, m_fWorldHeight;

    private void Awake()
    {
        _MakeInstance();
        m_fWorldHeight = Camera.main.orthographicSize; // Camera Height
        m_fWorldWidth = m_fWorldHeight * Screen.width / Screen.height; // Camera Width
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
}
