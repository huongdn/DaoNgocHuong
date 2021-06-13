using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour
{
    [SerializeField]
    private float m_fLandingSpeed;

    private bool m_bIsHitted, m_bIsLanded, m_bIsCrashing, m_bIsAddedRigidbody2D;
    //private Rigidbody2D m_rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        m_bIsHitted = false;
        m_bIsLanded = false;
        m_bIsCrashing = false;
        m_bIsAddedRigidbody2D = false;
        //m_rigidbody2D.mass = 1;
        //m_rigidbody2D.gravityScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_IsHitted())
        {
            _SoldierLanding();
        }
        //_CheckSoldierState();
    }

    void _SoldierLanding()
    {
        Vector3 tempPos = transform.position;

        tempPos.y -= m_fLandingSpeed * Time.deltaTime;

        transform.position = tempPos;
    }

    void _CheckSoldierState()
    {
        if (_IsHitted() && !m_bIsAddedRigidbody2D)
        {
            _AddRigidbody();
            m_bIsAddedRigidbody2D = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FiredBullet" && !_IsHitted())
        {
            m_bIsHitted = true;
            Rigidbody2D gameObjectsRigidBody = gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
            Vector2 force;
            force.x = 1;
            force.y = 2;
            gameObjectsRigidBody.AddForce(force);
        }

        if (collision.tag == "GroundEdge")
        { 
            if (!m_bIsHitted)
            {
                m_bIsLanded = true;
            }
            else
            {
                m_bIsCrashing = true;
            }
        }

        if (collision.tag == "SideEdge")
        {
            m_bIsCrashing = true;
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
    bool _IsCrashing()
    {
        return m_bIsCrashing;
    }   

    void _AddRigidbody()
    {
        //Rigidbody2D gameObjectsRigidBody = gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
        //Rigidbody2D temp = gameObject.GetComponent<Rigidbody2D>();
        //temp.mass = 1;

        //gameObjectsRigidBody.mass = 1; // Set the mass via the Rigidbody.
        //gameObjectsRigidBody.gravityScale = 1;
    }

    void _RemoveRigidbody()
    {
        Destroy(GetComponent<Rigidbody2D>());
    }
}
