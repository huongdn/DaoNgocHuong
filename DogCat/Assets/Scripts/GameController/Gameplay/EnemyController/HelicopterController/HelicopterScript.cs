using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour, IPooledObject
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

    [SerializeField]
    private AudioSource m_HelicopterHittedSFXRef;

    private float m_fDropPos;

    ObjectPooler objectPooler;

    string helicopterPoolTag, explosionPoolTag, soldierPoolTag/*, helicopterFragmentTag*/;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void _OnObjectSpawn()
    {
        if (ObjectPooler.m_sInstance)
        {
            objectPooler = ObjectPooler.m_sInstance;
        }

        helicopterPoolTag = "Helicopter";
        explosionPoolTag = "Explosion";
        soldierPoolTag = "Soldier";
        //helicopterFragmentTag = "HelicopterExplosion";

        m_bIsSoldierDropped = false;
        if (GameController.m_sInstance)
        {
            m_fDropPos = Random.Range(-GameController.m_sInstance._GetWorldWidth() * 0.9f, GameController.m_sInstance._GetWorldWidth() * 0.9f);
        }

        m_HelicopterHittedSFXRef = GetComponent<AudioSource>();

        gameObject.GetComponent<Renderer>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Mathf.Round( transform.rotation.y) == 1)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="FiredBullet" )
        {
            //SFX
            _HelicopterHittedSFX();

            _ExplodeHelicopter();
        }

        if (other.tag == "SideEdge")
        {
            _ExplodeHelicopter();
        }
    }

    private void _HelicopterHittedSFX()
    {
        m_HelicopterHittedSFXRef.transform.position = transform.position;
        m_HelicopterHittedSFXRef.Play();
    }

    void _ExplodeHelicopter()
    {
        GameObject helicopterFragments = Instantiate<GameObject>(m_HelicopterExplosionRef);
        //GameObject helicopterFragments = objectPooler._SpawnFromPool(helicopterFragmentTag);

        //GameObject explosion = Instantiate<GameObject>(m_ExplosionRef);
        GameObject explosion = objectPooler._SpawnFromPool(explosionPoolTag);

        //Map the helicopterFragment to the Helicopter
        helicopterFragments.transform.position = transform.position;
        helicopterFragments.transform.rotation = transform.rotation;

        //Map the explosion to the Helicopter
        explosion.transform.position = transform.position;
        //Animator animator = explosion.GetComponent<Animator>();
        
        //animator.SetTrigger("Explosion");

        // Hide Helicopter for finshing play sound
        gameObject.GetComponent<Renderer>().enabled = false;
        // Destroy  Helicopter after finshed play sound

        //Invoke( "_ReturnToPool", m_HelicopterHittedSFXRef.clip.length);
        StartCoroutine(_ReturnToPool(/*explosion, helicopterFragments*/));
    }

    IEnumerator _ReturnToPool(/*GameObject explosion, GameObject helicopterFragments*/)
    {
        yield return new WaitForSeconds(m_HelicopterHittedSFXRef.clip.length);
        //explosion._OnObjectHide();
        //objectPooler._ReturnObjectToPool(explosion, explosionPoolTag);
        objectPooler._ReturnObjectToPool(gameObject, helicopterPoolTag);
        //objectPooler._ReturnObjectToPool(helicopterFragments, helicopterFragmentTag);
    }

    void _DropSoldier()
    {
        if(!m_bIsSoldierDropped)
        {
            //GameObject soldier =  Instantiate(m_SoldierRef, transform.position, Quaternion.identity);
            GameObject soldier = objectPooler._SpawnFromPool(soldierPoolTag);
            soldier.transform.position = transform.position;
            soldier.transform.rotation = transform.rotation;

            m_bIsSoldierDropped = true;
        }
    }

    public void _OnObjectReturn()
    {
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}
