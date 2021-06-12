using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterScript : MonoBehaviour
{
    public float m_fFlyingSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _HelicopterFly();
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
        if(other.tag =="Bullet")
        {
            _DestroySelft();
        }
    }
    void _DestroySelft()
    {
        Destroy(gameObject);
    }
}
