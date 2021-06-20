using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterExplosionScript : MonoBehaviour, IPooledObject
{
    ObjectPooler objectPooler;
    string helicopterFragmentTag;
    public void _OnObjectReturn()
    {
        
    }

    public void _OnObjectSpawn()
    {
        gameObject.SetActive(true);
        objectPooler = ObjectPooler.m_sInstance;
        helicopterFragmentTag = "HelicopterExplosion";
        StartCoroutine(_ReturnToPool());
    }

    IEnumerator _ReturnToPool()
    {
        yield return new WaitForSeconds(1f);
        objectPooler._ReturnObjectToPool(gameObject, helicopterFragmentTag);
    }
}
