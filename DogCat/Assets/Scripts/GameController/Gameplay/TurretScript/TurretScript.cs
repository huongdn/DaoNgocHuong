using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class TurretScript : MonoBehaviour
{
    private const string FIREBUTTON = "Fire1";
    [SerializeField]
    private GameObject m_defaultBulletRef;

    [SerializeField]
    private float m_fFireRate;

    private GameObject m_turretBullet;

    private Transform m_defaultBulletTranform;
    private bool m_bIsTurretReloaded;
    private float m_fNextFireTime;

    AudioSource m_firedBulletSFXRef;
    //private BulletScript m_bulletScript;

    private void Awake()
    {
        m_defaultBulletTranform = m_defaultBulletRef.transform;
        m_bIsTurretReloaded = false;

        m_fNextFireTime = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject1 = Instantiate(m_defaultBulletRef, m_defaultBulletTranform.position, m_defaultBulletTranform.rotation);
        m_turretBullet = gameObject1;

        m_firedBulletSFXRef = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.m_sInstance && GameController.m_sInstance._IsGameplayStarted())
        {
            if(_IsTurretReloaded())
            {
                //Set Bullet direction
                _SetbulletDirection();

                //Check fire event - mouse button left down
                if (Input.GetButton(FIREBUTTON))
                {
                    _FireBullet();
                }
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
            if(m_turretBullet)
            {
                m_turretBullet.transform.up = direction;
            }
            //Debug.Log("Bullet.transform.up: x:" + Bullet.transform.up.x +" y:" + Bullet.transform.up.y +" z:" +Bullet.transform.up.z);
    }

    void _FireBullet()
    {
        m_fNextFireTime = Time.time + m_fFireRate;
        GameObject FiredBullet = Instantiate(m_defaultBulletRef, m_turretBullet.transform.position, m_turretBullet.transform.rotation);
        FiredBullet.GetComponent<Rigidbody2D>().velocity = FiredBullet.transform.up * 10f;
        FiredBullet.tag = "FiredBullet";
        //FiredBullet.GetComponent<BulletScript>()._EnableParticalSystem();

        m_firedBulletSFXRef.PlayOneShot(m_firedBulletSFXRef.clip, 1f);
        m_turretBullet.SetActive(false);
    }
    
    bool _IsTurretReloaded()
    {
        if ((Time.time < m_fNextFireTime))
        {
            m_bIsTurretReloaded = false;
        }
        else
        {
            if(m_turretBullet)
            {
                m_turretBullet.SetActive(true);
            }
            m_bIsTurretReloaded = true;
        }
        return m_bIsTurretReloaded;
    }
}
