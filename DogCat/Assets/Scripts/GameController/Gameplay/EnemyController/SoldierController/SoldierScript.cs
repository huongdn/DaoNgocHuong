using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierScript : MonoBehaviour, IPooledObject
{
    [SerializeField]
    private float m_fLandingSpeed;

    [SerializeField]
    private float m_fMovingSpeed;

    [SerializeField]
    private GameObject m_deadSoldierRef;

    private Vector2 m_vBulletForce/*, m_vCollisionContactPos*/;

    private bool m_bIsHitted, m_bIsLanded, m_bIsReachedBase;

    GameObject m_deadSoldier;

    [SerializeField]
    private float m_fForceValue;

    private Animator m_SoldierAnimator;

    public AudioSource m_SoldiderDeadSFXRef;

    ObjectPooler objectPooler;

    string soldierPoolTag;

    void Start()
    {
        //m_SoldierAnimator = GetComponent<Animator>();
        //_AnimatorSetIsLanded(false);

        //m_bIsHitted = false;
        //m_bIsLanded = false;
        //m_bIsReachedBase = false;

        //m_SoldiderDeadSFXRef = GetComponent<AudioSource>();
    }


    public void _OnObjectSpawn()
    {
        gameObject.SetActive(true);
        objectPooler = ObjectPooler.m_sInstance;
        soldierPoolTag = "Soldier";

        m_SoldierAnimator = GetComponent<Animator>();
        _AnimatorSetIsLanded(false);

        m_bIsHitted = false;
        m_bIsLanded = false;
        m_bIsReachedBase = false;

        m_SoldiderDeadSFXRef = GetComponent<AudioSource>();

        gameObject.GetComponent<Renderer>().enabled = true;
    }

    public void _OnObjectReturn()
    {

    }

    // Update is called once per frame
    void Update()
    {
  
        // Soldier landing
        if (!_IsHitted() && !_IsLanded() && !_IsReachedBase())
        {
            _SoldierLanding();
        }

        if(_IsHitted())
        {
            
        }

        // Make Soldier go to base after landed
        if (_IsLanded() && !_IsReachedBase())
        {
            _MoveToBase();
        }
        
        if(_IsReachedBase())
        {
            
        }
    }

    private void _SoldierHittedSFX()
    {
        m_SoldiderDeadSFXRef.transform.position = transform.position;
        m_SoldiderDeadSFXRef.PlayOneShot(m_SoldiderDeadSFXRef.clip, 1.0f);
    }

    private void _MoveToBase()
    {
        //Debug.Log("_MoveToBase");
        // Change animation to running
        _AnimatorSetIsLanded(true);

        Vector3 tempPos = transform.position;

        // Base pos x = 0, Soldier pos x is going to 0
        // Calculate relative pos of solder to base: -1 if pos.x < base.pos.x; 1 if pos.x > base.pos.x
        float relativePositionSolBase = ((0f - tempPos.x) / Mathf.Abs(0f - tempPos.x));
        tempPos.x += relativePositionSolBase * m_fLandingSpeed * Time.deltaTime;

        transform.position = tempPos;

        // Make Soldier face to Base
        Quaternion tempSolRotation = transform.rotation;
        tempSolRotation.y = (relativePositionSolBase - 1) / 2;

        transform.rotation = tempSolRotation;
    }

    private void _AnimatorSetIsLanded(bool isLanded)
    {
        if(m_SoldierAnimator)
        {
            m_SoldierAnimator.SetBool("IsLanded", isLanded);
        }
    }

    private void _DestroySoldier()
    {
        // Destroy Soldier animation
        m_deadSoldier = Instantiate<GameObject>(m_deadSoldierRef);

        //Map the deadSoldier to the Soldier
        m_deadSoldier.transform.position = transform.position;

        // Add force to deadSoldier
        Rigidbody2D deadSoldierRigidbody = m_deadSoldier.GetComponent<Rigidbody2D>();
        deadSoldierRigidbody.AddForce(m_vBulletForce * m_fForceValue);
        //deadSoldierRigidbody.AddForceAtPosition(m_vBulletForce * m_fForceValue, m_vCollisionContactPos);

        // Hide gameobject but still active it.
        gameObject.GetComponent<Renderer>().enabled = false;

        // Destroy after played sound
        StartCoroutine( _ReturnToPool());
    }

    IEnumerator _ReturnToPool()
    {
        //Destroy(gameObject, m_SoldiderDeadSFXRef.clip.length);
        yield return new WaitForSeconds(m_SoldiderDeadSFXRef.clip.length);
        objectPooler._ReturnObjectToPool(gameObject, soldierPoolTag);
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
            if (GameController.m_sInstance)
            {
                GameController.m_sInstance._IncreaseScore();
            }

            ////SFX
            _SoldierHittedSFX();

            //m_SoldiderDeadSFXRef.PlayOneShot(m_SoldiderDeadSFXRef.clip, 1f);
            _DestroySoldier();

        }

        if (collision.tag == "GroundEdge")
        { 
            if (!_IsHitted())
            {
                m_bIsLanded = true;
            }
        }

        if (collision.tag == "Player")
        { 
            if (!_IsHitted())
            {
                m_bIsReachedBase = true;

                _DestroySoldier();

                if (GameController.m_sInstance)
                {
                    if (GameController.m_sInstance._IsGameplayStarted())
                    {
                        GameController.m_sInstance._StopGameplay();
                    }
                }
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
    
    bool _IsReachedBase()
    {
        return m_bIsReachedBase;
    }
}
