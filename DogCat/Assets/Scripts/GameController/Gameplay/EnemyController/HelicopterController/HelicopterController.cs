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

    ObjectPooler objectPooler;

    string helicopterPoolTag;

    // Start is called before the first frame update
    void Start()
    {
        if(ObjectPooler.m_sInstance)
        {
            objectPooler = ObjectPooler.m_sInstance;
        }

        helicopterPoolTag = "Helicopter";

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

                    //Instantiate(m_HelicopterRef, leftPos, leftIdentity);
                    _SpawnFromPool(leftIdentity, leftPos);
                }
                else
                {
                    //Spawn right side helicopter
                    rightPos.y -= Random.Range(0.5f, 1.5f);
                    //Debug.Log("rightPos.y:" + rightPos.y);

                    //Instantiate(m_HelicopterRef, rightPos, rightIdentity);
                    //GameObject helicopter = objectPooler._SpawnFromPool(helicopterTag);
                    //helicopter.transform.position = rightPos;
                    //helicopter.transform.rotation = rightIdentity;
                    //helicopter.SetActive(true);

                    _SpawnFromPool(rightIdentity, rightPos);
                }
            }
        }
    }

    private void _SpawnFromPool(Quaternion dentity, Vector2 Pos)
    {
        GameObject helicopter = objectPooler._SpawnFromPool(helicopterPoolTag);
        helicopter.transform.position = Pos;
        helicopter.transform.rotation = dentity;
        helicopter.SetActive(true);
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
