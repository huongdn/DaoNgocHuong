using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject m_particleSystem;
    private void Start()
    {
        m_particleSystem = GameObject.FindGameObjectsWithTag("BulletParticalSystem")[0];
        if (gameObject.CompareTag("Bullet"))
        {
            _DisableParticalSystem();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SideEdge") || collision.CompareTag("Helicopter"))
        {
            Destroy(gameObject);
        }
    }

    //private void Update()
    //{
    //    // Destroy remain Bullet when new game start
    //    if (GameController.m_sInstance)
    //    {
    //        if (GameController.m_sInstance._IsStartedNewGame())
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    public void _DisableParticalSystem()
    {
        if(m_particleSystem)
        {
            m_particleSystem.SetActive(false);
        }
    }
    public void _EnableParticalSystem()
        {
            if(m_particleSystem)
            {
                m_particleSystem.SetActive(true);
            }
        }
}
