using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    [SerializeField]
    private float m_fLandingSpeed;

    [SerializeField]
    private GameObject m_deadSoldierRef;

    private Vector2 m_vBulletForce/*, m_vCollisionContactPos*/;

    private bool m_bIsHitted, m_bIsLanded;

    GameObject m_deadSoldier;

    [SerializeField]
    private float m_fForceValue;

    void Start()
    {
        m_bIsHitted = false;
        m_bIsLanded = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Soldier landing
        if(!_IsHitted() && !_IsLanded())
        {
            _SoldierLanding();
        }

        // Destroy soldier if hitted by bullet
        if(_IsHitted())
        {
            _DestroySoldier();
        }

        // Make Soldier go to base after landed
        if(_IsLanded())
        {
            _MoveToBase();
        }
    }

    private void _MoveToBase()
    {
        throw new NotImplementedException();
    }

    private void _DestroySoldier()
    {
        m_deadSoldier = Instantiate<GameObject>(m_deadSoldierRef);

        //GameObject explosion = Instantiate<GameObject>(m_ExplosionRef);

        //Map the deadSoldier to the Soldier
        m_deadSoldier.transform.position = transform.position;

        // Add force to deadSoldier
        Rigidbody2D deadSoldierRigidbody = m_deadSoldier.GetComponent<Rigidbody2D>();
        deadSoldierRigidbody.AddForce(m_vBulletForce * m_fForceValue);
        //deadSoldierRigidbody.AddForceAtPosition(m_vBulletForce * m_fForceValue, m_vCollisionContactPos);

        Destroy(gameObject);
    }

    void _SoldierLanding()
    {
        Vector3 tempPos = transform.position;

        tempPos.y -= m_fLandingSpeed * Time.deltaTime;

        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FiredBullet" && !_IsHitted())
        {
            m_bIsHitted = true;
            //m_vBulletForce.x = collision.gameObject.transform.position.x;
            //m_vBulletForce.y = collision.gameObject.transform.position.y;

            m_vBulletForce = collision.gameObject.transform.up;

            //Try to get collision point
            //m_vCollisionContactPos = collision.gameObject.GetComponent<Collider2D>().ClosestPoint(transform.position);

            //Debug.Log("m_vCollisionContactPos:" + m_vCollisionContactPos.x + " " + m_vCollisionContactPos.y);
            //Debug.Log("m_vBulletForce:" + m_vBulletForce.x + " " + m_vBulletForce.y);
        }

        if (collision.tag == "GroundEdge")
        { 
            if (!m_bIsHitted)
            {
                m_bIsLanded = true;
            }
        }
    }

    bool _IsHitted()
    {
        return m_bIsHitted;
    }   
    bool _IsLanded()
    {
        return m_bIsLanded;
    }
}
