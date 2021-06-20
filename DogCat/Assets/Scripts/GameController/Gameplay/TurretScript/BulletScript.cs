using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IPooledObject
{
    GameObject m_particleSystem;

    ObjectPooler objectPooler;
    private string bulletPoolTag;

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
            //Destroy(gameObject);
            _ReturnToPool();
        }
    }

    private void _ReturnToPool()
    {
        objectPooler._ReturnObjectToPool(gameObject, bulletPoolTag);
    }

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

    public void _OnObjectSpawn()
    {
        gameObject.SetActive(true);
        objectPooler = ObjectPooler.m_sInstance;
        bulletPoolTag = "Bullet";

        _EnableParticalSystem();

        //StartCoroutine(_ReturnToPool());
    }

    public void _OnObjectReturn()
    {

    }
}
