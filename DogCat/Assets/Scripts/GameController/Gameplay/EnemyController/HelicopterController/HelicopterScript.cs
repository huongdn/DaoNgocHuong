using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour
{
    [SerializeField]
    private GameObject m_SoldierRef;

    [SerializeField]
    private GameObject m_HelicopterExplosionRef;
    
    [SerializeField]
    private GameObject m_ExplosionRef;

    [SerializeField]
    private float m_fFlyingSpeed;

    private bool m_bIsSoldierDropped;

    private float m_fDropPos;
    // Start is called before the first frame update
    void Start()
    {
        m_bIsSoldierDropped = false;
        if(GameController.m_sInstance)
        {
            m_fDropPos = Random.Range(-GameController.m_sInstance._GetWorldWidth(), GameController.m_sInstance._GetWorldWidth());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //// Destroy remain Soldier when new game start
        //if (GameController.m_sInstance)
        //{
        //    if (GameController.m_sInstance._IsStartedNewGame())
        //    {
        //        Destroy(gameObject);
        //    }
        //}

        _HelicopterFly();
        if ( (transform.position.x <= m_fDropPos && _IsRotaionLeft()) || (transform.position.x >= m_fDropPos && !_IsRotaionLeft()))
        {
            _DropSoldier();
        }
    }

    void _HelicopterFly()
    {
        Vector3 tempPos = transform.position;

        if(_IsRotaionLeft())
        {
            tempPos.x -= m_fFlyingSpeed * Time.deltaTime;
        }
        else
        {
            tempPos.x += m_fFlyingSpeed * Time.deltaTime;
        }

        transform.position = tempPos;
    }

    bool _IsRotaionLeft()
    {
        //Debug.Log("Quaternion.identity.x:" + Quaternion.identity.x + " Quaternion.identity.y:" + Quaternion.identity.y + " Quaternion.identity.z:" + Quaternion.identity.z);
        //Debug.Log("transform.rotation.x:" + transform.rotation.x + " transform.rotation.y:" + transform.rotation.y + " transform.rotation.z:" + transform.rotation.z);
        //transform.rotation.x
        //Debug.Log("Mathf.Round( transform.rotation.y):" + Mathf.Round(transform.rotation.y));
        if (Mathf.Round( transform.rotation.y) == 1)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="FiredBullet" || other.tag == "SideEdge")
        {
            _ExplodeHelicopter();
        }
    }
    void _ExplodeHelicopter()
    {
        GameObject helicopterFragments = Instantiate<GameObject>(m_HelicopterExplosionRef);

        GameObject explosion = Instantiate<GameObject>(m_ExplosionRef);

        //Map the helicopterFragment to the Helicopter
        helicopterFragments.transform.position = transform.position;
        helicopterFragments.transform.rotation = transform.rotation;

        //Map the explosion to the Helicopter
        explosion.transform.position = transform.position;

        Destroy(gameObject);
    }

    void _DropSoldier()
    {
        if(!m_bIsSoldierDropped)
        {
            Instantiate(m_SoldierRef, transform.position, Quaternion.identity);
            m_bIsSoldierDropped = true;
        }
    }
}
