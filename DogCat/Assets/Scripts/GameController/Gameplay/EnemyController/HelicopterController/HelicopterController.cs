using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_HelicopterRef;

    [SerializeField]
    private float m_spawnerTime;

    private float m_nextSpawnTime;

    private bool m_canNextHelicopterSpawned;

    // Start is called before the first frame update
    void Start()
    {
        m_canNextHelicopterSpawned = false;
        m_nextSpawnTime = 0f;
    }

    private void Update()
    {
        if (GameController.m_sInstance)
        {
            if (GameController.m_sInstance._IsGameplayStarted() && _CanNextHelicopterSpawn())
            {
                Quaternion leftIdentity = Quaternion.identity;
                Quaternion rightIdentity = leftIdentity;
                rightIdentity.y = 180;

                _SpawnHelicopter(/*worldHeight, worldWidth,*/ leftIdentity, rightIdentity);                
            }
        }
    }
    
    void _SpawnHelicopter(/*float worldHeight, float worldWidth,*/ Quaternion leftIdentity, Quaternion rightIdentity)
    {
        if (GameController.m_sInstance)
        {
            if(GameController.m_sInstance._IsGameplayStarted())
            {
                m_nextSpawnTime = Time.time + m_spawnerTime;

                Vector2 leftPos = new Vector2(-GameController.m_sInstance._GetWorldWidth(), GameController.m_sInstance._GetWorldHeight());
                Vector2 rightPos = new Vector2(GameController.m_sInstance._GetWorldWidth(), GameController.m_sInstance._GetWorldHeight());

                if (Random.Range(1, 5) % 2 == 0)
                {
                    //Spawn left side helicopter
                    leftPos.y -= Random.Range(0.5f, 1.5f);

                    //Debug.Log("leftPos.y:" + leftPos.y);
                    Instantiate(m_HelicopterRef, leftPos, leftIdentity);
                }
                else
                {
                    //Spawn right side helicopter
                    rightPos.y -= Random.Range(0.5f, 1.5f);
                    //Debug.Log("rightPos.y:" + rightPos.y);
                    Instantiate(m_HelicopterRef, rightPos, rightIdentity);
                }
            }
        }
    }

    bool _CanNextHelicopterSpawn()
    {
        if ((Time.time < m_nextSpawnTime))
        {
            m_canNextHelicopterSpawned = false;
        }
        else
        {
            m_canNextHelicopterSpawned = true;
        }
        return m_canNextHelicopterSpawned;
    }
}
