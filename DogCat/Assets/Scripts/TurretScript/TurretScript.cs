using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class TurretScript : MonoBehaviour
{
    public GameObject m_defaultBullet;
    private Transform m_defaultBulletTranform;
    private bool m_bIsTurretReloaded;
    private float m_fNextFireTime;
    public float m_fFireRate;

    // Start is called before the first frame update
    void Start()
    {
        m_defaultBulletTranform = m_defaultBullet.transform;
        m_bIsTurretReloaded = false;
        
        m_fNextFireTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_IsTurretReloaded())
        {
            //Set Bullet direction
            _SetbulletDirection();

            //Check fire event - mouse button left down
            if (Input.GetMouseButtonDown(0))
            {
                _FireBullet();
            }
        }        
    }
    
    void _SetbulletDirection()
    {
            // Get direction for bullet from mouse position
            Vector2 direction = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);

            // Cap direction of bullet, don't let it too low
            if (direction.y < 0)
            {
                direction.y = 0;
            }

            m_defaultBullet.transform.up = direction;
            //Debug.Log("Bullet.transform.up: x:" + Bullet.transform.up.x +" y:" + Bullet.transform.up.y +" z:" +Bullet.transform.up.z);
    }

    void _FireBullet()
    {
        m_fNextFireTime = Time.time + m_fFireRate;
        GameObject FiredBullet = Instantiate(m_defaultBullet, m_defaultBullet.transform.position, m_defaultBullet.transform.rotation);
        m_defaultBullet.SetActive(false);
        FiredBullet.GetComponent<Rigidbody2D>().velocity = FiredBullet.transform.up * 10f;
    }
    
    bool _IsTurretReloaded()
    {
        if ((Time.time < m_fNextFireTime))
        {
            m_bIsTurretReloaded = false;
        }
        else
        {
            m_defaultBullet.SetActive(true);
            m_bIsTurretReloaded = true;
        }
        return m_bIsTurretReloaded;
    }
}
