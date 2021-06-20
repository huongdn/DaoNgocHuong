using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour, IPooledObject
{
    ObjectPooler objectPooler;
    string explosionPoolTag;
    public void _OnObjectSpawn()
    {
        gameObject.SetActive(true);
        objectPooler = ObjectPooler.m_sInstance;
        explosionPoolTag = "Explosion";
        StartCoroutine(_ReturnToPool());
    }

    public void _OnObjectReturn()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        //animator.SetBool("Exit", true);
        gameObject.SetActive(false);
    }

    IEnumerator _ReturnToPool()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        float clipTime = 0f;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Explosion":
                    clipTime = clip.length;
                    break;
                default:
                    clipTime = 3.0f;
                    break;
            }
        }
        yield return new WaitForSeconds(clipTime);
        objectPooler._ReturnObjectToPool(gameObject, explosionPoolTag);
    }
}
